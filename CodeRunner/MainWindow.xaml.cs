using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using System;
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
            //PageFactory.InitElements(Global.GlobalDefinition.driver, this);
        }

        private void RunCode(object sender, RoutedEventArgs e)
        {
            //run code here
            
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            //string url1 = "http://onlineservices.immigration.govt.nz/secure/Login+Working+Holiday.htm?_ga=1.116740868.305272213.1484108859";
            //string url2 = "http://onlineservices.immigration.govt.nz/WorkingHoliday/";
            //string gender = "F";
            int numberLoop = int.Parse(attempNo.Text);
            string address = addressTextBox.Text;
            string suburb = suburbTextBox.Text;
            string city = cityTextBox.Text;
            string passport = passportTextBox.Text;
            string bDay = birthDay.Text;
            string bMonth = birthMonth.Text;
            string bYear = birthYear.Text;
            string eDay = expireDay.Text;
            string eMonth = expireMonth.Text;
            string eYear = expireYear.Text;
            string iDay = issueDay.Text;
            string iMonth = issueMonth.Text;
            string iYear = issueYear.Text;
            string beenDay = beenDayTextBox.Text;
            string beenMonth = beenMonthTextBox.Text;
            string beenYear = beenYearTextBox.Text;
            bool genderCheck = genderRadio.IsChecked ?? false;
            bool beentoNZ = beenCheckBox.IsChecked ?? false;

            //if (genderCheck) { gender = "M"; }
            LoginPage loginObj = new LoginPage();
            loginObj.loginSteps(numberLoop, username, password);

            
        }

       

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            attempNo.Text = "";
            
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


        }

     
    }
}
