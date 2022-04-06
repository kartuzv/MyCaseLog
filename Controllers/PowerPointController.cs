﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using P14 = DocumentFormat.OpenXml.Office2010.PowerPoint;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;

using ShapeCrawler;
using MyCaseLog.Properties;
using System.Text;

namespace MyCaseLog.Controllers
{
    public class PowerPointController
    {
        private IDictionary<string, OpenXmlPart> UriPartDictionary = new Dictionary<string, OpenXmlPart>();
        private IDictionary<string, DataPart> UriNewDataPartDictionary = new Dictionary<string, DataPart>();
        private PresentationDocument document;

        public void InitPPPackageForCollection(string filePath)
        {
            using (document = PresentationDocument.Open(filePath, true))
            {
                int slideCount = CountSlides();
                if (slideCount == 1)
                {
                    ChangeParts();
                }
                else
                { 
                    //need to import another slide
                }
               
            }
        }

        private void ChangeParts()
        {
            //Stores the referrences to all the parts in a dictionary.
            BuildUriPartDictionary();
            //Adds new parts or new relationships.
            AddParts();
            //Changes the contents of the specified parts.
           
            ChangeThumbnailPart1(((ThumbnailPart)UriPartDictionary["/docProps/thumbnail.jpeg"]));
           
            ChangeSlidePart1(((SlidePart)UriPartDictionary["/ppt/slides/slide1.xml"]));
            ChangeNotesSlidePart1(((NotesSlidePart)UriPartDictionary["/ppt/notesSlides/notesSlide1.xml"]));

            ChangeCoreFilePropertiesPart1(((CoreFilePropertiesPart)UriPartDictionary["/docProps/core.xml"]));
            ChangeExtendedFilePropertiesPart1(((ExtendedFilePropertiesPart)UriPartDictionary["/docProps/app.xml"]));
        }

        /// <summary>
        /// Stores the references to all the parts in the package.
        /// They could be retrieved by their URIs later.
        /// </summary>
        private void BuildUriPartDictionary()
        {
            System.Collections.Generic.Queue<OpenXmlPartContainer> queue = new System.Collections.Generic.Queue<OpenXmlPartContainer>();
            queue.Enqueue(document);
            while (queue.Count > 0)
            {
                foreach (var part in queue.Dequeue().Parts)
                {
                    if (!UriPartDictionary.Keys.Contains(part.OpenXmlPart.Uri.ToString()))
                    {
                        UriPartDictionary.Add(part.OpenXmlPart.Uri.ToString(), part.OpenXmlPart);
                        queue.Enqueue(part.OpenXmlPart);
                    }
                }
            }
        }

        /// <summary>
        /// Adds new parts or new relationship between parts.
        /// </summary>
        private void AddParts()
        {
            //Generate new parts.
            ImagePart imagePart1 = UriPartDictionary["/ppt/slides/slide1.xml"].AddNewPart<ImagePart>("image/jpeg", "rId3");
            GenerateImagePart1Content(imagePart1);

        }

        private void GenerateImagePart1Content(ImagePart imagePart1)
        {
            //System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
            //imagePart1.FeedData(data);
            //data.Close();
        }

        private void ChangeCoreFilePropertiesPart1(CoreFilePropertiesPart coreFilePropertiesPart1)
        {
            var package = coreFilePropertiesPart1.OpenXmlPackage;
            package.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime(DateTime.Now.ToString(), System.Xml.XmlDateTimeSerializationMode.Local);
        }

        private void ChangeThumbnailPart1(ThumbnailPart thumbnailPart1)
        {
            //System.IO.Stream data = GetBinaryDataStream(thumbnailPart1Data);
            //thumbnailPart1.FeedData(data);
            //data.Close();
        }

        private void ChangeExtendedFilePropertiesPart1(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = extendedFilePropertiesPart1.Properties;

            Ap.TotalTime totalTime1 = properties1.GetFirstChild<Ap.TotalTime>();
            Ap.Words words1 = properties1.GetFirstChild<Ap.Words>();
            Ap.Paragraphs paragraphs1 = properties1.GetFirstChild<Ap.Paragraphs>();
            Ap.TitlesOfParts titlesOfParts1 = properties1.GetFirstChild<Ap.TitlesOfParts>();
            totalTime1.Text = "10";

            words1.Text = "3";

            paragraphs1.Text = "3";


            Vt.VTVector vTVector1 = titlesOfParts1.GetFirstChild<Vt.VTVector>();

            Vt.VTLPSTR vTLPSTR1 = vTVector1.Elements<Vt.VTLPSTR>().ElementAt(4);
            vTLPSTR1.Text = "Title-goes-here";

        }

