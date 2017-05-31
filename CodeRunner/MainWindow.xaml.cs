using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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

            outText.Text += (genderSelectCombobox.SelectedItem as ComboBoxItem).Content.ToString();



            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

            //new Thread(() =>
            //{

            //    Thread.CurrentThread.IsBackground = true;
            //    IWebDriver driver = new ChromeDriver(options);
            //    StartNow(driver);

            //}).Start();

            IWebDriver driver = new ChromeDriver();
            StartNow(driver);

        }

        private void ClearOutput(object sender, RoutedEventArgs e)
        {

            outText.Text = "";
            //usernameTextBox.Text = "";
            //passwordTextBox.Text = "";
           
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


        }
        private void loadTestData_Click(object sender, RoutedEventArgs e)
        {

          
            outText.Text = "";
          
            fNameTextBox.Text = "Xiaohong";
            gNameTextBox.Text = "Sun";
            addressTextBox.Text = "Road 56 Ave";
            suburbTextBox.Text = "Gothen";
            cityTextBox.Text = "Denmark";
            passportTextBox.Text = "xxx123123";
            birthDay.Text = "12";
            birthMonth.Text = "12";
            birthYear.Text = "1992";
            expireDay.Text = "20";
            expireMonth.Text = "12";
            expireYear.Text = "2020";
            issueDay.Text = "12";
            issueMonth.Text = "11";
            issueYear.Text = "2016";
            beenDayTextBox.Text = "12";
            beenMonthTextBox.Text = "12";
            beenYearTextBox.Text = "2016";

            cardHolderTextBox.Text = "Xiaosun";
            cardMonthTextBox.Text = "12";
            cardNoTextBox.Text = "19";
            cardYearTextBox.Text = "2020";
            visaType.Text = "V";
            cardCVCTextBox.Text = "178";


        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void StartNow(IWebDriver driver)
        {


            driver.Navigate().GoToUrl(url2TextBox.Text);

            driver.Manage().Window.Maximize();

            //Login attemp
            driver.FindElement(By.Name("username")).EnterValue(usernameTextBox.Text);
            driver.FindElement(By.Name("password")).EnterValue(passwordTextBox.Text);
            driver.FindElement(By.XPath("/html/body/div/form/div/div[3]/div/div[2]/div[3]/input")).Click();

            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "Login Successful" + Environment.NewLine;



            driver.Navigate().GoToUrl("https://onlineservices.immigration.govt.nz/WorkingHoliday/Application/Create.aspx?CountryId=24");


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
                    //FilloutTheForms(driver, submitUrl);
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

            if (mystatus == "Incomplete" || mystatus == "Ineligible")//Forms not complete
            {
                try { driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_editHyperLink_0")).Click(); }
                catch { driver.Navigate().GoToUrl(editUrl); }
              
                FilloutTheForms(driver);
                
            }
            else if (mystatus == "Completed pending submission") //Form not submitted
            {
                try { driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_submitHyperlink_0")).Click(); }
                catch { driver.Navigate().GoToUrl(submitUrl); } 
                Submit(driver);
            }

            else if (mystatus == "Submitted") //Form not paid
            {
                try { driver.FindElement(By.Id("ContentPlaceHolder1_applicationList_applicationsDataGrid_payHyperLink_0")).Click(); }
                catch { }
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
                try { WaitForElement(driver, By.Id("ContentPlaceHolder1_wizardPageHeader_submitSuperLink"), 10); } catch { }
                driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageHeader_submitSuperLink")).Click();
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

            string genderSelected = (genderSelectCombobox.SelectedItem as ComboBoxItem).Name.ToString();

            //First & Last names
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_familyNameTextBox")).EnterValue(fNameTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_givenName1Textbox")).EnterValue(gNameTextBox.Text);

            //Gender & DOB

            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_personDetails_genderDropDownList", "Female", 2);
            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_personDetails_CountryDropDownList", "China", 3);
            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_addressContactDetails_address_countryDropDownList", "China", 4);
            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_hasAgent_representedByAgentDropdownlist", "No", 5);
            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_hasCreditCard_hasCreditCardDropDownlist", "Yes", 7);

            ForceDropdown(driver, "ContentPlaceHolder1_personDetails_genderDropDownList", genderSelected);
            ForceDropdown(driver, "ContentPlaceHolder1_personDetails_CountryDropDownList", "46");   //d 46 = China, 82 = Germany
            ForceDropdown(driver, "ContentPlaceHolder1_addressContactDetails_address_countryDropDownList", "46");   //d 46 = China, 82 = Germany
            ForceDropdown(driver, "ContentPlaceHolder1_hasAgent_representedByAgentDropdownlist", "No");  
            ForceDropdown(driver, "ContentPlaceHolder1_hasCreditCard_hasCreditCardDropDownlist", "Yes");  




            var bmonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var birth = birthDay.Text + " " + bmonth + "," + birthYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_personDetails_dateOfBirthDatePicker_DatePicker")).EnterValue(birth);

            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_address1TextBox")).EnterValue(addressTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_suburbTextBox")).EnterValue(suburbTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_addressContactDetails_address_cityTextBox")).EnterValue(cityTextBox.Text);

            //Next.....
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            //Passport
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_passportNumberTextBox")).EnterValue(passportTextBox.Text);
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_confirmPassportNumberTextBox")).EnterValue(passportTextBox.Text);
            var expireMonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var expireDate = expireDay.Text + " " + expireMonth + "," + expireYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_passportExpiryDateDatePicker_DatePicker")).EnterValue(expireDate);

            //Other ID
            //DropdownSelect(driver, "s2id_ContentPlaceHolder1_identification_otherIdentificationDropdownlist", "National ID", 1);
            ForceDropdown(driver, "ContentPlaceHolder1_identification_otherIdentificationDropdownlist", "3");


            var issueMonth = (new DateTime(2011, int.Parse(birthMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var issueDate = birthDay.Text + " " + issueMonth + "," + birthYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_identification_otherIssueDateDatePicker_DatePicker")).EnterValue(issueDate);

            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Health(IWebDriver driver)
        {

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
            string[] HealthIdSelect = new string[] {
                                            "ContentPlaceHolder1_medicalConditions_renalDialysisDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_tuberculosisDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_cancerDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_heartDiseaseDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_disabilityDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_hospitalisationDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_residentailCareDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_pregnancy_pregnancyStatusDropDownList",
                                            "ContentPlaceHolder1_medicalConditions_tbRiskDropDownList"};

            foreach (string identifier in HealthIdSelect)
            {
                try { ForceDropdown(driver, identifier, "No"); } catch { }
            }

            //for (int i = 0; i < healthId.Length; i++)
            //{
            //    if (healthId.Length == 9)
            //    {
            //        try { DropdownSelect(driver, healthId[i], "No", (1 + i)); } catch { }
            //    }
            //    else if (healthId.Length == 8)
            //    {
            //        DropdownSelect(driver, healthId[0], "No", (1));
            //        DropdownSelect(driver, healthId[1], "No", (2));
            //        DropdownSelect(driver, healthId[2], "No", (3));
            //        DropdownSelect(driver, healthId[3], "No", (4));
            //        DropdownSelect(driver, healthId[4], "No", (5));
            //        DropdownSelect(driver, healthId[5], "No", (6));
            //        DropdownSelect(driver, healthId[6], "No", (7));
            //        DropdownSelect(driver, healthId[7], "No", (9));
            //    }

            //}

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

            string[] characterIDSelect = new string[]
            {
                                            "ContentPlaceHolder1_character_imprisonment5YearsDropDownList",
                                            "ContentPlaceHolder1_character_imprisonment12MonthsDropDownList",
                                            "ContentPlaceHolder1_character_removalOrderDropDownList",
                                            "ContentPlaceHolder1_character_deportedDropDownList",
                                            "ContentPlaceHolder1_character_chargedDropDownList",
                                            "ContentPlaceHolder1_character_convictedDropDownList",
                                            "ContentPlaceHolder1_character_underInvestigationDropDownList",
                                            "ContentPlaceHolder1_character_excludedDropDownList",
                                            "ContentPlaceHolder1_character_removedDropDownList"
            };

            foreach (string identifier in characterIDSelect)
            {
                try { ForceDropdown(driver, identifier, "No"); } catch { }
            }

            //DropdownSelect(driver, characterID[0], "No", (1));
            //DropdownSelect(driver, characterID[1], "No", (2));
            //DropdownSelect(driver, characterID[2], "No", (3));
            //DropdownSelect(driver, characterID[3], "No", (4));
            //DropdownSelect(driver, characterID[4], "No", (6));
            //DropdownSelect(driver, characterID[5], "No", (7));
            //DropdownSelect(driver, characterID[6], "No", (8));
            //DropdownSelect(driver, characterID[7], "No", (9));
            //DropdownSelect(driver, characterID[8], "No", (10));


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
            string[] SpecificIDSelect = new string[]
           {
                                            "ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_previousWhsPermitVisaDropDownList",
                                            "ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_sufficientFundsHolidayDropDownList",
                                            "ContentPlaceHolder1_offshoreDetails_beenToNzDropDownList",
                                            "ContentPlaceHolder1_offshoreDetails_requirementsQuestions_sufficientFundsOnwardTicketDropDownList",
                                            "ContentPlaceHolder1_offshoreDetails_requirementsQuestions_readRequirementsDropDownList",

           };

            ForceDropdown(driver, SpecificIDSelect[0], "No");
            ForceDropdown(driver, SpecificIDSelect[1], "Yes");
            ForceDropdown(driver, SpecificIDSelect[3], "Yes");
            ForceDropdown(driver, SpecificIDSelect[4], "Yes");

            //DropdownSelect(driver, SpecificID[0], "No", 1);
            //DropdownSelect(driver, SpecificID[1], "Yes", 2);
            //DropdownSelect(driver, SpecificID[3], "Yes", 4);
            //DropdownSelect(driver, SpecificID[4], "Yes", 5);

            //Intendday
            var intendMonth = (new DateTime(2011, int.Parse(intendTravelMonth.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
            var intendDate = intendTravelDay.Text + " " + intendMonth + "," + intendTravelYear.Text;
            driver.FindElement(By.Id("ContentPlaceHolder1_offshoreDetails_intendedTravelDateDatePicker_DatePicker")).EnterValue(intendDate);


            bool beentoNZ = (beenCheckBox.IsChecked == true) ? true : false;
            string beentoNZYet = beentoNZ ? "Yes" : "No";

            switch (beentoNZYet)
            {
                case "Yes":
                    //DropdownSelect(driver, SpecificID[2], "Yes", 3);
                    ForceDropdown(driver, SpecificIDSelect[2], "Yes");

                    var beenMonth = (new DateTime(2011, int.Parse(beenMonthTextBox.Text), 1)).ToString("MMM", new CultureInfo("en-US"));
                    var beenDate = beenDayTextBox.Text + " " + beenMonth + "," + beenYearTextBox.Text;
                    driver.FindElement(By.Id("ContentPlaceHolder1_offshoreDetails_whenInNZDatePicker")).EnterValue(beenDate);

                    break;
                case "No":
                    //DropdownSelect(driver, SpecificID[2], "No", 3);
                    ForceDropdown(driver, SpecificIDSelect[2], "No");
                    break;
                default: break;
            }

            //Save 
            driver.FindElement(By.Id("ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            FilloutTheForms(driver);
        }

        public void Submit(IWebDriver driver)
        {
            string currentUrl = driver.Url;
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
            //driver.FindElement(By.Id("ContentPlaceHolder1_submitImageButton")).Click();

            try { PaySteps(driver); }
            catch {

                driver.Navigate().GoToUrl(currentUrl);
                Submit(driver);
            }

        }

        public void PaySteps(IWebDriver driver)
        {

            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "You are trying to pay" + Environment.NewLine;

            //Confirm go to secure payment link
            driver.FindElement(By.Id("ContentPlaceHolder1_onlinePaymentAnchor2")).Click();
            driver.FindElement(By.Id("_ctl0_ContentPlaceHolder1_payerNameTextBox")).EnterValue(cardHolderTextBox.Text);
            driver.FindElement(By.Id("_ctl0_ContentPlaceHolder1_okButton")).Click();


            switch (visaType.Text.ToUpper())
            {
                case "M": driver.FindElement(By.Id("card_type_MASTERCARD")).Click(); break;
                case "V": driver.FindElement(By.Id("card_type_VISA")).Click(); break;
                    //default: break;
            }




            driver.FindElement(By.Id("cardverificationcode")).SendKeys(cardCVCTextBox.Text);
            driver.FindElement(By.Id("cardholder")).SendKeys(cardHolderTextBox.Text);
            driver.FindElement(By.Id("cardnumber")).SendKeys(cardNoTextBox.Text);
            driver.FindElement(By.Id("expirymonth")).SendKeys(cardMonthTextBox.Text);
            driver.FindElement(By.Id("expiryyear")).SendKeys(cardYearTextBox.Text);
            driver.FindElement(By.Id("pay_button")).Click();
















        }

        private void SubmitPayOnly(object sender, RoutedEventArgs e)
        {

            IWebDriver driver = new ChromeDriver();
            StartNow(driver);
        }



        //private static void DropdownSelect(IWebDriver driver, string identifier, string keyword, int index)
        //{
        //    driver.FindElement(By.Id(identifier)).Click();
        //    var selectResult = driver.FindElements(By.XPath("//*[@id='select2-results-" + index + "']/li"));
        //    foreach (var option in selectResult)
        //    {
        //        if (option.Text == keyword)
        //        {
        //            option.Click();
        //            break;
        //        }
        //    }
        //}

        public static IWebElement WaitForElement(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(ExpectedConditions.ElementIsVisible(by)));
        }
        private static void ForceDropdown(IWebDriver driver, string identifier, string selectValue)
        {

            IWebElement checkbox = driver.FindElement(By.Id(identifier));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].style.display = 'block'", checkbox);
            new SelectElement(checkbox).SelectByValue(selectValue);
        }

        private void beenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            beentoNZGrid.Visibility = Visibility.Visible;
        }

        private void beenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            beentoNZGrid.Visibility = Visibility.Hidden;
        }

     
    }
}
