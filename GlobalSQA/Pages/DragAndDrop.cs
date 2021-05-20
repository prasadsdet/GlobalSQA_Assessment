using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    public class DragAndDrop : Helpers.Setup
    {

        public DragAndDrop()
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//iframe[@class='demo-frame lazyloaded']")]
        private IWebElement FrameImag { get; set; }
        [FindsBy(How = How.Id, Using = "trash")]
        private IWebElement Trash { get; set; }
        [FindsBy(How = How.XPath, Using = "//ul[@id='gallery']//img")]
        private IWebElement AllImages { get; set; }
        
        internal void DragAndDropImages()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.SwitchTo().Frame(FrameImag);
                int countimg = driver.FindElements(By.XPath("//ul[@id='gallery']//img")).Count;
                for (int i = 0; i < countimg; i++)
                {                   
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(AllImages));
                    Actions actions = new Actions(driver);
                    actions.DragAndDrop(AllImages, Trash).Build().Perform();                 
                }
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(2000);
                string ImagesAfterDragged = Helpers.HelperClass.SaveScreenshot(driver, "ImagesAfterDragged");
                Test.Log(Status.Pass, "After Images Moved to Trash",MediaEntityBuilder.CreateScreenCaptureFromPath(ImagesAfterDragged).Build());                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error when Drag and drop images,Please check the logs...");

            }
        }

    }
}
