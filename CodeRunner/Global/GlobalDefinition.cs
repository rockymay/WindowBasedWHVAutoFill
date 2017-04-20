using Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeRunner.Global
{
    class GlobalDefinition
    {
        //public static IWebDriver driver { get; set; }

        
        public static void TextBox(IWebDriver driver, string locator, string locatorValue, string inputValue)
        {
            switch (locator)
            {
                case "Id":
                    driver.FindElement(By.Id(locatorValue)).Clear();
                    driver.FindElement(By.Id(locatorValue)).SendKeys(inputValue);
                    break;
                case "XPath":
                    driver.FindElement(By.XPath(locatorValue)).Clear();
                    driver.FindElement(By.XPath(locatorValue)).SendKeys(inputValue);
                    break;
                case "CssSelector":
                    driver.FindElement(By.CssSelector(locatorValue)).Clear();
                    driver.FindElement(By.CssSelector(locatorValue)).SendKeys(inputValue);
                    break;
                default: Console.WriteLine("Incorrect Xpath Value"); break;
            }
        }
        public static void ActionButton(IWebDriver driver, string locator, string locatorValue)
        {
            switch (locator)
            {
                case "Id":
                    driver.FindElement(By.Id(locatorValue)).Click();
                    Thread.Sleep(500);
                    break;
                case "XPath":
                    driver.FindElement(By.XPath(locatorValue)).Click();
                    Thread.Sleep(500);
                    break;
                case "CssSelector":
                    driver.FindElement(By.CssSelector(locatorValue)).Click();
                    Thread.Sleep(500);
                    break;
                default: Console.WriteLine("Incorrect Xpath Value"); break;
            }
        }
        public static void DropDown(IWebDriver driver, string locator, string locatorValue, string value)
        {
            switch (locator)
            {
                case "Id": new SelectElement(driver.FindElement(By.Id(locatorValue))).SelectByValue(value); break;
                case "XPath": new SelectElement(driver.FindElement(By.XPath(locatorValue))).SelectByValue(value); break;
                case "CssSelector": new SelectElement(driver.FindElement(By.CssSelector(locatorValue))).SelectByValue(value); break;
                default: Console.WriteLine("Incorrect Xpath Value"); break;
            }
        }
       
        public static IWebElement Element (IWebDriver driver, string locator, string locatorValue)
        {
            
            switch (locator)
            {
                case "Id": return driver.FindElement(By.Id(locatorValue));
                case "XPath": return driver.FindElement(By.XPath(locatorValue));
                case "CssSelector": return driver.FindElement(By.CssSelector(locatorValue));
                case "ClassName": return driver.FindElement(By.ClassName(locatorValue));
                case "LinkText": return driver.FindElement(By.LinkText(locatorValue));
                case "TagName": return driver.FindElement(By.TagName(locatorValue));
                default: return null;
            }


        }
    }



    public class ExcelLib
    {
        static List<Datacollection> dataCol = new List<Datacollection>();

        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }


        public static void ClearData()
        {
            dataCol.Clear();
        }


        private static DataTable ExcelToDataTable(string fileName, string SheetName)
        {
            // Open file and return as Stream
            using (System.IO.FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    excelReader.IsFirstRowAsColumnNames = true;

                    //Return as dataset
                    DataSet result = excelReader.AsDataSet();
                    //Get all the tables
                    DataTableCollection table = result.Tables;

                    // store it in data table
                    DataTable resultTable = table[SheetName];

                    //excelReader.Dispose();
                    //excelReader.Close();
                    // return
                    return resultTable;
                }
            }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations

                rowNumber = rowNumber - 1;
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;


                return data.ToString();
            }

            catch (Exception e)
            {
                //Added by Kumar
                Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                return null;
            }
        }

        public static void PopulateInCollection(string fileName, string SheetName)
        {
            ExcelLib.ClearData();
            DataTable table = ExcelToDataTable(fileName, SheetName);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };


                    //Add all the details for each row
                    dataCol.Add(dtTable);

                }
            }

        }
    }



    public class SaveScreenShotClass
    {
        public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
        {
            string folderLocation = Environment.CurrentDirectory;
            Console.WriteLine(folderLocation);

            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            var fileName = new StringBuilder();

            fileName.Append(@"\" + ScreenShotFileName);
            fileName.Append(DateTime.Now.ToString("_yyyy_MM_dd_mss"));
            fileName.Append(".png");
            screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Png);
            return fileName.ToString();
        }
    }
}
