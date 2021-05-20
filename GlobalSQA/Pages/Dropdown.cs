using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalSQA.Pages
{
    public class Dropdown : Helpers.Setup
    {
        public Dropdown()
        {
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//div[@rel-title='Select Country']//select")]
        private IWebElement CountryDropDown { get; set; }


        internal void SelectCountry()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                SelectElement drpCountry = new SelectElement(CountryDropDown);
                drpCountry.SelectByValue("IND");
                Thread.Sleep(2000);
                Test.Log(Status.Pass, "details",MediaEntityBuilder.CreateScreenCaptureFromBase64String("base64String").Build());
                string CountrySelected = Helpers.HelperClass.SaveScreenshot(driver, "CountrySelected");
                Test.Log(Status.Pass, "Selected Country", MediaEntityBuilder.CreateScreenCaptureFromPath(CountrySelected).Build());

            }
            catch(Exception e)
            {
                Test.Log(Status.Fail, "Test Failed"+e);

            }
        }

        internal void SelectCountryWithDDT()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                Helpers.ExcelLib.populateInCollection(wanted_path + "TestData.xlsx", "Data");
                SelectElement drpCountry = new SelectElement(CountryDropDown);
                drpCountry.SelectByValue(Helpers.ExcelLib.ReadData(2, "Country"));
                Thread.Sleep(2000);
                Test.Log(Status.Pass, "details", MediaEntityBuilder.CreateScreenCaptureFromBase64String("base64String").Build());
                string CountrySelected = Helpers.HelperClass.SaveScreenshot(driver, "CountrySelected");
                Test.Log(Status.Pass, "Selected Country", MediaEntityBuilder.CreateScreenCaptureFromPath(CountrySelected).Build());

            }
            catch (Exception e)
            {
                Test.Log(Status.Fail, "Test Failed" + e);

            }
        }
    }
}
