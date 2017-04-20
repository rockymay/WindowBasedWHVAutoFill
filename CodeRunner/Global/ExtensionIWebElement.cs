using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeRunner
{
    public static class ExtensionIWebElement
    {
        //Extention to IWebElement, Clear Textbox before Input
        public static void EnterValue(this IWebElement element, string input)
        {
            element.Clear();
            element.SendKeys(input);

        }


        //Extention to IWebElement, Wait for 0.5s After Every Click
        public static void ClickAndWait(this IWebElement element)
        {
            element.Click();
            Thread.Sleep(500);

        }

    }
}
