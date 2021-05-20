using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalSQA.Pages
{
    public class SamplePageTest : Helpers.Setup
    {
        public SamplePageTest()
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "input[type='file']")]
        private IWebElement FileUpload { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@class='name']")]
        private IWebElement Name { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@class='email']")]
        private IWebElement Email { get; set; }
        [FindsBy(How = How.XPath, Using = "//select[@class='select']")]
        private IWebElement Experience { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@type='url']")]
        private IWebElement Website { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Functional Testing']")]
        private IWebElement Functional { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Automation Testing']")]
        private IWebElement Automation { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@value='Manual Testing']")]
        private IWebElement Manual { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@value='Post Graduate']")]
        private IWebElement Education { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Alert Box : Click Here']")]
        private IWebElement AlertButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@class='textarea']")]
        private IWebElement Comment { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement Submit { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='twelve columns']//h3")]
        private IWebElement ResponseMesage { get; set; }

        internal void FormSubmit()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(FileUpload));
                string wanted_path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\GlobalSQA\\"));
                FileUpload.SendKeys(wanted_path+"NUNNA.jpg");
                Name.Click();
                Name.SendKeys("Nunna Veeraprasad" + Keys.Tab);
                Email.SendKeys("nunnaveeraprasad@gmail.com");
                Website.SendKeys("https://www.globalsqa.com/");
                SelectElement expe = new SelectElement(Experience);
                expe.SelectByValue("10+");
                Functional.Click();
                Automation.Click();
                Manual.Click();
                Education.Click();
                AlertButton.Click();
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                IAlert alert1 = driver.SwitchTo().Alert();
                alert1.Accept();
                Helpers.HelperClass.GetElementAndScrollTo(driver, By.XPath("//textarea[@class='textarea']"));
                Comment.Click();
                Comment.SendKeys("Good Quality Comment!");
                string FilledForm = Helpers.HelperClass.SaveScreenshot(driver, "FilledForm");
                Test.Log(Status.Pass, "Before form Submitted", MediaEntityBuilder.CreateScreenCaptureFromPath(FilledForm).Build());
                Submit.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ResponseMesage));
                Helpers.HelperClass.GetElementAndScrollTo(driver, By.XPath("//div[@class='twelve columns']//h3"));
                String responseMsg = ResponseMesage.Text;
                Assert.AreEqual(responseMsg, "Message Sent (go back)");
                string ResponseMsgScreenShot = Helpers.HelperClass.SaveScreenshot(driver, "ResponseMsgScreenShot");
                Test.Log(Status.Pass, "After Response Generated", MediaEntityBuilder.CreateScreenCaptureFromPath(ResponseMsgScreenShot).Build());
            }
            catch (Exception e)
            {
                Test.Log(AventStack.ExtentReports.Status.Fail, "Test failed,please check the logs"+e);
            }
        }


        internal void FormSubmitwithDDT()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(FileUpload));
                string wanted_path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\GlobalSQA\\"));
                Helpers.ExcelLib.populateInCollection(wanted_path+"TestData.xlsx", "Data");
                FileUpload.SendKeys(wanted_path + "NUNNA.jpg");
                Name.Click();
                Name.SendKeys(Helpers.ExcelLib.ReadData(2,"Name"));
                Email.SendKeys(Helpers.ExcelLib.ReadData(2, "Email"));
                Website.SendKeys(Helpers.ExcelLib.ReadData(2, "WebSite"));
                SelectElement expe = new SelectElement(Experience);
                expe.SelectByValue(Helpers.ExcelLib.ReadData(2, "Experiene"));
                Functional.Click();
                Automation.Click();
                Manual.Click();
                Education.Click();
                AlertButton.Click();
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                IAlert alert1 = driver.SwitchTo().Alert();
                alert1.Accept();
                Helpers.HelperClass.GetElementAndScrollTo(driver, By.XPath("//textarea[@class='textarea']"));
                Comment.Click();
                Comment.SendKeys(Helpers.ExcelLib.ReadData(2, "Comment"));
                string FilledForm = Helpers.HelperClass.SaveScreenshot(driver, "FilledForm");
                Test.Log(Status.Pass, "Before form Submitted", MediaEntityBuilder.CreateScreenCaptureFromPath(FilledForm).Build());
                Submit.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ResponseMesage));
                Helpers.HelperClass.GetElementAndScrollTo(driver, By.XPath("//div[@class='twelve columns']//h3"));
                String responseMsg = ResponseMesage.Text;
                Assert.AreEqual(responseMsg, Helpers.ExcelLib.ReadData(2, "Assertion"));
                string ResponseMsgScreenShot = Helpers.HelperClass.SaveScreenshot(driver, "ResponseMsgScreenShot");
                Test.Log(Status.Pass, "After Response Generated", MediaEntityBuilder.CreateScreenCaptureFromPath(ResponseMsgScreenShot).Build());
            }
            catch (Exception e)
            {
                Test.Log(AventStack.ExtentReports.Status.Fail, "Test failed,please check the logs" + e);
            }
        }

    }
}
