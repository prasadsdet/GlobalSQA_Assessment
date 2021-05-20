using ExcelDataReader;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalSQA.Helpers
{
    public class HelperClass : Helpers.Setup
    {
        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// This Method responsible for saving the screen shot in Screenshots  folder
        /// </summary>
        public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName)
        {
            string FolderPth = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\GlobalSQA\\ScreenShots\\"));
            var folderLocation = (FolderPth);
            if (!System.IO.Directory.Exists(folderLocation))
            {
                System.IO.Directory.CreateDirectory(folderLocation);
            }

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var filename = new StringBuilder(folderLocation);
            filename.Append(ScreenShotFileName);
            filename.Append(DateTime.Now.ToString("_dd-MM-yyyy_mss"));
            filename.Append(".Jpeg");
            screenshot.SaveAsFile(filename.ToString(), ScreenshotImageFormat.Jpeg);
            return filename.ToString();
        }

        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// This Method responsible for scroll element to visible area
        /// </summary>
        public static IWebElement GetElementAndScrollTo(IWebDriver driver, By by)
        {
            var js = (IJavaScriptExecutor)driver;
            try
            {
                var element = driver.FindElement(by);
                if (element.Location.Y > 200)
                {
                    js.ExecuteScript($"window.scrollTo({0}, {element.Location.Y - 200 })");
                }
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }

    /// <summary>
    /// Author : Prasad.Nunna
    /// Date : 20/05/2021
    /// This Class is responsible for getting data from excel file and populate in form fields
    /// </summary>
    public class ExcelLib
    {
        static List<DataCollection> DataCol = new List<DataCollection>();

        public class DataCollection
        {
            public int rowNumber { get; set; }
            public string ColName { get; set; }
            public string ColValue { get; set; }
        }

        public static void ClearData()
        {
            DataCol.Clear();
        }

        private static DataTable ExcelDataToTable(string fileName, string sheetName)
        {
            using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader excelreader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    var DataResult = excelreader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTableCollection Table = DataResult.Tables;
                    DataTable Resulttable = Table[sheetName];
                    return Resulttable;
                }
            }
        }

        public static string ReadData(int RowNumber, string ColumnName)
        {
            try
            {

                RowNumber = RowNumber - 1;
                string data = (from ColData in DataCol
                               where ColData.ColName == ColumnName && ColData.rowNumber == RowNumber
                               select ColData.ColValue).SingleOrDefault();

                if (data != null)
                {
                    return data.ToString();
                }
                else
                {
                    return "";
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception Occured in Read Data Method!" + Environment.NewLine + e.Message.ToString());
                return null;
            }
        }

        public static void populateInCollection(string fileName, string sheetName)
        {
            ExcelLib.ClearData();
            DataTable table = ExcelDataToTable(fileName, sheetName);


            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        ColName = table.Columns[col].ColumnName,
                        ColValue = table.Rows[row - 1][col].ToString()
                    };

                    DataCol.Add(dtTable);
                }
            }

        }



    }
}
