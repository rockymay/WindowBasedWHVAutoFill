using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
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
            DataFillSteps(driver);

        }

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
            



            //Save some string
            string editStatus = "Incomplete";
            string submitStatus = "Completed pending submission";
            string payStatus = "Submitted";
            string newForm = "Only";
            string existForm = "Reference";
            string noPlaceMessage = "Sorry, there is no form to edit";
            string warningMessage = "Unfortunately the available places is filled, attemp:";

            #region Define IWebElements
            string usernameTextBoxId = "OnlineServicesLoginStealth_VisaLoginControl_userNameTextBox";
            string passwordTextBoxId = "OnlineServicesLoginStealth_VisaLoginControl_passwordTextBox";
            string loginBtnId = "OnlineServicesLoginStealth_VisaLoginControl_loginImageButton";
            string countryListId = "ctl00_ContentPlaceHolder1_countryDropDownList";
            string okBtnId = "ctl00_ContentPlaceHolder1_okButton";
            string applyBtnId = "ctl00_ContentPlaceHolder1_applyNowButton";
            string editId = "ctl00_ContentPlaceHolder1_applicationList_applicationsDataGrid_ctl02_editHyperLink";
            string submitId = "ctl00_ContentPlaceHolder1_applicationList_applicationsDataGrid_ctl02_submitHyperlink";
            string statusId = "ctl00_ContentPlaceHolder1_applicationList_applicationsDataGrid_ctl02_statusLabel";
            string payId = "ctl00_ContentPlaceHolder1_applicationList_applicationsDataGrid_ctl02_payHyperLink";
            string errorMessageXpath = "//*[@id='aspnetForm']/table[3]/tbody/tr[2]/td/div/div/p[2]";
            //public IWebElement errorMessage { get; set; }
            #endregion

            //Navigate to
            //globalDrive.Navigate().GoToUrl(Global.ExcelLib.ReadData(2,"url1"));
            driver.Navigate().GoToUrl(url1TextBox.Text);

            Thread.Sleep(1000);
            driver.Manage().Window.Maximize();



            //Login attemp
            driver.FindElement(By.Id(usernameTextBoxId)).SendKeys(usernameTextBox.Text);

            driver.FindElement(By.Id(passwordTextBoxId)).SendKeys(passwordTextBox.Text);
            driver.FindElement(By.Id(loginBtnId)).ClickAndWait();
            driver.Navigate().GoToUrl(url2TextBox.Text);

            outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  "+"Login Successful" + Environment.NewLine;
            //Compare new page and already have form page
            string contentText = driver.FindElement(By.XPath("/html/body")).Text;
            
            //Check if there is an existing form
            if (contentText.Contains(newForm))
            {

                for (int i = 1; i < 1000000; i++)
                {
                    outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "Trying to apply for :" + i + Environment.NewLine;
                    //There is no form
                    new SelectElement(driver.FindElement(By.Id(countryListId))).SelectByValue(countryCodeTextBox.Text); //46=China, 82=Germany

                    //OKBtn.Click();
                    driver.FindElement(By.Id(okBtnId)).Click();
                    try
                    {
                        driver.FindElement(By.Id(applyBtnId)).Click(); //Apply Button

                        outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "Form taken successfully";
                        Global.SaveScreenShotClass.SaveScreenshot(driver, "FormTaken"); //Taking screenshot
                       
                        DataFillSteps(driver);
                        break;
                    }

                    catch (Exception)
                    {
                        outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + noPlaceMessage + i + Environment.NewLine;
                        driver.Navigate().GoToUrl(url2TextBox.Text);
                        //Thread.Sleep(700);
                        outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + driver.FindElement(By.XPath(errorMessageXpath)).Text + Environment.NewLine;
                    }

                }
            }

            //When you have form and it might be NOT COMPLETED/ COMPLETED YET SUBMITTED/ SUBMITTED YET PAID
            else if (contentText.Contains(existForm))
            {
                //Not completed
                if (editStatus.Equals(driver.FindElement(By.Id(statusId)).Text))
                {
                    try
                    {
                        driver.FindElement(By.Id(editId)).Click();
                        DataFillSteps(driver);
                    }
                    catch (Exception) { outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + warningMessage + Environment.NewLine;}
                }
                //Not submitted
                else if (submitStatus.Equals(driver.FindElement(By.Id(statusId)).Text))
                {
                    try
                    {
                        driver.FindElement(By.Id(submitId)).Click();
                        Submit(driver);
                        
                    }
                    catch (Exception) { Submit(driver); }
                }
                //Not paid
                else if (payStatus.Equals(driver.FindElement(By.Id(statusId)).Text))
                {
                    try
                    {
                        driver.FindElement(By.Id(payId)).Click();
                        PaySteps(driver);
                    }
                    catch (Exception) { outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + warningMessage + Environment.NewLine; }
                }
            }
        }

        public void CheckTableFilled(IWebDriver driver, ref bool[] pageTable)
        {
           
          try
            {
                for (int i = 0; i < 4; i++)
                {
                    string statusImage = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_ctl0" + i + "_statusImage")).GetAttribute("src");

                    pageTable[i] = statusImage.Contains("error") ? false : true;
                }
            }
            catch (Exception) { }


        }

        private bool[] pageTable = new bool[4];

        public void DataFillSteps(IWebDriver driver)
        {
            CheckTableFilled(driver, ref pageTable);

            foreach (bool item in pageTable) { outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + item; }

            for (int i = 0; i < 10; i++)
            {
                if (pageTable[0] && pageTable[1] && pageTable[2] && pageTable[3])
                {
                    try
                    {
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_submitImageButton")).Click();
                        Submit(driver);
                    }
                    catch (Exception) { outText.Text += DateTime.Now.ToString("HH:mm:ss") + "  " + "You cant submit NOW" + Environment.NewLine; }
                }
                else
                {
                    if (!pageTable[0]) { Personal(driver); }
                    else
                    {
                        if (!pageTable[1]) { Health(driver); }
                        else
                        {
                            if (!pageTable[2]) { Character(driver); }
                            else
                            {
                                if (!pageTable[3]) { WHV(driver); }
                            }
                        }
                    }
                }
            }
        }
        public void Personal(IWebDriver driver)
        {

            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_ctl00_tabButton")).Click();
            //First & Last Name  &&  Address, Suburb, City, Country
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_familyNameTextBox")).Clear();
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_givenName1Textbox")).Clear();

            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_familyNameTextBox")).SendKeys(fNameTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_givenName1Textbox")).SendKeys(gNameTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_addressContactDetails_address_address1TextBox")).SendKeys(addressTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_addressContactDetails_address_suburbTextBox")).SendKeys(suburbTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_addressContactDetails_address_cityTextBox")).SendKeys(suburbTextBox.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_addressContactDetails_address_countryDropDownList"))).SelectByValue(countryCodeTextBox.Text);//46=China, 82=Germany

            //Gender & DOB
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_genderDropDownList"))).SelectByValue(genderTextBox.Text.ToUpper());
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_dateOfBithDatePicker_Day"))).SelectByValue(birthDay.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_dateOfBithDatePicker_Month"))).SelectByValue(birthMonth.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_dateOfBithDatePicker_Year"))).SelectByValue(birthYear.Text);


            //Country
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_personDetails_CountryDropDownList"))).SelectByValue(countryCodeTextBox.Text); //46=China, 82=Germany
            //Agent?Email?VisaCard?
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_hasAgent_representedByAgentDropdownlist"))).SelectByValue("No");
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_communicationMethod_communicationMethodDropDownList"))).SelectByValue("1");
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_hasCreditCard_hasCreditCardDropDownlist"))).SelectByValue("Yes");

            //Next & Passport
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_nextImageButton")).Click();

            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_passportNumberTextBox")).SendKeys(passportTextBox.Text);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_confirmPassportNumberTextBox")).SendKeys(passportTextBox.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_passportExpiryDateDatePicker_Day"))).SelectByValue(expireDay.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_passportExpiryDateDatePicker_Month"))).SelectByValue(expireMonth.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_passportExpiryDateDatePicker_Year"))).SelectByValue(expireYear.Text);
            //Other ID
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_otherIdentificationDropdownlist"))).SelectByValue("3");
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_otherIssueDateDatePicker_Day"))).SelectByValue(issueDay.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_otherIssueDateDatePicker_Month"))).SelectByValue(issueMonth.Text);
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_identification_otherIssueDateDatePicker_Year"))).SelectByValue(issueYear.Text);

            //Save
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_validateButton")).Click();
            
            CheckTableFilled(driver, ref pageTable);
        }

        public void Health(IWebDriver driver)
        {
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_ctl01_tabButton")).Click();

            string message = "Please try again later.";
            string url = driver.Url;
            string[] healthId = new string[] {
                                            "ctl00_ContentPlaceHolder1_medicalConditions_renalDialysisDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_tuberculosisDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_cancerDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_heartDiseaseDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_disabilityDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_hospitalisationDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_residentailCareDropDownList",
                                            "ctl00_ContentPlaceHolder1_medicalConditions_tbRiskDropDownList"};

            foreach (string index in healthId)
            {
                new SelectElement(driver.FindElement(By.Id(index))).SelectByValue("No");
                Thread.Sleep(500);
                if (message == driver.FindElement(By.XPath("/html/body")).Text)
                {
                    driver.Navigate().Refresh();
                }
            }
           
            //Save
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_validateButton")).Click();
            CheckTableFilled(driver, ref pageTable);
        }

        public void Character(IWebDriver driver)
        {

            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_ctl02_tabButton")).Click();

            string[] characterID = new string[] 
            {
                                            "ctl00_ContentPlaceHolder1_character_imprisonment5YearsDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_imprisonment12MonthsDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_removalOrderDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_deportedDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_chargedDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_convictedDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_underInvestigationDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_excludedDropDownList",
                                            "ctl00_ContentPlaceHolder1_character_removedDropDownList"
            };

            foreach (string index in characterID)
            {
                new SelectElement(driver.FindElement(By.Id(index))).SelectByValue("No");
            }
            //Save refresh tableStatus
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_validateButton")).Click();
            CheckTableFilled(driver, ref pageTable);
        }

        public void WHV(IWebDriver driver)
        {

            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageHeader_nav_sectionTabs_TabHeaders_ctl03_tabButton")).Click();
            //Previously hold WHV?
           
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_previousWhsPermitVisaDropDownList"))).SelectByValue("No");
            Thread.Sleep(500);
            //Enough fund?
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_commonWHSQuestions_sufficientFundsHolidayDropDownList"))).SelectByValue("Yes");
            Thread.Sleep(500);
            //Intend travel day
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_intendedTravelDateDatePicker_Day"))).SelectByValue(intendTravelDay.Text);
            Thread.Sleep(500);
            //Intend travel month
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_intendedTravelDateDatePicker_Month"))).SelectByValue(intendTravelMonth.Text);
            Thread.Sleep(500);
            //Intend travel year
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_intendedTravelDateDatePicker_Year"))).SelectByValue(intendTravelYear.Text);
            //Previous been to NZ?
            bool beentoNZ = (beenCheckBox.IsChecked == true) ? true: false;
            string beentoNZYet = beentoNZ ? "Yes" : "No";
            

            switch (beentoNZYet)
            {
                case "Yes":
                    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_beenToNzDropDownList"))).SelectByIndex(2);
                    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_whenInNZDatePicker_Day"))).SelectByValue(beenDayTextBox.Text);
                    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_whenInNZDatePicker_Month"))).SelectByValue(beenMonthTextBox.Text);
                    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_whenInNZDatePicker_Year"))).SelectByValue(beenYearTextBox.Text);
                    break;
                case "No":
                    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_beenToNzDropDownList"))).SelectByIndex(1);
                    break;
                default: break;
            }
            //Enough Fund?
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_requirementsQuestions_sufficientFundsOnwardTicketDropDownList"))).SelectByValue("Yes");
            new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_offshoreDetails_requirementsQuestions_readRequirementsDropDownList"))).SelectByValue("Yes");
            //Save
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_wizardPageFooter_wizardPageNavigator_validateButton")).Click();
            CheckTableFilled(driver, ref pageTable);
        }

        public void Submit(IWebDriver driver)
        {
            string[] SubmitCheck = new string[] {
                                            "ctl00_ContentPlaceHolder1_falseStatementCheckBox",
                                            "ctl00_ContentPlaceHolder1_notesCheckBox",
                                            "ctl00_ContentPlaceHolder1_circumstancesCheckBox",
                                            "ctl00_ContentPlaceHolder1_warrantsCheckBox",
                                            "ctl00_ContentPlaceHolder1_informationCheckBox",
                                            "ctl00_ContentPlaceHolder1_healthCheckBox",
                                            "ctl00_ContentPlaceHolder1_adviceCheckBox",
                                            "ctl00_ContentPlaceHolder1_entitlementCheckbox",
                                            "ctl00_ContentPlaceHolder1_registrationCheckBox",
                                            "ctl00_ContentPlaceHolder1_permitExpiryCheckBox",
                                            "ctl00_ContentPlaceHolder1_medicalInsuranceCheckBox"
            };

            foreach (string index in SubmitCheck)
            {
                driver.FindElement(By.Id(index)).Click();
            }

           // driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_submitImageButton")).Click();



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
                case "M": driver.FindElement(By.Id("card_type_MASTERCARD")).Click();break;
                case"V": driver.FindElement(By.Id("card_type_VISA")).Click();break;
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

       
    }
}