        private void ChangeSlidePart1(SlidePart slidePart1)
        {
            Slide slide1 = slidePart1.Slide;

            CommonSlideData commonSlideData1 = slide1.GetFirstChild<CommonSlideData>();

            ShapeTree shapeTree1 = commonSlideData1.GetFirstChild<ShapeTree>();

            Shape shape1 = shapeTree1.GetFirstChild<Shape>();
            Shape shape2 = shapeTree1.Elements<Shape>().ElementAt(1);
            Shape shape3 = shapeTree1.Elements<Shape>().ElementAt(2);

            TextBody textBody1 = shape1.GetFirstChild<TextBody>();

            A.Paragraph paragraph1 = textBody1.GetFirstChild<A.Paragraph>();

            A.EndParagraphRunProperties endParagraphRunProperties1 = paragraph1.GetFirstChild<A.EndParagraphRunProperties>();

            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US", FontSize = 2000, Dirty = false };
            runProperties1.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text1 = new A.Text();
            text1.Text = "Title-goes-here";

            run1.Append(runProperties1);
            run1.Append(text1);
            paragraph1.InsertBefore(run1, endParagraphRunProperties1);

            Picture picture1 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties1 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties1 = new NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Content Placeholder 1" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks1 = new A.PictureLocks() { NoGrouping = true, NoChangeAspect = true };

            nonVisualPictureDrawingProperties1.Append(pictureLocks1);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties1 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape1 = new PlaceholderShape() { Size = PlaceholderSizeValues.Half, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties1.Append(placeholderShape1);

            nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);
            nonVisualPictureProperties1.Append(applicationNonVisualDrawingProperties1);

            BlipFill blipFill1 = new BlipFill();

            A.Blip blip1 = new A.Blip() { Embed = "rId3" };

            A.BlipExtensionList blipExtensionList1 = new A.BlipExtensionList();

            A.BlipExtension blipExtension1 = new A.BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" };

            A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");

            blipExtension1.Append(useLocalDpi1);

            blipExtensionList1.Append(blipExtension1);

            blip1.Append(blipExtensionList1);

            A.Stretch stretch1 = new A.Stretch();
            A.FillRectangle fillRectangle1 = new A.FillRectangle();

            stretch1.Append(fillRectangle1);

            blipFill1.Append(blip1);
            blipFill1.Append(stretch1);

            ShapeProperties shapeProperties1 = new ShapeProperties();

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset1 = new A.Offset() { X = 788468L, Y = 438150L };
            A.Extents extents1 = new A.Extents() { Cx = 4319039L, Cy = 6183313L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            shapeProperties1.Append(transform2D1);

            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties1);
            shapeTree1.InsertBefore(picture1, shape2);

            NonVisualShapeProperties nonVisualShapeProperties1 = shape2.GetFirstChild<NonVisualShapeProperties>();
            ShapeProperties shapeProperties2 = shape2.GetFirstChild<ShapeProperties>();

            NonVisualDrawingProperties nonVisualDrawingProperties2 = nonVisualShapeProperties1.GetFirstChild<NonVisualDrawingProperties>();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties2 = nonVisualShapeProperties1.GetFirstChild<ApplicationNonVisualDrawingProperties>();
            nonVisualDrawingProperties2.Id = (UInt32Value)9U;
            nonVisualDrawingProperties2.Name = "Content Placeholder 8";

            PlaceholderShape placeholderShape2 = applicationNonVisualDrawingProperties2.GetFirstChild<PlaceholderShape>();
            placeholderShape2.Index = (UInt32Value)2U;

            A.Transform2D transform2D2 = shapeProperties2.GetFirstChild<A.Transform2D>();

            A.Offset offset2 = transform2D2.GetFirstChild<A.Offset>();
            A.Extents extents2 = transform2D2.GetFirstChild<A.Extents>();
            offset2.X = 5838825L;
            extents2.Cx = 6210299L;

            shape3.Remove();
        }

        private void ChangeNotesSlidePart1(NotesSlidePart notesSlidePart1)
        {
            NotesSlide notesSlide1 = notesSlidePart1.NotesSlide;

            CommonSlideData commonSlideData1 = notesSlide1.GetFirstChild<CommonSlideData>();

            ShapeTree shapeTree1 = commonSlideData1.GetFirstChild<ShapeTree>();

            Shape shape1 = shapeTree1.Elements<Shape>().ElementAt(1);

            TextBody textBody1 = shape1.GetFirstChild<TextBody>();

            A.Paragraph paragraph1 = textBody1.GetFirstChild<A.Paragraph>();

            A.EndParagraphRunProperties endParagraphRunProperties1 = paragraph1.GetFirstChild<A.EndParagraphRunProperties>();

            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US" };
            runProperties1.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text1 = new A.Text();
            text1.Text = "Note-goes-here";

            run1.Append(runProperties1);
            run1.Append(text1);
            paragraph1.InsertBefore(run1, endParagraphRunProperties1);
        }

