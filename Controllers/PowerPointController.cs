using System;
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

//using ShapeCrawler;
//using MyCaseLog.Properties;
using System.Text;
using System.Drawing;


namespace MyCaseLog.Controllers
{
    public class PowerPointController
    {
        private IDictionary<string, OpenXmlPart> UriPartDictionary = new Dictionary<string, OpenXmlPart>();
        private IDictionary<string, DataPart> UriNewDataPartDictionary = new Dictionary<string, DataPart>();
        private PresentationDocument document;
        string pathToPPTemplate = "";//Settings.Default.LogDir + $"PPTemplate2Pic.pptx";
        string logPath = "";
        //SlidePart blankTemplateSlidePart = null;
        //private PresentationDocument _templateDoc;
        List<Bitmap> imgs = new List<Bitmap>();
        public PowerPointController(string pathToLogFolder, string pptTemplatePath)
        {

            pathToPPTemplate = pptTemplatePath;
            logPath = pathToLogFolder;

        }
      
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

        public void GenerateCasePowerPoint(CaseLogEntry e)
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string pathToPPSlides = logPath + $"\\MCL_{e.LogTSID}.pptx";
			File.Copy(pathToPPTemplate, pathToPPSlides, true);

            imgs = new List<Bitmap>();
			Bitmap bmp;
			foreach (var jpgPath in e.SnapPaths)
			{
				bmp = new Bitmap(jpgPath);
				imgs.Add(bmp);
			}

			using (document = PresentationDocument.Open(pathToPPSlides, true))
			{
				var presentationPart = document.PresentationPart;

				int lastSlideIdx = 0;
				//SlidePart lastSlidePart = GetLastSlide(document, out lastSlideIdx);
				SlidePart firstSlideAsTemplate = presentationPart.GetSlidePartsInOrder().First();
				SlidePart targetSlide = firstSlideAsTemplate;
               
				int slidesTotal = (imgs.Count % 2) == 1 ? (imgs.Count + 1) / 2 : imgs.Count / 2;
				if (imgs.Count == 0)//special case - blank slide, no images.
					slidesTotal = 1;

				AddCaseSlideToPresentation(presentationPart, firstSlideAsTemplate, e, 1, slidesTotal, targetSlide);

			}

		}
        public void AddMyCaseToCollection(CaseLogEntry e)
        {
            string pathToPPSlides = EnsurePPSlideCollectionExists(e.Specialty);

			//LoadTemplateSlidePart();//load template into memory
			imgs = new List<Bitmap>();

			using (document = PresentationDocument.Open(pathToPPSlides, true))
            {
                var presentationPart = document.PresentationPart;

                int lastSlideIdx = 0;
                //SlidePart lastSlidePart = GetLastSlide(document, out lastSlideIdx);
                SlidePart firstSlideAsTemplate = presentationPart.GetSlidePartsInOrder().First();
                SlidePart targetSlide = null;
                int slideCount = document.PresentationPart.SlideParts.Count();
                if (slideCount == 1)
                {
                    //has 'template' already been used? it title one?
                    string lastSlideTitle = GetSlideTitle(firstSlideAsTemplate);
                    if (lastSlideTitle == "")
                    {
                        //not used yet template slide 1st is our 1st target
                        targetSlide = firstSlideAsTemplate;
                    }
                }

                int slidesTotal = (e.SnapPaths.Count % 2)==1? (e.SnapPaths.Count+1)/2: e.SnapPaths.Count/2;
                if (e.SnapPaths.Count == 0)//special case - blank slide, no images.
                    slidesTotal = 1;

                AddCaseSlideToPresentation(presentationPart, firstSlideAsTemplate, e, 1, slidesTotal,targetSlide);

            }

            //_templateDoc.Dispose();
        }

