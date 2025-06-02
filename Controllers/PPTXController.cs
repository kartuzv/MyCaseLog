using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using A = DocumentFormat.OpenXml.Drawing;
using Picture = DocumentFormat.OpenXml.Presentation.Picture;
//src: https://www.shangmayuan.com/a/d48be7a1f953405fa9b4408a.html
//src: https://github.com/tkrotoff/PptxTemplater

namespace PPTXController
{
    public sealed class Pptx : IDisposable
    {
        private readonly PresentationDocument presentationDocument;
        ///  <summary> 
        /// The regex pattern extracts tags from the template
        ///  </summary> 
        public static readonly Regex TagPattern = new Regex(@" {{[A-Za-z0- 9_+\-\.]*}} ");
        ///  <summary> 
        /// MIME type of PowerPoint pptx file
        ///  </summary> 
        public const string MimeType = " application/vnd.openxmlformats-officedocument.presentationml .presentation ";
        ///  <summary>
        /// Initializes a new instance of the class.
        ///  </summary> 
        ///  <param name="file"></param> 
        ///  <param name="access"></param> 
        public Pptx(string file, FileAccess access)
        {
            bool isEditable = false;
            switch (access)
            {
                case FileAccess.Read:
                    isEditable = false;
                    break;
                case FileAccess.Write:
                case FileAccess.ReadWrite:
                    isEditable = true;
                    break;
            }

            this.presentationDocument = PresentationDocument.Open(file, isEditable);
        }
        ///  <summary> 
        /// initialization
        ///  </summary> 
        ///  <param name="stream"> PowerPoint stream. </param> 
        ///  <param name="access"> for opening PowerPoint File access mode </param> 
        ///  <remarks> Open PowerPoint stream in read-write (default) or read-only mode. </remarks> 
        public Pptx(Stream stream, FileAccess access)
        {
            bool isEditable = false;
            switch (access)
            {
                case FileAccess.Read:
                    isEditable = false;
                    break;
                case FileAccess.Write:
                case FileAccess.ReadWrite:
                    isEditable = true;
                    break;
            }

            this.presentationDocument = PresentationDocument.Open(stream, isEditable);
        }



        ///  <summary> 
        /// Close the PowerPoint file
        ///  </summary> 
        public void Dispose()
        {           
            if (presentationDocument != null)
            {
                this.presentationDocument.Dispose();
            }
        }
        /// <summary>
        /// 
        /// </summary>
       
        ///  <summary> 
        /// Calculate the number of slides.
        ///  </summary> 
        ///  <returns></returns> 
        public int SlidesCount()
        {
            PresentationPart presentationPart = this.presentationDocument.PresentationPart;
            return presentationPart.SlideParts.Count();
        }


        /// <summary>
        /// Finds the slides matching a given note.
        /// </summary>
        /// <param name="note">Note to match the slide with.</param>
        /// <returns>The matching slides.</returns>
        public IEnumerable<PptxSlide> FindSlides(string note)
        {
            List<PptxSlide> slides = new List<PptxSlide>();

            for (int i = 0; i < this.SlidesCount(); i++)
            {
                PptxSlide slide = this.GetSlide(i);
                IEnumerable<string> notes = slide.GetNotes();
                foreach (string tmp in notes)
                {
                    if (tmp.Contains(note))
                    {
                        slides.Add(slide);
                        break;
                    }
                }
            }

            return slides;
        }

        /// <summary>
        /// Gets the thumbnail (PNG format) associated with the PowerPoint file.
        /// </summary>
        /// <param name="size">The size of the thumbnail to generate, default is 256x192 pixels in 4:3 (160x256 in 16:10 portrait).</param>
        /// <returns>The thumbnail as a byte array (PNG format).</returns>
        /// <remarks>
        /// Even if the PowerPoint file does not contain any slide, still a thumbnail is generated.
        /// If the given size is superior to the default size then the thumbnail is upscaled and looks blurry so don't do it.
        /// </remarks>
        public byte[] GetThumbnail(Size size = default(Size))
        {
            byte[] thumbnail;

            var thumbnailPart = this.presentationDocument.ThumbnailPart;
            using (var stream = thumbnailPart.GetStream(FileMode.Open, FileAccess.Read))
            {
                var image = Image.FromStream(stream);
                if (size != default(Size))
                {
                    image = image.GetThumbnailImage(size.Width, size.Height, null, IntPtr.Zero);
                }

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    thumbnail = memoryStream.ToArray();
                }
            }

            return thumbnail;
        }

        /// <summary>
        /// Gets all the slides inside PowerPoint file.
        /// </summary>
        /// <returns>All the slides.</returns>
        public IEnumerable<PptxSlide> GetSlides()
        {
            List<PptxSlide> slides = new List<PptxSlide>();
            int nbSlides = this.SlidesCount();
            for (int i = 0; i < nbSlides; i++)
            {
                slides.Add(this.GetSlide(i));
            }
            return slides;
        }

