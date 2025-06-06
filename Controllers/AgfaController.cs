using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCaseLog.Controllers
{
	public static class AgfaController
	{
		private const string AGFA_WADO_URL_STUDYLOOKUP = "https://eivnacsc2.imaging.ccf.org/wado?user=PEERLEARNAPIUSER&password=j%5ES%26%25y%23HeVXSX8&requestType=Study&maxResults=1&contentType=text%2Fjavascript&AccessionNumber={0}";
		private const string AGFA_EI_INTEGRATIONS_SHOWSTUDY_STUDYUID = "http://localhost:5006/integration/showStudy?suid={0}";
		/*
		 * $.ajax({url: apiLookupURL }) .done(function (studyUID) {
		 if (studyUID != "") {
					let urlInEA = `http://localhost:5006/integration/showStudy?suid=${studyUID}`;
					console.log('calling EI', urlInEA);
					$("#eilink_progressbar").removeClass("bg-warning").addClass("bg-success");
					$.get(urlInEA, function (x) { }).fail(function () {
						$("#eilink_progressbar").removeClass("bg-success").addClass("bg-danger");
						window.open(`https://web.imaging.ccf.org/?theme=epicstudy&AccessionNumber=${acc}`, '_blank');
					});
				}
				if (studyUID == "") {
					$("#eilink_progressbar").removeClass("bg-success").addClass("bg-danger");
					window.open(`https://web.imaging.ccf.org/?theme=epicstudy&AccessionNumber=${acc}`, '_blank');
				}
			}
		 */

		public static async Task<bool> OpenInAgfaEI(string acc)
		{ 
			var studyUID = await LookupStudyUIDByAcc(acc);
			if (!string.IsNullOrEmpty(studyUID))
				return await OpenStudyInAgfaEIByStudyUID(studyUID);
			else return false;
		}
		private static async Task<bool>  OpenStudyInAgfaEIByStudyUID(string studyUID)
		{
			bool result = false;
			using (var httpClient = new HttpClient())
			{
				string wadoAPI_URL = string.Format(AGFA_EI_INTEGRATIONS_SHOWSTUDY_STUDYUID, studyUID);

				var  Tresponse = await httpClient.GetAsync(wadoAPI_URL);//.Result;

				result =  Tresponse.IsSuccessStatusCode;
			 }
			return result;
		}

		private static async Task<string> LookupStudyUIDByAcc(string acc)
		{
			string studyUID = "";

			using (var httpClient = new HttpClient())
			{
				string wadoAPI_URL = string.Format(AGFA_WADO_URL_STUDYLOOKUP, acc);

				HttpResponseMessage response = httpClient.GetAsync(wadoAPI_URL).Result;
				if (response.IsSuccessStatusCode)
				{
					var jsonString = await response.Content.ReadAsStringAsync();
					if (!jsonString.Contains("\"studyUID\":")) return "";

					var responseObj = JsonConvert.DeserializeObject<JObject>(jsonString);
					studyUID = FindFirstTagValueInWADOResponse(responseObj, "studyUID");
					
					
				}
			}
			return studyUID;

		}

		private static string FindFirstTagValueInWADOResponse(JObject jobj, string tagName)
		{
			string tagValue = "";
			var arrPatIDsAndStudy = jobj.SelectTokens(".children[0].children").Last().ToArray();//object Jtokens

			if (arrPatIDsAndStudy.Length > 0)
			{
				//from end to start - faster
				for (int i = (arrPatIDsAndStudy.Length - 1); i > -1; i--)
				{
					var studyObj = ((JObject)arrPatIDsAndStudy[i]).Properties().FirstOrDefault(x => x.Name == "studyUID");
					if (studyObj != null)//found
					{
						tagValue = studyObj.Value.ToString();
						break;
					}
				}
			}
			return tagValue;
		}
	}
}