        private void AddCaseSlideToPresentation(PresentationPart p, SlidePart templateSlide, CaseLogEntry e, int caseSlideNbr=1,int caseSlidesTotal=1, SlidePart targetSlide=null)
        {
            int slideCount = p.SlideParts.Count();

            if (targetSlide == null)
            {
                targetSlide = templateSlide.CloneSlide();
                p.AppendSlide(targetSlide);
                p.Presentation.Save();

                //targetSlide = p.AddBlankSlideFromTemplate(pathToPPTemplate);
                
                //targetSlide = AddNewSlideFromTemplate(p);
            }
            
            string slideTitle = $"[{caseSlideNbr}/{caseSlidesTotal}] {e.Specialty}|{e.BodyPart}|{e.PTIdType}:{e.PTMRN}|{DateTime.Now}";
            string slideNotes = $"{e.Notes}{Environment.NewLine}[TAGS]: {e.Tags}";
            
            SetSlideTitle(targetSlide, slideTitle);

            if (targetSlide.NotesSlidePart == null)
            {
                NotesSlidePart newNotesSlidePart = targetSlide.AddNewPart<NotesSlidePart>("rId2");
                GenerateNotesSlidePartContent(newNotesSlidePart, slideNotes, slideCount);
            }
            else { SetSlideNotes(targetSlide, slideNotes); }

            //left
            //long imgEmuWidthIsCx = e.snaps.ElementAt(0).Width * 9525;
            
            //SlideSize slideSize = p.Presentation.GetFirstChild<SlideSize>();
            //int maxHeight = slideSize.Cy;

            if (imgs.Count > 0)
            {
                AddImage(targetSlide, imgs.ElementAt(0), false);
				imgs.RemoveAt(0);
            }
            //right
            if (imgs.Count > 0)
            {
                AddImage(targetSlide, imgs.ElementAt(0), true);
				imgs.RemoveAt(0);
            }
            p.Presentation.Save();

            if (imgs.Count > 0)
                AddCaseSlideToPresentation(p, templateSlide, e, caseSlideNbr + 1,caseSlidesTotal);

            //AddImage(lastSlidePart, testImgLeft, ImagePartType.Jpeg, false);
            //AddImage(lastSlidePart, testImgRight, ImagePartType.Jpeg, false);
            //RemoveImage(lastSlidePart, 2);
            //RemoveImage(lastSlidePart, 1);


        }

        //private SlidePart AddNewSlideFromTemplate(PresentationPart p)
        //{
        //    SlidePart newSlidePartClode = p.AddNewPart<SlidePart>();

        //    Slide clonedSlideContentFromTemplate = (Slide)blankTemplateSlidePart.Slide.CloneNode(true);
        //    clonedSlideContentFromTemplate.Save(newSlidePartClode);
        //    newSlidePartClode.AddPart(blankTemplateSlidePart.SlideLayoutPart);

        //    p.AppendSlide(newSlidePartClode);
        //    p.Presentation.Save();

        //    return newSlidePartClode;
        //}

        private string EnsurePPSlideCollectionExists(string specialty)
        {
            //ensure slideCollection exists
            string pathToSlideCollection = logPath + $"MCL_{specialty}.pptx";
            if (!File.Exists(pathToSlideCollection))
            {
                File.Copy(pathToPPTemplate, pathToSlideCollection);

                //string pathToPPBlankSlide = Settings.Default.LogDir + $"\\PPT_Slide1_2pic.pptx";
                //create 1 slide
                //var ppc = new Controllers.PowerPointController();
                //ppc.InitPPPackageForCollection(pathToSlideCollection);
                //Controllers.PowerPointController.AddBlank2PicSlide(pathToPPBlankSlide, pathToSlideCollection);
            }

            return pathToSlideCollection;
        }

        //private void LoadTemplateSlidePart()
        //{
        //    _templateDoc = PresentationDocument.Open(pathToPPTemplate, false);

        //    blankTemplateSlidePart = _templateDoc.PresentationPart.GetSlidePartsInOrder().First();

        //}