        ///  <summary> 
        /// Get the PptxSlide of the given slide index
        ///  </summary> 
        ///  <param name="slideIndex"> Index of the slide. </param> 
        ///  <returns> A PptxSlide. </returns> 
        public PptxSlide GetSlide(int slideIndex)
        {
            PresentationPart presentationPart = this.presentationDocument.PresentationPart;

            // Get the collection of slide IDs
            OpenXmlElementList slideIds = presentationPart.Presentation.SlideIdList.ChildElements;

            // Get the relationship ID of the slide
            string relId = ((SlideId)slideIds[slideIndex]).RelationshipId;

            // Get the specified slide part from the relationship ID
            SlidePart slidePart = (SlidePart)presentationPart.GetPartById(relId);

            return new PptxSlide(presentationPart, slidePart);
        }

        /// <summary>
        /// Replaces the cells from a table (tbl).
        /// Algorithm for a slide template containing one table.
        /// </summary>
        public static IEnumerable<PptxSlide> ReplaceTable_One(PptxSlide slideTemplate, PptxTable tableTemplate, IList<PptxTable.Cell[]> rows)
        {
            return ReplaceTable_Multiple(slideTemplate, tableTemplate, rows, new List<PptxSlide>());
        }

        /// <summary>
        /// Replaces the cells from a table (tbl).
        /// Algorithm for a slide template containing multiple tables.
        /// </summary>
        /// <param name="slideTemplate">The slide template that contains the table(s).</param>
        /// <param name="tableTemplate">The table (tbl) to use, should be inside the slide template.</param>
        /// <param name="rows">The rows to replace the table's cells.</param>
        /// <param name="existingSlides">Existing slides created for the other tables inside the slide template.</param>
        /// <returns>The newly created slides if any.</returns>
        public static IEnumerable<PptxSlide> ReplaceTable_Multiple(PptxSlide slideTemplate, PptxTable tableTemplate, IList<PptxTable.Cell[]> rows, List<PptxSlide> existingSlides)
        {
            List<PptxSlide> slidesCreated = new List<PptxSlide>();

            string tag = tableTemplate.Title;

            PptxSlide lastSlide = slideTemplate;
            if (existingSlides.Count > 0)
            {
                lastSlide = existingSlides.Last();
            }

            PptxSlide lastSlideTemplate = lastSlide.Clone();

            foreach (PptxSlide slide in existingSlides)
            {
                PptxTable table = slide.FindTables(tag).First();
                List<PptxTable.Cell[]> remainingRows = table.SetRows(rows);
                rows = remainingRows;
            }

            // Force SetRows() at least once if there is no existingSlides
            // this means we are being called by ReplaceTable_One()
            bool loopOnce = existingSlides.Count == 0;

            while (loopOnce || rows.Count > 0)
            {
                PptxSlide newSlide = lastSlideTemplate.Clone();
                PptxTable table = newSlide.FindTables(tag).First();
                List<PptxTable.Cell[]> remainingRows = table.SetRows(rows);
                rows = remainingRows;

                PptxSlide.InsertAfter(newSlide, lastSlide);
                lastSlide = newSlide;
                slidesCreated.Add(newSlide);

                if (loopOnce) loopOnce = false;
            }

            lastSlideTemplate.Remove();

            return slidesCreated;
        }

    }


    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Text.RegularExpressions;
    //using System.Web;


    /// <summary>
    /// Represents a paragraph inside a PowerPoint file.
    /// </summary>
    /// <remarks>
    /// Could not simply be named Paragraph, conflicts with DocumentFormat.OpenXml.Drawing.Paragraph.
    ///
    /// Structure of a paragraph:
    /// a:p (Paragraph)
    ///  a:r (Run)
    ///   a:t (Text)
    ///
    /// <![CDATA[
    /// <a:p>
    ///  <a:r>
    ///   <a:rPr lang="en-US" dirty="0" smtClean="0"/>
    ///   <a:t>
    ///    Hello this is a tag: {{hello}}
    ///   </a:t>
    ///  </a:r>
    ///  <a:endParaRPr lang="fr-FR" dirty="0"/>
    /// </a:p>
    ///
    /// <a:p>
    ///  <a:r>
    ///   <a:rPr lang="en-US" dirty="0" smtClean="0"/>
    ///   <a:t>
    ///    Another tag: {{bonjour
    ///   </a:t>
    ///  </a:r>
    ///  <a:r>
    ///   <a:rPr lang="en-US" dirty="0" smtClean="0"/>
    ///   <a:t>
    ///    }} le monde !
    ///   </a:t>
    ///  </a:r>
    ///  <a:endParaRPr lang="en-US" dirty="0"/>
    /// </a:p>
    /// ]]>
    /// </remarks>
    internal static class PptxParagraph
    {
        /// <summary>
        /// Replaces a tag inside a paragraph (a:p).
        /// </summary>
        /// <param name="p">The paragraph (a:p).</param>
        /// <param name="tag">The tag to replace by newText, if null or empty do nothing; tag is a regex string.</param>
        /// <param name="newText">The new text to replace the tag with, if null replaced by empty string.</param>
        /// <returns><c>true</c> if a tag has been found and replaced, <c>false</c> otherwise.</returns>
        internal static bool ReplaceTag(A.Paragraph p, string tag, string newText)
        {
            bool replaced = false;

            if (string.IsNullOrEmpty(tag))
            {
                return replaced;
            }

            if (newText == null)
            {
                newText = string.Empty;
            }
            newText = RemoveInvalidXMLChars(newText);

            while (true)
            {
                // Search for the tag
                Match match = Regex.Match(GetTexts(p), tag);
                if (!match.Success)
                {
                    break;
                }

                replaced = true;

                List<TextIndex> texts = GetTextIndexList(p);

                for (int i = 0; i < texts.Count; i++)
                {
                    TextIndex text = texts[i];
                    if (match.Index >= text.StartIndex && match.Index <= text.EndIndex)
                    {
                        // Got the right A.Text

                        int index = match.Index - text.StartIndex;
                        int done = 0;

                        for (; i < texts.Count; i++)
                        {
                            TextIndex currentText = texts[i];
                            List<char> currentTextChars = new List<char>(currentText.Text.Text.ToCharArray());

                            for (int k = index; k < currentTextChars.Count; k++, done++)
                            {
                                if (done < newText.Length)
                                {
                                    if (done >= tag.Length - 1)
                                    {
                                        // Case if newText is longer than the tag
                                        // Insert characters
                                        int remains = newText.Length - done;
                                        currentTextChars.RemoveAt(k);
                                        currentTextChars.InsertRange(k, newText.Substring(done, remains));
                                        done += remains;
                                        break;
                                    }
                                    else
                                    {
                                        currentTextChars[k] = newText[done];
                                    }
                                }
                                else
                                {
                                    if (done < tag.Length)
                                    {
                                        // Case if newText is shorter than the tag
                                        // Erase characters
                                        int remains = tag.Length - done;
                                        if (remains > currentTextChars.Count - k)
                                        {
                                            remains = currentTextChars.Count - k;
                                        }
                                        currentTextChars.RemoveRange(k, remains);
                                        done += remains;
                                        break;
                                    }
                                    else
                                    {
                                        // Regular case, nothing to do
                                        //currentTextChars[k] = currentTextChars[k];
                                    }
                                }
                            }

                            currentText.Text.Text = new string(currentTextChars.ToArray());
                            index = 0;
                        }
                    }
                }
            }

            return replaced;
        }

        /// <summary>
        /// Removes characters that are invalid for XML encoding.
        /// </summary>
        /// <param name="input">Text to be encoded.</param>
        /// <returns>Text with invalid XML characters removed.</returns>
        /// <remarks>
        /// <see href="http://stackoverflow.com/questions/20762/how-do-you-remove-invalid-hexadecimal-characters-from-an-xml-based-data-source-p">How do you remove invalid hexadecimal characters from an XML-based data source</see>
        /// </remarks>
        private static string RemoveInvalidXMLChars(string input)
        {
            return new string(input.Where(value =>
                                (value >= 0x0020 && value <= 0xD7FF) ||
                                (value >= 0xE000 && value <= 0xFFFD) ||
                                value == 0x0009 ||
                                value == 0x000A ||
                                value == 0x000D).ToArray());
        }

        /// <summary>
        /// Returns all the texts found inside a given paragraph.
        /// </summary>
        /// <remarks>
        /// If all A.Text in the given paragraph are empty, returns an empty string.
        /// </remarks>
        internal static string GetTexts(A.Paragraph p)
        {
            StringBuilder concat = new StringBuilder();
            foreach (A.Text t in p.Descendants<A.Text>())
            {
                concat.Append(t.Text);
            }
            return concat.ToString();
        }

        /// <summary>
        /// Associates a A.Text with start and end index matching a paragraph full string (= the concatenation of all A.Text of a paragraph).
        /// </summary>
        private class TextIndex
        {
            public A.Text Text { get; private set; }
            public int StartIndex { get; private set; }
            public int EndIndex { get { return StartIndex + Text.Text.Length; } }

            public TextIndex(A.Text t, int startIndex)
            {
                this.Text = t;
                this.StartIndex = startIndex;
            }
        }

        /// <summary>
        /// Gets all the TextIndex for a given paragraph.
        /// </summary>
        private static List<TextIndex> GetTextIndexList(A.Paragraph p)
        {
            List<TextIndex> texts = new List<TextIndex>();

            StringBuilder concat = new StringBuilder();
            foreach (A.Text t in p.Descendants<A.Text>())
            {
                int startIndex = concat.Length;
                texts.Add(new TextIndex(t, startIndex));
                concat.Append(t.Text);
            }

            return texts;
        }
    }

    //using DocumentFormat.OpenXml.Packaging;
    //using DocumentFormat.OpenXml.Presentation;
    //using System;
    //using System.Collections.Generic;
    //using System.IO;
    //using System.Linq;
    //using System.Web;


    //using A = DocumentFormat.OpenXml.Drawing;

    public class PptxSlide
    {
        /// <summary>
        /// Holds the presentation part.
        /// </summary>
        private readonly PresentationPart presentationPart;

        /// <summary>
        /// Holds the slide part.
        /// </summary>
        private readonly SlidePart slidePart;

        /// <summary>
        /// Initializes a new instance of the <see cref="PptxSlide"/> class.
        /// </summary>
        /// <param name="presentationPart">The presentation part.</param>
        /// <param name="slidePart">The slide part.</param>
        internal PptxSlide(PresentationPart presentationPart, SlidePart slidePart)
        {
            this.presentationPart = presentationPart;
            this.slidePart = slidePart;
        }

        /// <summary>
        /// Gets all the texts found inside the slide.
        /// </summary>
        /// <returns>The list of texts detected into the slide.</returns>
        /// <remarks>
        /// Some strings inside the array can be empty, this happens when all A.Text from a paragraph are empty
        /// <see href="http://msdn.microsoft.com/en-us/library/office/cc850836">How to: Get All the Text in a Slide in a Presentation</see>
        /// </remarks>
        public IEnumerable<string> GetTexts()
        {
            return this.slidePart.Slide.Descendants<A.Paragraph>().Select(p => PptxParagraph.GetTexts(p));
        }

        /// <summary>
        /// Gets the slide title if any.
        /// </summary>
        /// <returns>The title or an empty string.</returns>
        public string GetTitle()
        {
            string title = string.Empty;

            // Find the title if any
            Shape titleShape = this.slidePart.Slide.Descendants<Shape>().FirstOrDefault(sp => IsShapeATitle(sp));
            if (titleShape != null)
            {
                title = string.Join(" ", titleShape.Descendants<A.Paragraph>().Select(p => PptxParagraph.GetTexts(p)));
            }

            return title;
        }

        /// <summary>
        /// Gets all the notes associated with the slide.
        /// </summary>
        /// <returns>All the notes.</returns>
        /// <remarks>
        /// <see href="http://msdn.microsoft.com/en-us/library/office/gg278319.aspx">Working with Notes Slides</see>
        /// </remarks>
        public IEnumerable<string> GetNotes()
        {
            var notes = new List<string>();
            if (this.slidePart.NotesSlidePart != null)
            {
                notes.AddRange(this.slidePart.NotesSlidePart.NotesSlide.Descendants<A.Paragraph>().Select(p => PptxParagraph.GetTexts(p)));
            }
            return notes;
        }

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns>All the tables.</returns>
        /// <remarks>Assigns an "artificial" id (tblId) to the tables that match the tag.</remarks>
        public IEnumerable<PptxTable> GetTables()
        {
            var tables = new List<PptxTable>();

            int tblId = 0;
            foreach (GraphicFrame graphicFrame in this.slidePart.Slide.Descendants<GraphicFrame>())
            {
                var cNvPr = graphicFrame.NonVisualGraphicFrameProperties.NonVisualDrawingProperties;
                if (cNvPr.Description != null)
                {
                    cNvPr.Title = cNvPr.Description;
                }
                if (cNvPr.Title != null)
                {
                    string title = cNvPr.Title.Value;
                    tables.Add(new PptxTable(this, tblId, title));
                    tblId++;
                }
            }

            return tables;
        }

        ///  <summary> 
        /// Find the table in the slide given its tag
        ///  </summary> 
        ///  <param name="tag"> The tag associated with the table so it can be found. </param> 
        ///  <returns> The table or null. </returns> 
        public IEnumerable<PptxTable> FindTables(string tag)
        {
            return this.GetTables().Where(table => table.Title.Contains(tag));
        }

        /// <summary>
        /// Type of replacement to perform inside ReplaceTag().
        /// </summary>
        public enum ReplacementType
        {
            /// <summary>
            /// Replaces the tags everywhere.
            /// </summary>
            Global,

            /// <summary>
            /// Does not replace tags that are inside a table.
            /// </summary>
            NoTable
        }

        /// <summary>
        /// replace tags
        /// </summary>
        /// <param name="tag">The tag to replace by newText, if null or empty do nothing; tag is a regex string.</param>
        /// <param name="newText">The new text to replace the tag with, if null replaced by empty string.</param>
        /// <param name="replacementType">The type of replacement to perform.</param>
        public void ReplaceTag(string tag, string newText, ReplacementType replacementType)
        {
            foreach (A.Paragraph p in this.slidePart.Slide.Descendants<A.Paragraph>())
            {
                switch (replacementType)
                {
                    case ReplacementType.Global:
                        PptxParagraph.ReplaceTag(p, tag, newText);
                        break;

                    case ReplacementType.NoTable:
                        var tables = p.Ancestors<A.Table>();
                        if (!tables.Any())
                        {
                            // If the paragraph has no table ancestor
                            PptxParagraph.ReplaceTag(p, tag, newText);
                        }
                        break;
                }
            }

            this.Save();
        }

        ///  <summary> 
        /// Replaces the text (marker) in the slide with another text (marker) for the given PptxTable.Cell.
        /// This is a convenient method that overloads the original ReplaceTag() method.
        ///  </summary> 
        ///  <param name="tagPair"> The tag/new text, BackgroundPicture is ignored. </param> 
        ///  <param name="replacementType"> The type of replacement to perform. </param> 
        public void ReplaceTag(PptxTable.Cell tagPair, ReplacementType replacementType)
        {
            this.ReplaceTag(tagPair.Tag, tagPair.NewText, replacementType);
        }

        /// <summary>
        /// Replaces a picture by another inside the slide.
        /// </summary>
        /// <param name="tag">The tag associated with the original picture so it can be found, if null or empty do nothing.</param>
        /// <param name="newPicture">The new picture (as a byte array) to replace the original picture with, if null do nothing.</param>
        /// <param name="contentType">The picture content type: image/png, image/jpeg...</param>
        /// <remarks>
        /// <see href="http://stackoverflow.com/questions/7070074/how-can-i-retrieve-images-from-a-pptx-file-using-ms-open-xml-sdk">How can I retrieve images from a .pptx file using MS Open XML SDK?</see>
        /// <see href="http://stackoverflow.com/questions/7137144/how-can-i-retrieve-some-image-data-and-format-using-ms-open-xml-sdk">How can I retrieve some image data and format using MS Open XML SDK?</see>
        /// <see href="http://msdn.microsoft.com/en-us/library/office/bb497430.aspx">How to: Insert a Picture into a Word Processing Document</see>
        /// </remarks>
        public void ReplacePicture(string tag, byte[] newPicture, string contentType)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            if (newPicture == null)
            {
                return;
            }

            ImagePart imagePart = this.AddPicture(newPicture, contentType);

            foreach (Picture pic in this.slidePart.Slide.Descendants<Picture>())
            {
                var cNvPr = pic.NonVisualPictureProperties.NonVisualDrawingProperties;
                if (cNvPr.Description != null)
                {
                    cNvPr.Title = cNvPr.Description.Value;
                }
                if (cNvPr.Title != null)
                {
                    string title = cNvPr.Title.Value;
                    if (title.Contains(tag))
                    {
                        // Gets the relationship ID of the part
                        string rId = this.slidePart.GetIdOfPart(imagePart);

                        pic.BlipFill.Blip.Embed.Value = rId;
                    }
                }
            }

            // Need to save the slide otherwise the relashionship is not saved.
            // Example: <a:blip r:embed="rId2">
            // r:embed is not updated with the right rId
            this.Save();
        }

        /// <summary>
        /// Replaces a picture by another inside the slide.
        /// </summary>
        /// <param name="tag">The tag associated with the original picture so it can be found, if null or empty do nothing.</param>
        /// <param name="newPictureFile">The new picture (as a file path) to replace the original picture with, if null do nothing.</param>
        /// <param name="contentType">The picture content type: image/png, image/jpeg...</param>
        public void ReplacePicture(string tag, string newPictureFile, string contentType)
        {
            byte[] bytes = File.ReadAllBytes(newPictureFile);
            this.ReplacePicture(tag, bytes, contentType);
        }

        /// <summary>
        /// Clones this slide.
        /// </summary>
        /// <returns>The clone.</returns>
        /// <remarks>
        /// <see href="http://blogs.msdn.com/b/brian_jones/archive/2009/08/13/adding-repeating-data-to-powerpoint.aspx">Adding Repeating Data to PowerPoint</see>
        /// <see href="http://startbigthinksmall.wordpress.com/2011/05/17/cloning-a-slide-using-open-xml-sdk-2-0/">Cloning a Slide using Open Xml SDK 2.0</see>
        /// <see href="http://www.exsilio.com/blog/post/2011/03/21/Cloning-Slides-including-Images-and-Charts-in-PowerPoint-presentations-Using-Open-XML-SDK-20-Productivity-Tool.aspx">See Cloning Slides including Images and Charts in PowerPoint presentations and Using Open XML SDK 2.0 Productivity Tool</see>
        /// </remarks>
        public PptxSlide Clone()
        {
            SlidePart slideTemplate = this.slidePart;

            // Clone slide contents
            SlidePart slidePartClone = this.presentationPart.AddNewPart<SlidePart>();
            using (var templateStream = slideTemplate.GetStream(FileMode.Open))
            {
                slidePartClone.FeedData(templateStream);
            }

            // Copy layout part
            slidePartClone.AddPart(slideTemplate.SlideLayoutPart);

            // Copy the image parts
            foreach (ImagePart image in slideTemplate.ImageParts)
            {
                ImagePart imageClone = slidePartClone.AddImagePart(image.ContentType, slideTemplate.GetIdOfPart(image));
                using (var imageStream = image.GetStream())
                {
                    imageClone.FeedData(imageStream);
                }
            }

            return new PptxSlide(this.presentationPart, slidePartClone);
        }

        /// <summary>
        /// Inserts this slide after a given target slide.
        /// </summary>
        /// <param name="newSlide">The new slide to insert.</param>
        /// <param name="prevSlide">The previous slide.</param>
        /// <remarks>
        /// This slide will be inserted after the slide specified as a parameter.
        /// <see href="http://startbigthinksmall.wordpress.com/2011/05/17/cloning-a-slide-using-open-xml-sdk-2-0/">Cloning a Slide using Open Xml SDK 2.0</see>
        /// </remarks>
        public static void InsertAfter(PptxSlide newSlide, PptxSlide prevSlide)
        {
            // Find the presentationPart
            var presentationPart = prevSlide.presentationPart;

            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the slide id where to insert our slide
            SlideId prevSlideId = null;
            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                // See http://openxmldeveloper.org/discussions/development_tools/f/17/p/5302/158602.aspx
                if (slideId.RelationshipId == presentationPart.GetIdOfPart(prevSlide.slidePart))
                {
                    prevSlideId = slideId;
                    break;
                }
            }

            // Find the highest id
            uint maxSlideId = slideIdList.ChildElements.Cast<SlideId>().Max(x => x.Id.Value);

            // public override T InsertAfter<T>(T newChild, DocumentFormat.OpenXml.OpenXmlElement refChild)
            // Inserts the specified element immediately after the specified reference element.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId + 1;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(newSlide.slidePart);
        }

        /// <summary>
        /// Removes the slide from the PowerPoint file.
        /// </summary>
        /// <remarks>
        /// <see href="http://msdn.microsoft.com/en-us/library/office/cc850840.aspx">How to: Delete a Slide from a Presentation</see>
        /// </remarks>
        public void Remove()
        {
            SlideIdList slideIdList = this.presentationPart.Presentation.SlideIdList;

            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.RelationshipId == this.presentationPart.GetIdOfPart(this.slidePart))
                {
                    slideIdList.RemoveChild(slideId);
                    break;
                }
            }

            this.presentationPart.DeletePart(this.slidePart);
        }

        /// <summary>
        /// Determines whether the given shape is a title.
        /// </summary>
        private static bool IsShapeATitle(Shape sp)
        {
            bool isTitle = false;

            var ph = sp.NonVisualShapeProperties.ApplicationNonVisualDrawingProperties.GetFirstChild<PlaceholderShape>();
            if (ph != null && ph.Type != null && ph.Type.HasValue)
            {
				var phHolderShapeType = ((PlaceholderValues)ph.Type);
				// Any title shape.// A centered title.
				isTitle =  (phHolderShapeType == PlaceholderValues.Title || phHolderShapeType == PlaceholderValues.CenteredTitle);

            }

            return isTitle;
        }

        /// <summary>
        /// 将新图片添加到之后的幻灯片中
        /// </summary>
        /// <param name="picture">The picture as a byte array.</param>
        /// <param name="contentType">The picture content type: image/png, image/jpeg...</param>
        /// <returns>The image part</returns>
        internal ImagePart AddPicture(byte[] picture, string contentType)
        {
         
            var type  = ImagePartType.Jpeg;
			switch (contentType)
            {
                case "image/bmp":
                    type = ImagePartType.Bmp;
                    break;
                case "image/emf": // TODO
                    type = ImagePartType.Emf;
                    break;
                case "image/gif": // TODO
                    type = ImagePartType.Gif;
                    break;
                case "image/ico": // TODO
                    type = ImagePartType.Icon;
                    break;
                case "image/jpeg":
                    type = ImagePartType.Jpeg;
                    break;
                case "image/pcx": // TODO
                    type = ImagePartType.Pcx;
                    break;
                case "image/png":
                    type = ImagePartType.Png;
                    break;
                case "image/tiff": // TODO
                    type = ImagePartType.Tiff;
                    break;
                case "image/wmf": // TODO
                    type = ImagePartType.Wmf;
                    break;
            }

            ImagePart imagePart = this.slidePart.AddImagePart(type);

            // FeedData() closes the stream and we cannot reuse it (ObjectDisposedException)
            // solution: copy the original stream to a MemoryStream
            using (MemoryStream stream = new MemoryStream(picture))
            {
                imagePart.FeedData(stream);
            }

            // No need to detect duplicated images
            // PowerPoint do it for us on the next manual save

            return imagePart;
        }

        /// <summary>
        /// Gets the relationship ID of a given image part.
        /// </summary>
        /// <param name="imagePart">The image part.</param>
        /// <returns>The relationship ID of the image part.</returns>
        internal string GetIdOfImagePart(ImagePart imagePart)
        {
            return this.slidePart.GetIdOfPart(imagePart);
        }

        /// <summary>
        /// Finds a table (a:tbl) given its "artificial" id (tblId).
        /// </summary>
        /// <param name="tblId">The table id.</param>
        /// <returns>The table or null if not found.</returns>
        /// <remarks>The "artificial" id (tblId) is created inside FindTables().</remarks>
        internal A.Table FindTable(int tblId)
        {
            A.Table tbl = null;

            IEnumerable<GraphicFrame> graphicFrames = this.slidePart.Slide.Descendants<GraphicFrame>();
            GraphicFrame graphicFrame = graphicFrames.ElementAt(tblId);
            if (graphicFrame != null)
            {
                tbl = graphicFrame.Descendants<A.Table>().First();
            }

            return tbl;
        }

        /// <summary>
        /// Removes a table (a:tbl) given its "artificial" id (tblId).
        /// </summary>
        /// <param name="tblId">The table id.</param>
        /// <remarks>
        /// <![CDATA[
        /// p:graphicFrame
        ///  a:graphic
        ///   a:graphicData
        ///    a:tbl (Table)
        /// ]]>
        /// </remarks>
        internal void RemoveTable(int tblId)
        {
            IEnumerable<GraphicFrame> graphicFrames = this.slidePart.Slide.Descendants<GraphicFrame>();
            GraphicFrame graphicFrame = graphicFrames.ElementAt(tblId);
            graphicFrame.Remove();
        }

        /// <summary>
        /// Saves the slide.
        /// </summary>
        /// <remarks>
        /// This is mandatory to save the slides after modifying them otherwise
        /// the next manipulation that will be performed on the pptx won't
        /// include the modifications done before.
        /// </remarks>
        internal void Save()
        {
            this.slidePart.Slide.Save();
        }
    }


    //using DocumentFormat.OpenXml.Packaging;
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Web;

    //using A = DocumentFormat.OpenXml.Drawing;
    public class PptxTable
    {
        private PptxSlide slideTemplate;

        private readonly int tblId;

        public string Title { get; private set; }

        internal PptxTable(PptxSlide slideTemplate, int tblId, string title)
        {
            this.slideTemplate = slideTemplate;
            this.tblId = tblId;
            this.Title = title;
        }

        /// <summary>
        /// Represents a cell inside a table (a:tbl).
        /// </summary>
        public class Cell
        {
            internal string Tag { get; private set; }

            internal string NewText { get; private set; }

            public class BackgroundPicture
            {
                public byte[] Content { get; set; }
                public string ContentType { get; set; }
                public int Top { get; set; }
                public int Right { get; set; }
                public int Bottom { get; set; }
                public int Left { get; set; }
            }

            internal BackgroundPicture Picture { get; private set; }

            public Cell(string tag, string newText)
            {
                this.Tag = tag;
                this.NewText = newText;
            }

            public Cell(string tag, string newText, BackgroundPicture backgroundPicture)
            {
                this.Tag = tag;
                this.NewText = newText;
                this.Picture = backgroundPicture;
            }
        }

        /// <summary>
        /// Removes the table from the slide.
        /// </summary>
        public void Remove()
        {
            this.slideTemplate.RemoveTable(this.tblId);
        }

        /// <summary>
        /// Removes the given columns.
        /// </summary>
        /// <param name="columns">Indexes of the columns to remove.</param>
        public void RemoveColumns(IEnumerable<int> columns)
        {
            A.Table tbl = this.slideTemplate.FindTable(this.tblId);
            A.TableGrid tblGrid = tbl.TableGrid;

            // Remove the latest columns first
            IEnumerable<int> columnsSorted = from column in columns
                                             orderby column descending
                                             select column;

            int tblRowsCount = RowsCount(tbl);

            foreach (int column in columnsSorted)
            {
                for (int row = 0; row < tblRowsCount; row++)
                {
                    A.TableRow tr = GetRow(tbl, row);

                    // Remove the column from the row
                    A.TableCell tc = GetCell(tr, column);
                    tc.Remove();
                }

                // Remove the column from TableGrid
                A.GridColumn gridCol = tblGrid.Descendants<A.GridColumn>().ElementAt(column);
                gridCol.Remove();
            }

            this.slideTemplate.Save();
        }

        /// <summary>
        /// Sets a background picture for a table cell (a:tc).
        /// </summary>
        /// <remarks>
        /// <![CDATA[
        /// <a:tc>
        ///  <a:txBody>
        ///   <a:bodyPr/>
        ///   <a:lstStyle/>
        ///   <a:p>
        ///    <a:endParaRPr lang="fr-FR" dirty="0"/>
        ///   </a:p>
        ///  </a:txBody>
        ///  <a:tcPr> (TableCellProperties)
        ///   <a:blipFill dpi="0" rotWithShape="1">
        ///    <a:blip r:embed="rId2"/>
        ///    <a:srcRect/>
        ///    <a:stretch>
        ///     <a:fillRect b="12000" r="90000" t="14000"/>
        ///    </a:stretch>
        ///   </a:blipFill>
        ///  </a:tcPr>
        /// </a:tc>
        /// ]]>
        /// </remarks>
        private static void SetTableCellPropertiesWithBackgroundPicture(PptxSlide slide, A.TableCellProperties tcPr, Cell.BackgroundPicture backgroundPicture)
        {
            if (backgroundPicture.Content == null)
            {
                return;
            }

            ImagePart imagePart = slide.AddPicture(backgroundPicture.Content, backgroundPicture.ContentType);

            A.BlipFill blipFill = new A.BlipFill();
            A.Blip blip = new A.Blip() { Embed = slide.GetIdOfImagePart(imagePart) };
            A.SourceRectangle srcRect = new A.SourceRectangle();
            A.Stretch stretch = new A.Stretch();
            A.FillRectangle fillRect = new A.FillRectangle()
            {
                Top = backgroundPicture.Top,
                Right = backgroundPicture.Right,
                Bottom = backgroundPicture.Bottom,
                Left = backgroundPicture.Left
            };
            stretch.AppendChild(fillRect);
            blipFill.AppendChild(blip);
            blipFill.AppendChild(srcRect);
            blipFill.AppendChild(stretch);
            tcPr.AppendChild(blipFill);
        }

        /// <summary>
        /// Replaces a tag inside the table (a:tbl).
        /// </summary>
        /// <param name="cell">Contains the tag, the new text and a pciture.</param>
        /// <returns><c>true</c> if a tag has been found and replaced, <c>false</c> otherwise.</returns>
        public bool ReplaceTag(Cell cell)
        {
            bool replacedAtLeastOnce = false;

            PptxSlide slide = this.slideTemplate;
            A.Table tbl = slide.FindTable(this.tblId);

            // a:tr
            foreach (A.TableRow tr in tbl.Descendants<A.TableRow>())
            {
                // a:tc
                foreach (A.TableCell tc in tr.Descendants<A.TableCell>())
                {
                    bool replaced = ReplaceTag(slide, tc, cell);
                    if (replaced)
                    {
                        replacedAtLeastOnce = true;
                    }
                }
            }

            return replacedAtLeastOnce;
        }

        /// <summary>
        /// Replaces a tag inside a given table cell (a:tc).
        /// </summary>
        /// <param name="slide">The PptxSlide.</param>
        /// <param name="tc">The table cell (a:tc).</param>
        /// <param name="cell">Contains the tag, the new text and a picture.</param>
        /// <returns><c>true</c> if a tag has been found and replaced, <c>false</c> otherwise.</returns>
        private static bool ReplaceTag(PptxSlide slide, A.TableCell tc, Cell cell)
        {
            bool replacedAtLeastOnce = false;

            // a:p
            foreach (A.Paragraph p in tc.Descendants<A.Paragraph>())
            {
                bool replaced = PptxParagraph.ReplaceTag(p, cell.Tag, cell.NewText);
                if (replaced)
                {
                    replacedAtLeastOnce = true;

                    // a:tcPr
                    if (cell.Picture != null)
                    {
                        A.TableCellProperties tcPr = tc.GetFirstChild<A.TableCellProperties>();
                        SetTableCellPropertiesWithBackgroundPicture(slide, tcPr, cell.Picture);
                    }
                }
            }

            return replacedAtLeastOnce;
        }

        /// <summary>
        /// Replaces the cells from the table (tbl).
        /// </summary>
        /// <returns>The list of remaining rows that could not be inserted, you will have to create a new slide.</returns>
        public List<Cell[]> SetRows(IList<Cell[]> rows)
        {
            PptxSlide slide = this.slideTemplate;
            A.Table tbl = slide.FindTable(this.tblId);

            int tblRowsCount = RowsCount(tbl);

            // done starts at 1 instead of 0 because we don't care about the first row
            // The first row contains the titles for the columns
            int done = 1;
            for (int i = 0; i < rows.Count(); i++)
            {
                Cell[] row = rows[i];

                if (done < tblRowsCount)
                {
                    // a:tr
                    A.TableRow tr = GetRow(tbl, done);

                    // a:tc
                    foreach (A.TableCell tc in tr.Descendants<A.TableCell>())
                    {
                        foreach (Cell cell in row)
                        {
                            ReplaceTag(slide, tc, cell);
                        }
                    }

                    done++;
                }
                else
                {
                    break;
                }
            }

            // Remove the last remaining rows if any
            for (int row = tblRowsCount - 1; row >= done; row--)
            {
                A.TableRow tr = GetRow(tbl, row);
                tr.Remove();
            }

            // Save the latest slide
            // Mandatory otherwise the next time SetRows() is run (on a different table)
            // the rows from the previous tables will not contained the right data (from PptxParagraph.ReplaceTag())
            slide.Save();

            // Computes the remaining rows if any
            List<Cell[]> remainingRows = new List<Cell[]>();
            for (int row = done - 1; row < rows.Count; row++)
            {
                remainingRows.Add(rows[row]);
            }

            return remainingRows;
        }

        /// <summary>
        /// Gets the columns titles as an array of strings.
        /// </summary>
        public IEnumerable<string> ColumnTitles()
        {
            List<string> titles = new List<string>();

            A.Table tbl = this.slideTemplate.FindTable(this.tblId);
            A.TableRow tr = GetRow(tbl, 0); // The first table row == the columns

            int columnsCount = this.ColumnsCount();
            for (int i = 0; i < columnsCount; i++)
            {
                A.TableCell tc = GetCell(tr, i);
                var text = string.Join(" ", tc.Descendants<A.Paragraph>().Select(PptxParagraph.GetTexts).ToArray());
                titles.Add(text);
            }

            return titles;
        }

        /// <summary>
        /// Gets the number of columns inside the table (tbl).
        /// </summary>
        /// <returns>The number of columns.</returns>
        public int ColumnsCount()
        {
            A.Table tbl = this.slideTemplate.FindTable(this.tblId);
            return CellsCount(tbl) / RowsCount(tbl);
        }

        /// <summary>
        /// Gets the number of cells inside the table (tbl).
        /// </summary>
        /// <returns>The number of cells.</returns>
        public int CellsCount()
        {
            A.Table tbl = this.slideTemplate.FindTable(this.tblId);
            return CellsCount(tbl);
        }

        /// <summary>
        /// Helper method.
        /// </summary>
        private static int CellsCount(A.Table tbl)
        {
            return tbl.Descendants<A.TableCell>().Count();
        }

        /// <summary>
        /// Helper method.
        /// </summary>
        private static int RowsCount(A.Table tbl)
        {
            return tbl.Descendants<A.TableRow>().Count();
        }

        /// <summary>
        /// Helper method.
        /// </summary>
        private static A.TableRow GetRow(A.Table tbl, int row)
        {
            A.TableRow tr = tbl.Descendants<A.TableRow>().ElementAt(row);
            return tr;
        }

        /// <summary>
        /// Helper method.
        /// </summary>
        private static A.TableCell GetCell(A.TableRow tr, int column)
        {
            A.TableCell tc = tr.Descendants<A.TableCell>().ElementAt(column);
            return tc;
        }
    }
}
