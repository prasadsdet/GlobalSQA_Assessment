using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace GlobalSQA.Helpers
{
    [SetUpFixture]
    public class Setup
    {
        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentTest Test;
        public static string wanted_path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\GlobalSQA\\"));

        /// <summary>
        /// Author: Prasad.Nunna
        /// Date: 20/05/2021
        /// Summary : OneTimeSetup will initilize the Extent Report
        /// </summary>
        [OneTimeSetUp]
        public void InitilizeReport()
        {
            ExtentHtmlReporter htmlExtentReport = new ExtentHtmlReporter(wanted_path+"TestReport.html");//
            htmlExtentReport.LoadConfig(wanted_path+"html-config.xml");
            extent = new ExtentReports();
            extent.AttachReporter(htmlExtentReport);
        }

        /// <summary>
        /// Author: Prasad.Nunna
        /// Date: 20/05/2021
        /// Summary : Setup will initilize driver i specified
        /// </summary>
        [SetUp]
        public void SetupDrivers()
        {
            ChromeOptions ChromeOpt = new ChromeOptions();
            ChromeOpt.AddUserProfilePreference("download.default_directory", "C:\\");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://www.globalsqa.com/demo-site/draganddrop/");
        }
        /// <summary>
        /// Author: Prasad.Nunna
        /// Date: 20/05/2021
        /// Summary : Teardown method will close the browser window after test executed
        /// </summary>
        [TearDown]
        public void teamdown()
        {
            driver.Close();
            driver.Quit();
        }

        /// /// <summary>
        /// Author: Prasad.Nunna
        /// Date: 20/05/2021
        /// Summary : OnetimeTeardown method will close the report and append the results to Test Report
        /// </summary>
        [OneTimeTearDown]
        public void CloseReport()
        {
            extent.Flush();
        }

    }
}
