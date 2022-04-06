using System.Drawing;

namespace MyCaseLog
{
	public class CaseLogEntry
	{
		public string LogTSID { get; set; }
		public string Modality { get; set; }

		public string BodyPart { get; set; }
		public string Protocol { get; set; }
		public string PTSex { get; set; }
		public string PTAge { get; set; }
		public string PTMRN { get; set; }
		public string StudyAcc { get; set; }
		public string StudyDesc { get; set; }

		public bool IsPublished { get; set; }

		public bool IsLocalConference { get; set; }
		public bool IsSocietyConference { get; set; }
		public string LogStudyPath { get; set; }
		public string BillAmount { get; set; }
		
		public string Hosp { get; set; }

		public string Dx { get; set; }

		public System.Collections.Generic.List<Bitmap> snaps = new System.Collections.Generic.List<Bitmap>();
	}
}
