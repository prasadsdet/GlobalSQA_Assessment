using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalSQA
{
    [TestFixture]
    public class TestSuite : Helpers.Setup
    {
        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// Summary : This Test will Drag all the images to Trash Bit
        /// </summary>
        [Test]
        public void DragAnddrop()
        {
            Test = extent.CreateTest("DragAndDropImages");
            driver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/draganddrop/");
            Pages.DragAndDrop ddImg = new Pages.DragAndDrop();
            ddImg.DragAndDropImages();
            Test.Pass("Test passed Successfully");
        }

        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// Summary : This Test will select the Country from Dropdown
        /// </summary>
        [Test]
        public void DropdownCountry()
        {
            Test = extent.CreateTest("SelectCountryFromDropdown");
            driver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/select-dropdown-menu/");
            Pages.Dropdown DDCountry = new Pages.Dropdown();
            DDCountry.SelectCountry();
            Test.Pass("Test passed Successfully");
        }

        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// Summary : This Test will select the Country from Dropdown and the Data is in TestData.xlsx file
        /// </summary>
        [Test]
        public void DropdownCountryWithExcelData()
        {
            Test = extent.CreateTest("DropdownCountryWithExcelData");
            driver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/select-dropdown-menu/");
            Pages.Dropdown DDCountry = new Pages.Dropdown();
            DDCountry.SelectCountryWithDDT();
            Test.Pass("Test passed Successfully");
        }

        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// Summary : This Test will Fill the form using keyword driven Approach
        /// </summary>
        [Test]
        public void SampleForm()
        {
            Test = extent.CreateTest("FillFormWithKeyword");
            driver.Navigate().GoToUrl("https://www.globalsqa.com/samplepagetest/");
            Pages.SamplePageTest FillForm = new Pages.SamplePageTest();
            FillForm.FormSubmit();
            Test.Pass("Test passed Successfully");
        }

        /// <summary>
        /// Author : Prasad.Nunna
        /// Date : 20/05/2021
        /// Summary : This Test will Fill the form using Data driven Approach(Data in the Excel ile)
        /// </summary>
        [Test]
        public void SampleFormWithExcelData()
        {
            Test = extent.CreateTest("FillFormWithDataDrivenApproach");
            driver.Navigate().GoToUrl("https://www.globalsqa.com/samplepagetest/");
            Pages.SamplePageTest FillForm = new Pages.SamplePageTest();
            FillForm.FormSubmitwithDDT();
            Test.Pass("Test passed Successfully");
        }
    }
}