        #region Binary Data
        //private string thumbnailPart1Data = "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCACQAQADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9U6KKKAOe1fxBdWPizQ9MiSI2175nmsykuNqMw2nOByozkHrWJZeNvEU1rMsvh2UXAsjcxzeTKiNLsU+VsKkg7iy8kZ2E8ZFd5RQB59ffE/UbBYJJvC11DDNGuJLiRowsuTlGzHxgKee/pjmtK68aapDqkltD4ZvJbZLiSE3R3quFVSJAAhJUksOM/d43ZxXX0UAcFefEDWbKG1hPhm5nvprKO42xiTbvZV3oBszlCeQSOO+SBV2HxhrEt1Gknh24t4gnmyN87kqYGkCrhANwYKhBPU4APUdhRQBxNp431y8ktkPhe5s1la2zJMXYKsjIJAQE4ZAzdcD5ckjoYz48161tGa48JXk00dtHK32YsVd2VSUUbcggnB9CO+CR3VFAHGSeNtYhsnm/4Rq6mmaR1it4xICFWBXG5inUuSnAxwcbscx/8JprzRyTDwzcBY0hb7PhzISyzFxuKgcFI+m44fkZO0dvRQBxvhTx7d+JNSe1l0OaxWN5I5JGl3+WyY4YbRjJ3gc/w+9dlRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFAH/9k=";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

        //  InsertNewSlide(@"..\..\Documents\Myppt2.pptx", 1, "My new slide");

        // Insert a slide into the specified presentation.
        public static void InsertNewSlide(string presentationFile, int position, string slideTitle)
        {
            // Open the source document as read/write. 
            using (PresentationDocument presentationDocument = PresentationDocument.Open(presentationFile, true))
            {
                // Pass the source document and the position and title of the slide to be inserted to the next method.
                InsertNewSlide(presentationDocument, position, slideTitle);
            }
        }

        // Insert the specified slide into the presentation at the specified position.
        public static SlidePart InsertNewSlide(PresentationDocument presentationDocument, int position, string slideTitle)
        {

            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            if (slideTitle == null)
            {
                throw new ArgumentNullException("slideTitle");
            }

            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }

            // Declare and instantiate a new slide.
            Slide slide = new Slide(new CommonSlideData(new ShapeTree()));
            uint drawingObjectId = 1;

