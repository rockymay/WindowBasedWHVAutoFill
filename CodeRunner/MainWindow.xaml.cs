using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace CodeRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            outText.Text = DateTime.Now.ToString("yyyy_MM_dd_HH:mm:ss");

        }

        private void RunCode(object sender, RoutedEventArgs e)
        {

            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + usernameTextBox.Text + Environment.NewLine + passwordTextBox.Text + Environment.NewLine;
            IWebDriver driver = new ChromeDriver();
            loginSteps(driver);

        }


        #region  Construct Element

        //[FindsBy(How = How.Name, Using = "username")]
        //public IWebElement username { get; set; }
        //[FindsBy(How = How.Name, Using = "password")]
        //public IWebElement password { get; set; }
        //[FindsBy(How = How.XPath, Using = "/html/body/div/form/div/div[3]/div/div[2]/div[3]/input")]
        //public IWebElement appluNowButton { get; set; }

        ////Application Status
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_applicationList_applicationsDataGrid_referenceNumberLabel_0")]
        //public IWebElement reference { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_applicationList_applicationsDataGrid_statusLabel_0")]
        //public IWebElement status { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_applicationList_applicationsDataGrid_paymentStatusLabel_0")]
        //public IWebElement paymentStatus { get; set; }

        ////Form Tabs
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_0")]
        //public IWebElement personalTab { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_1")]
        //public IWebElement healthTab { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_2")]
        //public IWebElement characterTab { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_3")]
        //public IWebElement specificTab { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_0")]
        //public IWebElement personalTabImage { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_1")]
        //public IWebElement healthTabImage { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_2")]
        //public IWebElement characterTabImage { get; set; }
        //[FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_3")]
        //public IWebElement specificTabImage { get; set; }




        //Previous, Save, Complete Later, Next
        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_previousImageButton")]
        public IWebElement previousBtn { get; set; }
        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_validateButton")]
        public IWebElement saveBtn { get; set; }
        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_completeLaterImageButton")]
        public IWebElement completeLaterBtn { get; set; }
        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")]
        public IWebElement nextBtn { get; set; }


        #endregion

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            genderTextBox.Text = "";
            idTextBox.Text = "";
            outText.Text = "";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            attempNo.Text = "1";
            fNameTextBox.Text = "";
            gNameTextBox.Text = "";
            addressTextBox.Text = "";
            suburbTextBox.Text = "";
            cityTextBox.Text = "";
            passportTextBox.Text = "";
            birthDay.Text = "";
            birthMonth.Text = "";
            birthYear.Text = "";
            expireDay.Text = "";
            expireMonth.Text = "";
            expireYear.Text = "";
            issueDay.Text = "";
            issueMonth.Text = "";
            issueYear.Text = "";
            beenDayTextBox.Text = "";
            beenMonthTextBox.Text = "";
            beenYearTextBox.Text = "";
            cardCVCTextBox.Text = "";
            cardHolderTextBox.Text = "";
            cardMonthTextBox.Text = "";
            cardNoTextBox.Text = "";
            cardYearTextBox.Text = "";
            visaType.Text = "V";
            intendTravelDay.Text = "1";
            intendTravelMonth.Text = "1";
            intendTravelYear.Text = "2018";


        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void male_Checked(object sender, RoutedEventArgs e)
        {
            //if (male.IsChecked == true) ;
            //{ }

        }

        public void loginSteps(IWebDriver driver)
        {


            driver.Navigate().GoToUrl(url2TextBox.Text);

            driver.Manage().Window.Maximize();

            //Login attemp
            driver.FindElement(By.Name("username")).SendKeys(usernameTextBox.Text);
            driver.FindElement(By.Name("password")).SendKeys(passwordTextBox.Text);
            driver.FindElement(By.XPath("/html/body/div/form/div/div[3]/div/div[2]/div[3]/input")).Click();

            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "Login Successful" + Environment.NewLine;



            driver.Navigate().GoToUrl("https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Create.aspx?CountryId=24");

            //string body = driver.FindElement(By.Id("main")).ToString();

            //if (body.Contains("Access denied"))
            //{
            //    driver.Navigate().GoToUrl(url2TextBox.Text);
            //    driver.Manage().Window.Maximize();
            //    //Login attemp
            //    driver.FindElement(By.Name("username")).SendKeys(usernameTextBox.Text);
            //    driver.FindElement(By.Name("password")).SendKeys(passwordTextBox.Text);
            //    driver.FindElement(By.XPath("/html/body/div/form/div/div[3]/div/div[2]/div[3]/input")).Click();
            //}

            string page = string.Empty;
            page = driver.FindElement(By.Id("form1")).Text;

            while (page.Contains("There is no scheme open for this country."))
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Create.aspx?CountryId=24");
            }

            if (page.Contains("Scheme is available"))
            {
                driver.FindElement(By.Id("ContentPlaceHolder1_applyNowButton")).Click();  //Click APPLY NOW

                string pageContent = driver.FindElement(By.Id("form1")).Text;
                if (pageContent.Contains("Multiple applications are not supported."))
                {
                    Options(driver);
                }
                else
                {
                    FilloutTheForms(driver);
                }


            }



        }

        //Decide which step to go when form exists
        public void Options(IWebDriver driver)
        {
            var baseUrl = "https://onlineservices.immigration.govt.nz/WorkingHoliday/";
            driver.Navigate().GoToUrl(baseUrl);

            string myreference = driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_referenceNumberLabel_0")).Text;
            string mystatus = driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_statusLabel_0")).Text;
            string mypaymentstatus = driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_paymentStatusLabel_0")).Text;

            string editUrl = "https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Edit.aspx?ApplicationId=" + myreference;
            string submitUrl = "https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Submit.aspx?ApplicationId" + myreference;
            string paymentUrl = "https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Edit.aspx?ApplicationId=" + myreference;
            https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Submit.aspx?ApplicationId=1822647

            if (mystatus == "Incomplete" || mystatus == "Ineligible")//Forms not complete
            {
                driver.Navigate().GoToUrl(editUrl);
                FilloutTheForms(driver);
            }
            else if (mystatus == "Completed pending submission") //Form not submitted
            {
                driver.Navigate().GoToUrl(submitUrl);
                Submit(driver);
            }

            else if (mystatus == "XXXXX") //Form not paid
            {
                driver.Navigate().GoToUrl(paymentUrl);
                PaySteps(driver);
            }
        }

        //Looping the forms filing 
        public void FilloutTheForms(IWebDriver driver)
        {
            IWebElement personalTab = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_0"));
            IWebElement healthTab = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_1"));
            IWebElement characterTab = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_2"));
            IWebElement specificTab = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_tabButton_3"));

            IWebElement personalTabImage = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_0"));
            IWebElement healthTabImage = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_1"));
            IWebElement characterTabImage = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_2"));
            IWebElement specificTabImage = driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_statusImage_3"));



            if (
                personalTabImage.GetAttribute("src").Contains("SectionTick") &&
                 healthTabImage.GetAttribute("src").Contains("SectionTick") &&
                  characterTabImage.GetAttribute("src").Contains("SectionTick") &&
                   specificTabImage.GetAttribute("src").Contains("SectionTick")
                )
            {
                driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_submitSuperLink"));//Need to rectify
                Submit(driver);


            }
            if (personalTabImage.GetAttribute("src").Contains("SectionCross"))
            {
                personalTab.Click();
                Personal(driver);
            }
            if (healthTabImage.GetAttribute("src").Contains("SectionCross"))
            {
                healthTab.Click();
                Health(driver);
            }
            if (characterTabImage.GetAttribute("src").Contains("SectionCross"))
            {
                //Fillout Character
                characterTab.Click();
                Character(driver);
            }
            if (specificTabImage.GetAttribute("src").Contains("SectionCross"))
            {
                //Fillout Specific
                specificTab.Click();
                Specific(driver);
            }


            FilloutTheForms(driver);
        }

        public void Personal(IWebDriver driver)
        {


            //First & Last names
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_familyNameTextBox")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_givenName1Textbox")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_familyNameTextBox")).SendKeys(fNameTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_givenName1Textbox")).SendKeys(gNameTextBox.Text);

            //Gender & DOB

            DropdownSelect(driver, "s2id_ContentPlaceHolder1_personDetails_genderDropDownList", "Female", 2);
            DropdownSelect(driver, "s2id_ContentPlaceHolder1_personDetails_CountryDropDownList", "China", 3);
            DropdownSelect(driver, "s2id_ContentPlaceHolder1_addressContactDetails_address_countryDropDownList", "China", 4);
            DropdownSelect(driver, "s2id_ContentPlaceHolder1_hasAgent_representedByAgentDropdownlist", "No", 5);
            DropdownSelect(driver, "s2id_ContentPlaceHolder1_hasCreditCard_hasCreditCardDropDownlist", "Yes", 7);

            var bmonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var birth = birthDay.Text + " " + bmonth + "," + birthYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_dateOfBirthDatePicker_DatePicker")).SendKeys(birth);

            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_address1TextBox")).SendKeys(addressTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_suburbTextBox")).SendKeys(suburbTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_cityTextBox")).SendKeys(cityTextBox.Text);

            //Next.....
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            //Passport
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_passportNumberTextBox")).SendKeys(passportTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_confirmPassportNumberTextBox")).SendKeys(passportTextBox.Text);
            var expireMonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var expireDate = expireDay.Text + " " + expireMonth + "," + expireYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_passportExpiryDateDatePicker_DatePicker")).SendKeys(expireDate);

            //Other ID
            DropdownSelect(driver, "s2id_ContentPlaceHolder1_identification_otherIdentificationDropdownlist", "National ID", 1);
            var issueMonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var issueDate = birthDay.Text + " " + issueMonth + "," + birthYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_otherIssueDateDatePicker_DatePicker")).SendKeys(issueDate);

            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Health(IWebDriver driver)
        {

            string message = "Please try again later.";
            string url = driver.Url;
            string[] healthId = new string[] {
                                            "s2id_ContentPlaceHolder1_medicalConditions_renalDialysisDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_tuberculosisDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_cancerDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_heartDiseaseDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_disabilityDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_hospitalisationDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_residentailCareDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_pregnancy_pregnancyStatusDropDownList",
                                            "s2id_ContentPlaceHolder1_medicalConditions_tbRiskDropDownList"};


            for (int i = 0; i < healthId.Length; i++)
            {
                if (healthId.Length == 9)
                {
                    try { DropdownSelect(driver, healthId[i], "No", (1 + i)); } catch { }
                }
                else if (healthId.Length == 8)
                {
                    DropdownSelect(driver, healthId[0], "No", (1));
                    DropdownSelect(driver, healthId[1], "No", (2));
                    DropdownSelect(driver, healthId[2], "No", (3));
                    DropdownSelect(driver, healthId[3], "No", (4));
                    DropdownSelect(driver, healthId[4], "No", (5));
                    DropdownSelect(driver, healthId[5], "No", (6));
                    DropdownSelect(driver, healthId[6], "No", (7));
                    DropdownSelect(driver, healthId[7], "No", (9));
                }

            }

            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Character(IWebDriver driver)
        {

            string[] characterID = new string[]
            {
                                            "s2id_ContentPlaceHolder1_character_imprisonment5YearsDropDownList",
                                            "s2id_ContentPlaceHolder1_character_imprisonment12MonthsDropDownList",
                                            "s2id_ContentPlaceHolder1_character_removalOrderDropDownList",
                                            "s2id_ContentPlaceHolder1_character_deportedDropDownList",
                                            "s2id_ContentPlaceHolder1_character_chargedDropDownList",
                                            "s2id_ContentPlaceHolder1_character_convictedDropDownList",
                                            "s2id_ContentPlaceHolder1_character_underInvestigationDropDownList",
                                            "s2id_ContentPlaceHolder1_character_excludedDropDownList",
                                            "s2id_ContentPlaceHolder1_character_removedDropDownList"
            };


            DropdownSelect(driver, characterID[0], "No", (1));
            DropdownSelect(driver, characterID[1], "No", (2));
            DropdownSelect(driver, characterID[2], "No", (3));
            DropdownSelect(driver, characterID[3], "No", (4));
            DropdownSelect(driver, characterID[4], "No", (6));
            DropdownSelect(driver, characterID[5], "No", (7));
            DropdownSelect(driver, characterID[6], "No", (8));
            DropdownSelect(driver, characterID[7], "No", (9));
            DropdownSelect(driver, characterID[8], "No", (10));


            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Specific(IWebDriver driver)
        {
            string[] SpecificID = new string[]
            {
                                            "s2id_ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_previousWhsPermitVisaDropDownList",
                                            "s2id_ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_sufficientFundsHolidayDropDownList",
                                            "s2id_ContentPlaceHolder1_offshoreDetails_beenToNzDropDownList",
                                            "s2id_ContentPlaceHolder1_offshoreDetails_requirementsQuestions_sufficientFundsOnwardTicketDropDownList",
                                            "s2id_ContentPlaceHolder1_offshoreDetails_requirementsQuestions_readRequirementsDropDownList",
                                          
            };

            DropdownSelect(driver, SpecificID[0], "No", 1);
            DropdownSelect(driver, SpecificID[1], "Yes", 2);
            DropdownSelect(driver, SpecificID[3], "Yes", 4);
            DropdownSelect(driver, SpecificID[4], "Yes", 5);

            //Intendday
            var intendMonth = (new DateTime(2011, int.Parse(intendTravelMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var intendDate = intendTravelDay.Text + " " + intendMonth + "," + intendTravelYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_offshoreDetails_intendedTravelDateDatePicker_DatePicker")).SendKeys(intendDate);


            bool beentoNZ = (beenCheckBox.IsChecked == true) ? true : false;
            string beentoNZYet = beentoNZ ? "Yes" : "No";
            
            switch (beentoNZYet)
            {
                case "Yes":
                    DropdownSelect(driver, SpecificID[2], "Yes", 3);

                    var beenMonth = (new DateTime(2011, int.Parse(beenMonthTextBox.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
                    var beenDate = beenDayTextBox.Text + " " + beenMonth + "," + beenYearTextBox.Text;
                    driver.FindElement(By.Id("ContentPlaceHolder1_offshoreDetails_whenInNZDatePicker")).SendKeys(beenDate);
                    
                    break;
                case "No":
                    DropdownSelect(driver, SpecificID[2], "No", 3);
                    break;
                default: break;
            }

            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Submit(IWebDriver driver)
        {
            
            string[] SubmitCheck = new string[] {
                                            "ContentPlaceHolder1_falseStatementCheckBox",
                                            "ContentPlaceHolder1_notesCheckBox",
                                            "ContentPlaceHolder1_circumstancesCheckBox",
                                            "ContentPlaceHolder1_warrantsCheckBox",
                                            "ContentPlaceHolder1_informationCheckBox",
                                            "ContentPlaceHolder1_healthCheckBox",
                                            "ContentPlaceHolder1_adviceCheckBox",
                                            "ContentPlaceHolder1_registrationCheckBox",
                                            "ContentPlaceHolder1_entitlementCheckbox",
                                            "ContentPlaceHolder1_permitExpiryCheckBox",
                                            "ContentPlaceHolder1_medicalInsuranceCheckBox"
            };
            //*[@id="ContentPlaceHolder1_preSubmitPanel"]/table/tbody/tr[4]/td[2]/label/span
            foreach (string index in SubmitCheck)
            {
                IWebElement checkbox = driver.FindElement(By.Id(index));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].style.display = 'block'", checkbox);
                checkbox.Click();
            }

            //Becareful
            // driver.FindElement(By.Id("ContentPlaceHolder1_submitImageButton")).Click();

            PaySteps(driver);

        }

        public void PaySteps(IWebDriver driver)
        {
            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "You are trying to pay" + Environment.NewLine;

            //PERSONAL
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_onlinePaymentAnchor")).Click();
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_payorNameTextBox")).SendKeys(cardHolderTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_okImageButton")).Click();

            switch (visaType.Text.ToUpper())
            {
                case "M": driver.FindElement(By.Id("card_type_MASTERCARD")).Click(); break;
                case "V": driver.FindElement(By.Id("card_type_VISA")).Click(); break;
                    //default: break;
            }




            driver.FindElement(By.Id("cardverificationcode")).SendKeys(cardCVCTextBox.Text);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("expirymonth")).SendKeys(cardMonthTextBox.Text);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("expiryyear")).SendKeys(cardYearTextBox.Text);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("cardholder")).SendKeys(cardHolderTextBox.Text);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("cardnumber")).SendKeys(cardNoTextBox.Text);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("pay_button")).Click();

















        }

        private void SubmitPayOnly(object sender, RoutedEventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            loginSteps(driver);

        }



        private static void DropdownSelect(IWebDriver driver, string identifier, string keyword, int index)
        {
            driver.FindElement(By.Id(identifier)).Click();
            var selectResult = driver.FindElements(By.XPath("//*[@id='select2-results-" + index + "']/li"));
            foreach (var option in selectResult)
            {
                if (option.Text == keyword)
                {
                    option.Click();
                    break;
                }
            }
        }


    }
}
