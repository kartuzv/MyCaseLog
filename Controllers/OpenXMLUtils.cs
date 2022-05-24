using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaseLog.Controllers
{

    /*
     
     var presentationFile = "SomeFile.pptx";
        using (PresentationDocument presentationDocument  = PresentationDocument.Open(presentationFile, true))
        {
        var presentationPart = presentationDocument.PresentationPart;
        var templatePart = presentationPart.GetSlidePartsInOrder().Last();
 
        var newSlidePart = templatePart.CloneSlide();
 
        presentationPart.AppendSlide(newSlidePart);
        }

     */

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
}
