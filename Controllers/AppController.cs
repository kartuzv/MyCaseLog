using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCaseLog.Controllers
{
	public static class AppController
	{
		public static DataTable ExcelToDataTable(string filePath, int sheetIndex = 0)
		{
			DataTable dt = new DataTable();
			FileInfo existingFile = new FileInfo(filePath);
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			using (ExcelPackage pck = new ExcelPackage(existingFile))
			{
				var ws = pck.Workbook.Worksheets[0];
				foreach (var cell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
				{
					dt.Columns.Add(cell.Text.Trim());
				}
				for (int i = 2; i <= ws.Dimension.End.Row; i++)
				{
					var row = ws.Cells[i, 1, i, ws.Dimension.End.Column];
					DataRow newRow = dt.NewRow();

					//loop all cells in the row
					foreach (var cell in row)
					{
						if (cell.Formula != "" && cell.Text == "")
						{
							if (cell.Formula.Contains("HYPERLINK("))//HYPERLINK("url/path","friendly-name")
							{
								string targetFile = cell.Formula.Split(",")[1];
								targetFile = targetFile.Replace(")", "").Replace("\"", "");
								newRow[cell.Start.Column - 1] = targetFile.Trim();
								continue;
							}
						}

						newRow[cell.Start.Column - 1] = cell.Text;
					}

					dt.Rows.Add(newRow);

				}
			}
			return dt;
		}

		public static DataTable ExcelPackageToDataTable(ExcelPackage excelPackage, int sheetIndex = 0)
		{
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			DataTable dt = new DataTable();
			ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[sheetIndex];

			//check if the worksheet is completely empty
			if (worksheet.Dimension == null)
			{
				return dt;
			}

			//create a list to hold the column names
			List<string> columnNames = new List<string>();

			//needed to keep track of empty column headers
			int currentColumn = 1;

			//loop all columns in the sheet and add them to the datatable
			foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
			{
				string columnName = cell.Text.Trim();

				//check if the previous header was empty and add it if it was
				if (cell.Start.Column != currentColumn)
				{
					columnNames.Add("Header_" + currentColumn);
					dt.Columns.Add("Header_" + currentColumn);
					currentColumn++;
				}

				//add the column name to the list to count the duplicates
				columnNames.Add(columnName);

				//count the duplicate column names and make them unique to avoid the exception
				//A column named 'Name' already belongs to this DataTable
				int occurrences = columnNames.Count(x => x.Equals(columnName));
				if (occurrences > 1)
				{
					columnName = columnName + "_" + occurrences;
				}

				//add the column to the datatable
				dt.Columns.Add(columnName);

				currentColumn++;
			}

			//start adding the contents of the excel file to the datatable
			for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
			{
				var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
				DataRow newRow = dt.NewRow();

				//loop all cells in the row
				foreach (var cell in row)
				{
					newRow[cell.Start.Column - 1] = cell.Text;
				}

				dt.Rows.Add(newRow);
			}

			return dt;
		}

		public static Image FromFile(string path)
		{
			var bytes = File.ReadAllBytes(path);
			var ms = new MemoryStream(bytes);
			var img = Image.FromStream(ms);
			return img;
		}
	}
}