            // Construct the slide content.            
            // Specify the non-visual properties of the new slide.
            NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new NonVisualGroupShapeProperties());
            nonVisualProperties.NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = 1, Name = "" };
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties();
            nonVisualProperties.ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties();

            // Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(new GroupShapeProperties());

            // Declare and instantiate the title shape of the new slide.
            Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());

            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties
                (new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Title" },
                new NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Type = PlaceholderValues.Title }));
            titleShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new A.BodyProperties(),
                    new A.ListStyle(),
                    new A.Paragraph(new A.Run(new A.Text() { Text = slideTitle })));

            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());
            drawingObjectId++;

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the body shape.
            bodyShape.TextBody = new TextBody(new A.BodyProperties(),
                    new A.ListStyle(),
                    new A.Paragraph());

            // Create the slide part for the new slide.
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            SlideId prevSlideId = null;

            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Get the ID of the previous slide.
            SlidePart lastSlidePart;

            if (prevSlideId != null)
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(prevSlideId.RelationshipId);
            }
            else
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(((SlideId)(slideIdList.ChildElements[0])).RelationshipId);
            }

            // Use the same slide layout as that of the previous slide.
            if (null != lastSlidePart.SlideLayoutPart)
            {
                slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            }

            // Insert the new slide into the slide list after the previous slide.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);

            // Save the modified presentation.
            presentationPart.Presentation.Save();

            return slidePart;
        }

        public void AddMyCaseToCollection(CaseLogEntry e)
        {
            string pathToPPSlides = EnsurePPSlideCollectionExists(e.BodyPart);

            //using IPresentation presentation = SCPresentation.Open(pathToPPSlides, isEditable: true);

            //ISlide slide = presentation.Slides.Last();

            // Get picture shape
            //IAutoShape autoShape = slide.Shapes.OfType<IAutoShape>().Skip(1).First();//.Shapes.OfType<IPicture>().First();
            //IAutoShape picture2 = slide.Shapes  //.OfType<IPicture>().Skip(1).First();
            //autoShape.Placeholder.
            // Change image
            //picture1.Image.SetImage(e.snaps[0].ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg));
            //picture2.Image.SetImage(e.snaps[1].ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg));


            // Save changes
            //presentation.Save();

            //last slide should be blank?
        }

        private static string EnsurePPSlideCollectionExists(string bodyPart)
        {
            //ensure slideCollection exists
            string pathToSlideCollection = Settings.Default.LogDir + $"\\MCL_{bodyPart}.pptx";
            if (!File.Exists(pathToSlideCollection))
            {
                string pathToPPTemplate = Settings.Default.LogDir + $"\\PPT_Slide1_2pic_Blank.pptx";
                File.Copy(pathToPPTemplate, pathToSlideCollection);

                //string pathToPPBlankSlide = Settings.Default.LogDir + $"\\PPT_Slide1_2pic.pptx";
                //create 1 slide
                //var ppc = new Controllers.PowerPointController();
                //ppc.InitPPPackageForCollection(pathToSlideCollection);
                //Controllers.PowerPointController.AddBlank2PicSlide(pathToPPBlankSlide, pathToSlideCollection);
            }

            return pathToSlideCollection;
        }

        public string RunTEST()
        {
            string pptTESTFile = Settings.Default.LogDir + $"\\MCL_head.pptx";

            string testImgPathLeft = Settings.Default.LogDir + $"\\DxRadHand.jpg";
            string testImgPathRight = Settings.Default.LogDir + $"\\rightimg.jpg";
            byte[] testImgLeft = File.ReadAllBytes(testImgPathLeft);
            byte[] testImgRight = File.ReadAllBytes(testImgPathRight);

            using (document = PresentationDocument.Open(pptTESTFile, true))
            {
                var presentationPart = document.PresentationPart;

                int lastSlideIdx = 0;
                SlidePart lastSlidePart = GetLastSlide(document, out lastSlideIdx);

                int slideCount = document.PresentationPart.SlideParts.Count();
                if (lastSlideIdx == 1)
                {
                    string lastSlideTitle = GetSlideTitle(lastSlidePart);

                    //InsertNewSlide(document, lastSlideIdx + 2, "");

                    /*
                                        
                    var firstTemplateAsBlankTemplate = presentationPart.GetSlidePartsInOrder().First();//.Last();
                    var newSlidePart = templatePart.CloneSlide();

                    presentationPart.AppendSlide(newSlidePart);
                                        // Save the modified presentation.
                                        presentationPart.Presentation.Save();

                                        */


                    //todo-erase title and notes before settting
                    SetSlideTitle(lastSlidePart, "title-from-code! | " + DateTime.Now.ToString());
                    if (lastSlidePart.NotesSlidePart == null)
                    {
                        NotesSlidePart newNotesSlidePart = lastSlidePart.AddNewPart<NotesSlidePart>("rId2");
                        GenerateNotesSlidePartContent(newNotesSlidePart,"injected-note-here", slideCount);
                    }
                    else { SetSlideNotes(lastSlidePart, "notes-showing up here " + DateTime.Now.ToString()); }

                    //AddImage(lastSlidePart, testImgLeft, ImagePartType.Jpeg, false);
                    //AddImage(lastSlidePart, testImgRight, ImagePartType.Jpeg, false);
                    RemoveImage(lastSlidePart, 2);
                    RemoveImage(lastSlidePart, 1);

                    presentationPart.Presentation.Save();

                    if (lastSlideTitle == "")
                    {
                        //use it to fill data
                    }
                    else
                    { 
                        
                    }
                }                    
                
                //List<string> txt = GetAllTextInSlide(lastSlidePart);
            }

            return "X";
        }

        public static void AddImage(SlidePart slidePart, byte[] imgAsByteArray, ImagePartType imgType, bool isRightSidePic)
        {

            var imgPart = slidePart.AddImagePart(imgType);

            using (MemoryStream stream = new MemoryStream(imgAsByteArray))
            {
                imgPart.FeedData(stream);
            }

            var tree = slidePart
                    .Slide
                    .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                    .First();

            //Shape shape2 = tree.Elements<Shape>().ElementAt(1);//getplaceholder?

            int picChildIDinTree = tree.ChildElements.Count - 1;
            Picture picture = new DocumentFormat.OpenXml.Presentation.Picture();

            picture.NonVisualPictureProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureProperties();
            picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties
            {
                Name = "Pic-" + picChildIDinTree,
                Id = (UInt32)picChildIDinTree
            });

            var nonVisualPictureDrawingProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureDrawingProperties();
            nonVisualPictureDrawingProperties.Append(new DocumentFormat.OpenXml.Drawing.PictureLocks()
            {
                NoChangeAspect = true
            });
            picture.NonVisualPictureProperties.Append(nonVisualPictureDrawingProperties);
            picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.ApplicationNonVisualDrawingProperties());

            var blipFill = new DocumentFormat.OpenXml.Presentation.BlipFill();
            var blip1 = new DocumentFormat.OpenXml.Drawing.Blip()
            {
                Embed = slidePart.GetIdOfPart(imgPart)
            };
            var blipExtensionList1 = new DocumentFormat.OpenXml.Drawing.BlipExtensionList();
            var blipExtension1 = new DocumentFormat.OpenXml.Drawing.BlipExtension()
            {
                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
            };
            var useLocalDpi1 = new DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi()
            {
                Val = false
            };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
            blipExtension1.Append(useLocalDpi1);
            blipExtensionList1.Append(blipExtension1);
            blip1.Append(blipExtensionList1);

            var stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
            stretch.Append(new DocumentFormat.OpenXml.Drawing.FillRectangle());
            blipFill.Append(blip1);
            blipFill.Append(stretch);
            picture.Append(blipFill);

            picture.ShapeProperties = new DocumentFormat.OpenXml.Presentation.ShapeProperties();
            picture.ShapeProperties.Transform2D = new DocumentFormat.OpenXml.Drawing.Transform2D();
            //left side
            if (isRightSidePic)
            {
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {
                    X = 6657974L,
                    Y = 435266L
                });
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
                {
                    Cx = 4486275L,
                    Cy = 6422734L
                });
            }
            else
            {
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {
                    X = 788468L,
                    Y = 438150L
                });
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
                {
                    Cx = 4319039L,
                    Cy = 6183313L
                });

            }


            picture.ShapeProperties.Append(new DocumentFormat.OpenXml.Drawing.PresetGeometry
            {
                Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
            });

            tree.Append(picture);
            //tree.InsertBefore(picture, shape2);
        }

        public static void RemoveImage(SlidePart slidePart, int imgNbr)
        {
            var tree = slidePart
                   .Slide
                   .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                   .First();

            //Picture picture1 = tree.GetFirstChild<Picture>();
            Picture picture1 = tree.Elements<Picture>().ElementAt(imgNbr-1);

            //NonVisualPictureProperties nonVisualPictureProperties1 = picture1.GetFirstChild<NonVisualPictureProperties>();
            BlipFill blipFill1 = picture1.GetFirstChild<BlipFill>();
            //ShapeProperties shapeProperties1 = picture1.GetFirstChild<ShapeProperties>();

            //NonVisualDrawingProperties nonVisualDrawingProperties1 = nonVisualPictureProperties1.GetFirstChild<NonVisualDrawingProperties>();
            //nonVisualDrawingProperties1.Id = (UInt32Value)3U;
            //nonVisualDrawingProperties1.Name = "Pic-3";

            A.Blip blip1 = blipFill1.GetFirstChild<A.Blip>();
            string imgPartRelID = blip1.Embed.Value;
            slidePart.DeletePart(imgPartRelID);
            
            blip1.Remove();
            blipFill1.Remove();

            /*A.Blip blip2 = new A.Blip() { Embed = "rId3", CompressionState = A.BlipCompressionValues.Print };

            A.BlipExtensionList blipExtensionList1 = new A.BlipExtensionList();

            A.BlipExtension blipExtension1 = new A.BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" };

            A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");

            blipExtension1.Append(useLocalDpi1);

            blipExtensionList1.Append(blipExtension1);

            blip2.Append(blipExtensionList1);
            blipFill1.InsertBefore(blip2, blip1);

            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();
            presetGeometry1.Append(adjustValueList1);
            */
            picture1.Remove();

        }

        private void SetSlideTitle(SlidePart slidePart, string title)
        {
            Slide slide = slidePart.Slide;

            CommonSlideData commonSlideData1 = slide.GetFirstChild<CommonSlideData>();

            ShapeTree shapeTree1 = commonSlideData1.GetFirstChild<ShapeTree>();

            Shape shape1 = shapeTree1.GetFirstChild<Shape>();
            //Shape shape2 = shapeTree1.Elements<Shape>().ElementAt(1);
            //Shape shape3 = shapeTree1.Elements<Shape>().ElementAt(2);

            TextBody textBody1 = shape1.GetFirstChild<TextBody>();

            A.Paragraph paragraph1 = textBody1.GetFirstChild<A.Paragraph>();
            A.EndParagraphRunProperties endParagraphRunProperties1 = paragraph1.GetFirstChild<A.EndParagraphRunProperties>();

            A.Run run1 = paragraph1.GetFirstChild<A.Run>();
            if (run1!=null)
                run1.Remove();

            run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US", FontSize = 2000, Dirty = false };
            runProperties1.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text1 = new A.Text();
            text1.Text = title;

            run1.Append(runProperties1);
            run1.Append(text1);
            paragraph1.InsertBefore(run1, endParagraphRunProperties1);

        }

        private void SetSlideNotes (SlidePart slidePart, string noteToSet)
        {
            NotesSlidePart notesSlidePart1 = slidePart.NotesSlidePart;

            NotesSlide notesSlide1 = notesSlidePart1.NotesSlide;

            CommonSlideData commonSlideData1 = notesSlide1.GetFirstChild<CommonSlideData>();

            ShapeTree shapeTree1 = commonSlideData1.GetFirstChild<ShapeTree>();

            Shape shape1 = shapeTree1.Elements<Shape>().ElementAt(1);

            TextBody textBody1 = shape1.GetFirstChild<TextBody>();

            A.Paragraph paragraph1 = textBody1.GetFirstChild<A.Paragraph>();

            A.EndParagraphRunProperties endParagraphRunProperties1 = paragraph1.GetFirstChild<A.EndParagraphRunProperties>();

            A.Run run1 = paragraph1.GetFirstChild<A.Run>();
            if (run1 != null)
                run1.Remove();

            run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US" };
            runProperties1.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text1 = new A.Text();
            text1.Text = noteToSet;

            run1.Append(runProperties1);
            run1.Append(text1);
            paragraph1.InsertBefore(run1, endParagraphRunProperties1);
        }

        private void GenerateNotesSlidePartContent(NotesSlidePart notesSlidePart1, string slideNotes, int slideNum)
        {
            NotesSlide notesSlide1 = new NotesSlide();
            notesSlide1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            notesSlide1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            notesSlide1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData1 = new CommonSlideData();

            ShapeTree shapeTree1 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties1 = new NonVisualDrawingProperties() { Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties1 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties1 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties1.Append(nonVisualDrawingProperties1);
            nonVisualGroupShapeProperties1.Append(nonVisualGroupShapeDrawingProperties1);
            nonVisualGroupShapeProperties1.Append(applicationNonVisualDrawingProperties1);

            GroupShapeProperties groupShapeProperties1 = new GroupShapeProperties();

            A.TransformGroup transformGroup1 = new A.TransformGroup();
            A.Offset offset1 = new A.Offset() { X = 0L, Y = 0L };
            A.Extents extents1 = new A.Extents() { Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset1 = new A.ChildOffset() { X = 0L, Y = 0L };
            A.ChildExtents childExtents1 = new A.ChildExtents() { Cx = 0L, Cy = 0L };

            transformGroup1.Append(offset1);
            transformGroup1.Append(extents1);
            transformGroup1.Append(childOffset1);
            transformGroup1.Append(childExtents1);

            groupShapeProperties1.Append(transformGroup1);

            Shape shape1 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties1 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties2 = new NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Slide Image Placeholder 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties1 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks1 = new A.ShapeLocks() { NoGrouping = true, NoRotation = true, NoChangeAspect = true };

            nonVisualShapeDrawingProperties1.Append(shapeLocks1);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties2 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape1 = new PlaceholderShape() { Type = PlaceholderValues.SlideImage };

            applicationNonVisualDrawingProperties2.Append(placeholderShape1);

            nonVisualShapeProperties1.Append(nonVisualDrawingProperties2);
            nonVisualShapeProperties1.Append(nonVisualShapeDrawingProperties1);
            nonVisualShapeProperties1.Append(applicationNonVisualDrawingProperties2);
            ShapeProperties shapeProperties1 = new ShapeProperties();

            shape1.Append(nonVisualShapeProperties1);
            shape1.Append(shapeProperties1);

            Shape shape2 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties2 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties3 = new NonVisualDrawingProperties() { Id = (UInt32Value)3U, Name = "Notes Placeholder 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties2 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks2 = new A.ShapeLocks() { NoGrouping = true };

            nonVisualShapeDrawingProperties2.Append(shapeLocks2);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties3 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape2 = new PlaceholderShape() { Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties3.Append(placeholderShape2);

            nonVisualShapeProperties2.Append(nonVisualDrawingProperties3);
            nonVisualShapeProperties2.Append(nonVisualShapeDrawingProperties2);
            nonVisualShapeProperties2.Append(applicationNonVisualDrawingProperties3);
            ShapeProperties shapeProperties2 = new ShapeProperties();

            TextBody textBody1 = new TextBody();
            A.BodyProperties bodyProperties1 = new A.BodyProperties();
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.Run run1 = new A.Run();

            A.RunProperties runProperties1 = new A.RunProperties() { Language = "en-US" };
            runProperties1.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text1 = new A.Text();
            text1.Text = slideNotes;

            run1.Append(runProperties1);
            run1.Append(text1);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph1.Append(run1);
            paragraph1.Append(endParagraphRunProperties1);

            textBody1.Append(bodyProperties1);
            textBody1.Append(listStyle1);
            textBody1.Append(paragraph1);

            shape2.Append(nonVisualShapeProperties2);
            shape2.Append(shapeProperties2);
            shape2.Append(textBody1);

            Shape shape3 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties3 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties4 = new NonVisualDrawingProperties() { Id = (UInt32Value)4U, Name = "Slide Number Placeholder 3" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties3 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks3 = new A.ShapeLocks() { NoGrouping = true };

            nonVisualShapeDrawingProperties3.Append(shapeLocks3);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties4 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape3 = new PlaceholderShape() { Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)10U };

            applicationNonVisualDrawingProperties4.Append(placeholderShape3);

            nonVisualShapeProperties3.Append(nonVisualDrawingProperties4);
            nonVisualShapeProperties3.Append(nonVisualShapeDrawingProperties3);
            nonVisualShapeProperties3.Append(applicationNonVisualDrawingProperties4);
            ShapeProperties shapeProperties3 = new ShapeProperties();

            TextBody textBody2 = new TextBody();
            A.BodyProperties bodyProperties2 = new A.BodyProperties();
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();

            A.Field field1 = new A.Field() { Id = "{B00C64AE-9EED-49CA-8F9B-C87CF13728B7}", Type = "slidenum" };

            A.RunProperties runProperties2 = new A.RunProperties() { Language = "en-US" };
            runProperties2.SetAttribute(new OpenXmlAttribute("", "smtClean", "", "0"));
            A.Text text2 = new A.Text();
            text2.Text = slideNum.ToString();

            field1.Append(runProperties2);
            field1.Append(text2);
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties() { Language = "en-US" };

            paragraph2.Append(field1);
            paragraph2.Append(endParagraphRunProperties2);

            textBody2.Append(bodyProperties2);
            textBody2.Append(listStyle2);
            textBody2.Append(paragraph2);

            shape3.Append(nonVisualShapeProperties3);
            shape3.Append(shapeProperties3);
            shape3.Append(textBody2);

            shapeTree1.Append(nonVisualGroupShapeProperties1);
            shapeTree1.Append(groupShapeProperties1);
            shapeTree1.Append(shape1);
            shapeTree1.Append(shape2);
            shapeTree1.Append(shape3);

            CommonSlideDataExtensionList commonSlideDataExtensionList1 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension1 = new CommonSlideDataExtension() { Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId1 = new P14.CreationId() { Val = (UInt32Value)2615747055U };
            creationId1.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension1.Append(creationId1);

            commonSlideDataExtensionList1.Append(commonSlideDataExtension1);

            commonSlideData1.Append(shapeTree1);
            commonSlideData1.Append(commonSlideDataExtensionList1);

            ColorMapOverride colorMapOverride1 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping1 = new A.MasterColorMapping();

            colorMapOverride1.Append(masterColorMapping1);

            notesSlide1.Append(commonSlideData1);
            notesSlide1.Append(colorMapOverride1);

            notesSlidePart1.NotesSlide = notesSlide1;
        }



        // Get the slide part of the first slide in the presentation document.
        public static SlidePart GetLastSlide(PresentationDocument presentationDocument, out int indexOfLastSlide)
        {
            // Get relationship ID of the first slide
            PresentationPart presentationPart = presentationDocument.PresentationPart;
            //int slideCount= presentationPart.SlideParts.Count();    
            Presentation presentation = presentationPart.Presentation;

            // Get the collection of slide IDs from the slide ID list.
            var slideIds = presentation.SlideIdList.ChildElements;
            indexOfLastSlide = slideIds.Count - 1;

            // Get the relationship ID of the slide.
            string slidePartRelationshipId = (slideIds[indexOfLastSlide] as SlideId).RelationshipId;

            // Get the specified slide part from the relationship ID.
            SlidePart slidePart = (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);
            
            return slidePart;
        }


        public static string[] GetAllTextInSlide(PresentationDocument presentationDocument, int slideIndex)
        {
            // Verify that the presentation document exists.
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            // Verify that the slide index is not out of range.
            if (slideIndex < 0)
            {
                throw new ArgumentOutOfRangeException("slideIndex");
            }

            // Get the presentation part of the presentation document.
            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation part and presentation exist.
            if (presentationPart != null && presentationPart.Presentation != null)
            {
                // Get the Presentation object from the presentation part.
                Presentation presentation = presentationPart.Presentation;

                // Verify that the slide ID list exists.
                if (presentation.SlideIdList != null)
                {
                    // Get the collection of slide IDs from the slide ID list.
                    var slideIds = presentation.SlideIdList.ChildElements;

                    // If the slide ID is in range...
                    if (slideIndex < slideIds.Count)
                    {
                        // Get the relationship ID of the slide.
                        string slidePartRelationshipId = (slideIds[slideIndex] as SlideId).RelationshipId;

                        // Get the specified slide part from the relationship ID.
                        SlidePart slidePart = (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);

                        // Pass the slide part to the next method, and
                        // then return the array of strings that method
                        // returns to the previous method.
                        return GetAllTextInSlide(slidePart).ToArray();
                    }
                }
            }
            // Else, return null.
            return null;
        }

        public static List<string> GetAllTextInSlide(SlidePart slidePart)
        {
            List<string> texts = new List<string>();
            // Iterate through all the paragraphs in the slide.
            foreach (A.Paragraph paragraph in  slidePart.Slide.Descendants<A.Paragraph>())
            {
                // Create a new string builder.                    
                StringBuilder paragraphText = new StringBuilder();

                //int txts = paragraph.Descendants<A.Text>().Count();

                // Iterate through the lines of the paragraph.
                foreach (A.Text text in paragraph.Descendants<A.Text>())
                {
                    // Append each line to the previous lines.
                    paragraphText.Append(text.Text);
                }

                if (paragraphText.Length > 0)
                {
                    // Add each paragraph to the linked list.
                    texts.Add(paragraphText.ToString());
                }
            }

            return texts;

        }

        public static string GetSlideTitle(SlidePart slidePart)
        {
            // Find all the title shapes.
            var shapes = from shape in slidePart.Slide.Descendants<Shape>()
                         where IsTitleShape(shape)
                         select shape;

            StringBuilder paragraphText = new StringBuilder();

            foreach (var shape in shapes)
            {
                // Get the text in each paragraph in this shape.
                foreach (var paragraph in shape.TextBody.Descendants<A.Paragraph>())
                {
                    // Add a line break.
                    //paragraphText.Append(paragraphSeparator);

                    foreach (var text in paragraph.Descendants<A.Text>())
                    {
                        paragraphText.Append(text.Text);
                    }

                    //paragraphSeparator = "\n";
                }
            }

            return paragraphText.ToString().Trim();
        }

        // Determines whether the shape is a title shape.
        private static bool IsTitleShape(Shape shape)
        {
            var placeholderShape = shape.NonVisualShapeProperties.ApplicationNonVisualDrawingProperties.GetFirstChild<PlaceholderShape>();
            if (placeholderShape != null && placeholderShape.Type != null && placeholderShape.Type.HasValue)
            {
                switch ((PlaceholderValues)placeholderShape.Type)
                {
                    // Any title shape.
                    case PlaceholderValues.Title:

                    // A centered title.
                    case PlaceholderValues.CenteredTitle:
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }

        public static int GetNextBlankSlideIndex(string pathToPPT)
        {
            int nextBlankSlide = 1;
            string pathToSrcPPT = Settings.Default.LogDir + $"\\PPT_Slide1_2pic_Blank.pptx";
            
            using IPresentation presentation = SCPresentation.Open(pathToPPT, true);
            nextBlankSlide = presentation.Slides.Count();
            ISlide removingSlide = presentation.Slides.First();

            return nextBlankSlide;
        }
        public static void AddBlank2PicSlide(string pathToSrcPPT, string pathToDestPPT)
        {
            //using IPresentation presentation = SCPresentation.Open(pathToDestPPT, true);
            //ISlide removingSlide = presentation.Slides.First();
            //presentation.Slides.Remove(removingSlide);

            //// Move second slide to first position
            //presentation.Slides[1].Number = 2;

            // Copy second slide from source into dest
            using IPresentation source = SCPresentation.Open(pathToSrcPPT, true);
            using IPresentation dest = SCPresentation.Open(pathToDestPPT, true);
            
            ISlide copyingSlide = source.Slides[0];
            dest.Slides.Add(copyingSlide);

            // Save changes
            dest.Save();
        }

        // Count the slides in the presentation.
        public int CountSlides()
        {
            // Check for a null document object.
            if (document == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            int slidesCount = 0;

            // Get the presentation part of document.
            PresentationPart presentationPart = document.PresentationPart;

            // Get the slide count from the SlideParts.
            if (presentationPart != null)
            {
                slidesCount = presentationPart.SlideParts.Count();
            }

            // Return the slide count to the previous method.
            return slidesCount;
        }

        //public static void AddPictureToSlide()
        //{

        //    using IPresentation presentation = SCPresentation.Open("helloWorld.pptx", isEditable: true);

        //    // Get picture shape
        //    IPicture picture = presentation.Slides[0].Shapes.OfType<IPicture>().First();

        //    // Change image
        //    picture.Image.SetImage("new-image.png");

        //    // Save changes
        //    presentation.Save();

        //}
    }
   
}

public static class OpenXmlUtils
{
    public static IEnumerable<SlidePart> GetSlidePartsInOrder(this PresentationPart presentationPart)
    {
        SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

        return slideIdList.ChildElements
            .Cast<SlideId>()
            .Select(x => presentationPart.GetPartById(x.RelationshipId))
            .Cast<SlidePart>();
    }

    public static SlidePart CloneSlide(this SlidePart templatePart)
    {
        // find the presentationPart: makes the API more fluent
        var presentationPart = templatePart.GetParentParts()
            .OfType<PresentationPart>()
            .Single();

        // clone slide contents
        Slide currentSlide = (Slide)templatePart.Slide.CloneNode(true);
        var slidePartClone = presentationPart.AddNewPart<SlidePart>();
        currentSlide.Save(slidePartClone);

        // copy layout part
        slidePartClone.AddPart(templatePart.SlideLayoutPart);

        return slidePartClone;
    }

    public static void AppendSlide(this PresentationPart presentationPart, SlidePart newSlidePart)
    {
        SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

        // find the highest id
        uint maxSlideId = slideIdList.ChildElements
            .Cast<SlideId>()
            .Max(x => x.Id.Value);

        // Insert the new slide into the slide list after the previous slide.
        var id = maxSlideId + 1;

        SlideId newSlideId = new SlideId();
        slideIdList.Append(newSlideId);
        newSlideId.Id = id;
        newSlideId.RelationshipId = presentationPart.GetIdOfPart(newSlidePart);
    }
}