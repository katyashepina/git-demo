﻿using Framework.Model;
using Framework.Pages;
using Framework.Services;
using Framework.Utils;
using log4net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test
{
    [TestFixture]
    public class PayOnlineFormTest : CommonConditions
    {
        const string ErrorTextForSendWithCorrectData =
            "Start SendPayOnlineWithCorrectData unit test.";

        const string ErrorTextForSendPayOnlineWithInCorrectData =
            "Start SendPayOnlineWithOutCorrectData unit test.";

        const string ErrorTextForSendWithOutCorrectPrice =
            "Start SendWithOutCorrectPrice unit test.";
        
        static private ILog Log = LogManager.GetLogger(typeof(PayOnlineFormTest));


        [Test, Description("TestForSendWithCorrectData")]
        public void SendWithCorrectData()
        {
            string expectingMessage = ErrorCreater.CorrectNamePhoneEmail();

            User user = UserCreater.WithAllProperties();

            string errorMessage = new PayOnlinePage()
                                    .FillNameRenterField(user)
                                    .FillNameRenterField(user)
                                    .FillEmailField(user)
                                    .FillPhoneField(user)
                                    .FillPriceField(user)
                                    .SendPayOnline()
                                    .GetErrorMessageText();

            Log.Info(ErrorTextForSendWithCorrectData);

            Assert.AreEqual(expectingMessage, errorMessage, "All data is correct for pay online");
        }


        [Test, Description("TestForSendWithOutCorrectData")]
        public void SendWithOutCorrectData()
        {
            string expectingMessage = ErrorCreater.FormWithInvalidPhone();

            User user = UserCreater.UserWithSimilarStartDateAndEndDate();

            string errorMessage = new PayOnlinePage()
                                    .FillNameRenterField(user)
                                    .FillNameRenterField(user)
                                    .FillEmailField(user)
                                    .FillPhoneField(user)
                                    .FillPriceField(user)
                                    .SendPayOnline()
                                    .GetErrorMessageText();

            Log.Info(ErrorTextForSendPayOnlineWithInCorrectData);

            Assert.AreEqual(expectingMessage, errorMessage, "Phone is incorrect for pay online");
        }


        [Test, Description("TestForSendWithOutCorrectPrice")]
        public void SendWithOutCorrectPrice()
        {
            string expectingMessage = ErrorCreater.FormWithInvalidPrice();

            User user = UserCreater.UserWithIncorrectPrice();

            string errorMessage = new PayOnlinePage()
                                    .FillNameRenterField(user)
                                    .FillNameRenterField(user)
                                    .FillEmailField(user)
                                    .FillPhoneField(user)
                                    .FillPriceField(user)
                                    .SendPayOnline()
                                    .GetErrorMessageText();

            Log.Info(ErrorTextForSendWithOutCorrectPrice);

            Assert.AreEqual(expectingMessage, errorMessage, "Price is incorrect for pay online");
        }
    }
}
