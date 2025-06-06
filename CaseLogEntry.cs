
using System.Collections.Generic;


namespace MyCaseLog
{
	public class CaseLogEntry
	{
		public int IDX { get; set; }
		public string LogTSID { get; set; }
		public string Modality { get; set; }
		public string Specialty { get; set; }
		public string BodyPart { get; set; }
		public string Protocol { get; set; }
		public string PTSex { get; set; }
		public string PTAge { get; set; }
		public string PTIdType { get; set; }
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
		public string Tags { get; set; }
		public string Notes { get; set; }
		public List<string> SnapPaths { get; set; }
	}
}