        public string RunTEST()
        {
            string pptTESTFile = logPath + $"\\MCL_head.pptx";

            string testImgPathLeft = logPath + $"\\DxRadHand.jpg";
            string testImgPathRight = logPath + $"\\rightimg.jpg";
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

        public static void AddImage(SlidePart slidePart, System.Drawing.Bitmap img , bool isRightSidePic)
        {
            int emuPerPixel = 9525;
            Size orgSize = img.Size;
            Size resizedImage = CalculateDimensions(orgSize, 680);//max height of 1-half is 680 for WIDE-Screen SLIIDE
            long imageWidthEMU = (long)(resizedImage.Width * emuPerPixel);
            long imageHeightEMU = (long)(resizedImage.Height * emuPerPixel);
            //e.Cx = imageWidthEMU; e.Cy = imageHeightEMU;

            var imgPart = slidePart.AddImagePart(ImagePartType.Jpeg);

            using (MemoryStream stream = new MemoryStream(img.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg)))
            {
                imgPart.FeedData(stream);
            }

            var tree = slidePart
                    .Slide
                    .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                    .First();

            if (isRightSidePic)//remove last shape (placeholder)
            {
                //last appended shape
                tree.ElementAt(tree.ChildElements.Count - 1).Remove();
            }

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
            picture.ShapeProperties.Append(new DocumentFormat.OpenXml.Drawing.PresetGeometry
            {
                Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
            });

            picture.ShapeProperties.Transform2D = new DocumentFormat.OpenXml.Drawing.Transform2D();

            if (isRightSidePic)
            {
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {
                    X = 6099349L,
                    Y = 351693L
                });//640left 36px top
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
				{
                    Cx = imageWidthEMU,
                    Cy = imageHeightEMU
                });

                //share1 at 0 is slide title box, then pic-left, then shape(placeholder added before) as shape2 
                //Shape shape2 = tree.Elements<Shape>().ElementAt(1);
                //shape2.Remove();
            }
            else //LEFT Picture (top-left, first half, + righ placehoolder)
            {
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {                    
                    X = 0L,
                    Y = 351694L 
                });//0left 36px top

				picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
				{
                    Cx = imageWidthEMU,
                    Cy = imageHeightEMU
                });

			}

            tree.Append(picture);

            //FOR LEFT pic need to add placeholder
            if (!isRightSidePic)
            {
                Shape shape1 = new Shape();

                NonVisualShapeProperties nonVisualShapeProperties1 = new NonVisualShapeProperties();
                NonVisualDrawingProperties nonVisualDrawingProperties1 = new NonVisualDrawingProperties() { Id = (UInt32Value)20U, Name = "Right-side Placeholder" };

                NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties1 = new NonVisualShapeDrawingProperties();
                A.ShapeLocks shapeLocks1 = new A.ShapeLocks() { NoGrouping = true };

                nonVisualShapeDrawingProperties1.Append(shapeLocks1);

                ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties1 = new ApplicationNonVisualDrawingProperties();
                PlaceholderShape placeholderShape1 = new PlaceholderShape() { Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

                applicationNonVisualDrawingProperties1.Append(placeholderShape1);

                nonVisualShapeProperties1.Append(nonVisualDrawingProperties1);
                nonVisualShapeProperties1.Append(nonVisualShapeDrawingProperties1);
                nonVisualShapeProperties1.Append(applicationNonVisualDrawingProperties1);

                ShapeProperties shapeProperties2 = new ShapeProperties();

                A.Transform2D transform2D2 = new A.Transform2D();
                A.Offset offset2 = new A.Offset() { X = 6144426L, Y = 351692L };
                A.Extents extents2 = new A.Extents() { Cx = 5973510L, Cy = 6506307L };

                transform2D2.Append(offset2);
                transform2D2.Append(extents2);

                shapeProperties2.Append(transform2D2);

                TextBody textBody1 = new TextBody();
                A.BodyProperties bodyProperties1 = new A.BodyProperties();
                A.ListStyle listStyle1 = new A.ListStyle();

                A.Paragraph paragraph1 = new A.Paragraph();
                A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties() { Language = "en-US", Dirty = false };

                paragraph1.Append(endParagraphRunProperties1);

                textBody1.Append(bodyProperties1);
                textBody1.Append(listStyle1);
                textBody1.Append(paragraph1);

                shape1.Append(nonVisualShapeProperties1);
                shape1.Append(shapeProperties2);
                shape1.Append(textBody1);
                tree.Append(shape1);

            }
           
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

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.Background1 };
            solidFill1.Append(schemeColor1);
            runProperties1.Append(solidFill1);

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

        public string GetSlideNotes(SlidePart slidePart)
        {
            string notes = "";
            ShapeTree tree = slidePart.NotesSlidePart.NotesSlide.Descendants<ShapeTree>().First();
            notes = tree.ChildElements.ElementAt(3).InnerText;// tree.Descendants<A.Run>().First().Text.Text;
            return notes;
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

        public List<string> GetAllCaseTitles(string specialty, bool removeSpecialtyFromTitle=true)
        {
            List<string> titles = new List<string>();

            string pathToSlideCollection = logPath + $"MCL_{specialty}.pptx";
            if (!File.Exists(pathToSlideCollection))
                return titles;

            using (document = PresentationDocument.Open(pathToSlideCollection, false))
            {
                var lstSlideParts = document.PresentationPart.GetSlidePartsInOrder();
                int caseIdx = 1;
                foreach (SlidePart sp in lstSlideParts)
                {
                    string slideTitle = GetSlideTitle(sp);
                    if (slideTitle.StartsWith("[1/"))
                    {
                        string slideNotes = GetSlideNotes(sp);

                        string notes = slideNotes.Substring(0, slideNotes.IndexOf("[")-1);
                        string tags = slideNotes.Substring(slideNotes.IndexOf("["));
                        string formattedTitle = slideTitle.Substring(slideTitle.IndexOf("|") + 1);
                        //less frequent scenario
                        if (!removeSpecialtyFromTitle)
                            formattedTitle = slideTitle.Substring(slideTitle.IndexOf("]") + 2);

                        titles.Add($"{caseIdx}|{formattedTitle}|{tags}|{notes}");
                        caseIdx++;
                    }
                        
                } 
            }

            return titles;
        }


        // Determines whether the shape is a title shape.
        private static bool IsTitleShape(Shape shape)
        {
            var placeholderShape = shape.NonVisualShapeProperties.ApplicationNonVisualDrawingProperties.GetFirstChild<PlaceholderShape>();
            if (placeholderShape != null && placeholderShape.Type != null && placeholderShape.Type.HasValue)
            {
                var phHolderShapeType = ((PlaceholderValues)placeholderShape.Type);
                // Any title shape.// A centered title.
                return (phHolderShapeType == PlaceholderValues.Title || phHolderShapeType == PlaceholderValues.CenteredTitle);
            }
            return false;
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

        public static Size CalculateDimensions(Size oldSize, int maxDimention)
        {           
            if (oldSize.Width <= maxDimention && oldSize.Height <= maxDimention)
                return oldSize;

            Size newSize = new Size();
            if (oldSize.Width > oldSize.Height)
            {
                newSize.Width = maxDimention;
                newSize.Height = (int)(oldSize.Height * (float)maxDimention / (float)oldSize.Width);
            }
            else
            {
                newSize.Width = (int)(oldSize.Width * (float)maxDimention / (float)oldSize.Height);
                newSize.Height = maxDimention;
            }
            return newSize;
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
        var tree = slidePartClone
                    .Slide
                    .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                    .First();
        //0,1,2
        for (int i = tree.ChildElements.Count - 1; i > 2; i--)
            tree.ElementAt(i).Remove();

        return slidePartClone;
    }

    public static SlidePart AddBlankSlideFromTemplate(this PresentationPart p, string pathToTemplate)
    {
        SlidePart slidePartClone = null;
        using (PresentationDocument document = PresentationDocument.Open(pathToTemplate, false))
        {
            var presentationPart = document.PresentationPart;

            int lastSlideIdx = 0;
            
            SlidePart templateSlidePart = presentationPart.GetSlidePartsInOrder().First();
            Slide currentSlide = (Slide)templateSlidePart.Slide.CloneNode(true);

            slidePartClone = p.AddNewPart<SlidePart>();
            currentSlide.Save(slidePartClone);

            // copy layout part
            slidePartClone.AddPart(templateSlidePart.SlideLayoutPart);
        }
        if (slidePartClone != null)
        {
            p.AppendSlide(slidePartClone);
            // Save the modified presentation.
            p.Presentation.Save();
        }

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