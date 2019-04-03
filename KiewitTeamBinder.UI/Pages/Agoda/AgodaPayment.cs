using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.AgodaEnums;
using KiewitTeamBinder.Common.Models.Agoda;
using OpenQA.Selenium.Support.UI;

namespace KiewitTeamBinder.UI.Pages.Agoda
{
    public class AgodaPayment : AgodaGeneral
    {
        public AgodaPayment(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _txtFullName => By.XPath("(//input[@id='fullName'])[1]");
        static By _txtGuestFullName => By.XPath("(//input[@id='fullName'])[2]");
        static By _txtFirstName => By.XPath("(//input[@id='firstName'])[1]");
        static By _txtGuestFirstName => By.XPath("(//input[@id='firstName'])[2]");
        static By _txtLastName => By.XPath("(//input[@id='lastName'])[1]");
        static By _txtGuestLastName => By.XPath("(//input[@id='lastName'])[2]");
        static By _txtPaymentElement(string txtId) => By.XPath($"//input[@id='{txtId}']");
        static By _cboPaymentElement(string cboId) => By.XPath($"//select[@id='{cboId}']");
        static By _chkIsBookForSomeoneElse => By.XPath("//input[@id='isMakingThisBookingForSomeoneElse']");
        static By _chkIsArrangeTransportation => By.XPath("//input[@data-selenium='transportation-checkbox']");
        static By _radPaymentElement(string radFor) => By.XPath($"//label[@for='{radFor}']/span[@class='radio - check']");
        static By _btnNextPage => By.XPath("//button[@name='booking-continue-btn']");
        static By _lblHotel => By.XPath("//span[@class='hotel-name']");
        static By _lblCheckinCheckoutDate => By.XPath("//h4[contains(@data-bind,'checkinCheckoutDate')]");
        static By _lblDuration => By.XPath("//h4[contains(@data-bind,'nightsText')]");
        static By _lblRoomType => By.XPath("//span[contains(@data-bind,'roomTypeListText')]");
        static By _lblOccupancy => By.XPath("//span[@id='occupancyDetails']");
        static By _lblTotalPrice => By.XPath("//strong[@id='finalAmount' or @id='totalAmount']");
        static By _btnBackToBookingDetails => By.XPath("//div[@class='text-left flip hidden-xs']/button[@data-bind='click: backToFirstStep']");

        #endregion

        #region Elements
        public IWebElement TxtFullName => StableFindElement(_txtFullName);
        public IWebElement TxtGuestFullName => StableFindElement(_txtGuestFullName);
        public IWebElement TxtFirstName => StableFindElement(_txtFirstName);
        public IWebElement TxtGuestFirstName => StableFindElement(_txtGuestFirstName);
        public IWebElement TxtLastName => StableFindElement(_txtLastName);
        public IWebElement TxtGuestLastName => StableFindElement(_txtGuestLastName);
        public IWebElement TxtPaymentElement(string txtId) => StableFindElement(_txtPaymentElement(txtId));
        public IWebElement CboPaymentElement(string cboId) => StableFindElement(_cboPaymentElement(cboId));
        public IWebElement ChkIsBookForSomeoneElse => StableFindElement(_chkIsBookForSomeoneElse);
        public IWebElement ChkIsArrangeTransportation => StableFindElement(_chkIsArrangeTransportation);
        public IWebElement RadPaymentElement(string radFor) => StableFindElement(_radPaymentElement(radFor));
        public IWebElement BtnNextPage => StableFindElement(_btnNextPage);
        public IWebElement LblHotel => StableFindElement(_lblHotel);
        public IWebElement LblCheckinCheckoutDate => StableFindElement(_lblCheckinCheckoutDate);
        public IWebElement LblDuration => StableFindElement(_lblDuration);
        public IWebElement LblRoomType => StableFindElement(_lblRoomType);
        public IWebElement LblOccupancy => StableFindElement(_lblOccupancy);
        public IWebElement LblTotalPrice => StableFindElement(_lblTotalPrice);
        public IWebElement BtnBackToBookingDetails => StableFindElement(_btnBackToBookingDetails);

        #endregion

        #region Methods
        public AgodaPayment EnterCustomerInformation(CustomerInformation customerInfo)
        {
            var node = CreateStepNode();
            node.Info("Enter the customer information");
            if (customerInfo.FullName != null)
                TxtFullName.InputText(customerInfo.FullName);
            else
            {
                TxtFirstName.InputText(customerInfo.FirstName);
                TxtLastName.InputText(customerInfo.LastName);
            }
            TxtPaymentElement(Payment.Email.ToDescription()).InputText(customerInfo.Email);
            TxtPaymentElement(Payment.RetypeEmail.ToDescription()).InputText(customerInfo.RetypeEmail);
            if (customerInfo.Country != null)
                CboPaymentElement(Payment.ResidenceCountry.ToDescription()).SelectItem(customerInfo.Country);
            if (customerInfo.MobileNumber != null)
                TxtPaymentElement(Payment.MobileNumber.ToDescription()).InputText(customerInfo.MobileNumber);
            if (customerInfo.IsBookForSomeoneElse != null)
            {
                if (customerInfo.IsBookForSomeoneElse == true)
                {
                    ChkIsBookForSomeoneElse.Check();
                    if (customerInfo.GuestFullName != null)
                        TxtGuestFullName.InputText(customerInfo.GuestFullName);
                    else
                    {
                        TxtGuestFirstName.InputText(customerInfo.GuestFirstName);
                        TxtGuestLastName.InputText(customerInfo.GuestLastName);
                    }
                }
            }
            if (customerInfo.NonSmokingRoom != null)
            {
                if (customerInfo.NonSmokingRoom == true)
                    RadPaymentElement(Payment.NonSmokingRoom.ToDescription()).Check();
                else if (customerInfo.NonSmokingRoom == false)
                    RadPaymentElement(Payment.SmokingRoom.ToDescription()).Check();
            }
            if (customerInfo.IsLargeBedOrTwinBeds != null)
            {
                if (customerInfo.IsLargeBedOrTwinBeds == true)
                    RadPaymentElement(Payment.LargeBed.ToDescription()).Check();
                else if (customerInfo.IsLargeBedOrTwinBeds == false)
                    RadPaymentElement(Payment.TwinBeds.ToDescription()).Check();
            }
            if (customerInfo.ArrivalTime != null)
                CboPaymentElement(Payment.ArrivalTime.ToDescription()).SelectItem(customerInfo.ArrivalTime);
            BtnNextPage.Click();
            EndStepNode(node);
            return this;
        }

        public List <KeyValuePair<string, bool>> ValidateRoomPrice(PlaceToStay placeToStay, Room room)
        {
            var node = CreateStepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            try
            {
                //string currency = Constant.currency;
                string oneRoomPriceValue = Constant.roomPrice;
                int oneRoomPrice = Int32.Parse(ConvertPrice(oneRoomPriceValue));
                string roomPrice = (Math.Round(oneRoomPrice * placeToStay.Room * placeToStay.Duration * 1.05 * 1.1)).ToString();
                string roomPriceInPaymentPage = LblTotalPrice.Text;
                //bool isCurrencyCorrect = currency.Equals(roomPriceInPaymentPage.Substring(0, 1));
                bool isPriceCorrect = (ConvertPrice(roomPriceInPaymentPage)).Contains(roomPrice);
                //if (isCurrencyCorrect)
                //    validations.Add(SetPassValidation(node, ValidationMessage.ValidateRoomPriceCurency));
                //else
                //    validations.Add(SetFailValidation(node, ValidationMessage.ValidateRoomPriceCurency, currency, roomPriceInPaymentPage.Substring(0, 1)));
                if (isPriceCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateRoomPriceValue));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateRoomPriceValue, roomPrice, roomPriceInPaymentPage));
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateRoomPriceValue, e));
            }
            EndStepNode(node);
            return validations;
        }

        public string ConvertPrice(string price)
        {
            var node = CreateStepNode();
            node.Info("Convert price: " + price);
            string oneRoomPrice;
            if (price.Contains(","))
                oneRoomPrice = price.Replace(",", "");
            else if (price.Contains("."))
                oneRoomPrice = price.Replace(".", "");
            else
                oneRoomPrice = price;
            EndStepNode(node);
            return oneRoomPrice;
        }

        public KeyValuePair<string, bool> ValidateHotelInformation(string hotelName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                string actualHotelName = LblHotel.Text;
                bool isHotelNameCorrect = actualHotelName.Contains(hotelName);
                if (isHotelNameCorrect)
                    validation = SetPassValidation(node, ValidationMessage.ValidateHotelInformation);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateHotelInformation, hotelName, actualHotelName);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateHotelInformation, e);
            }
            EndStepNode(node);
            return validation;
        }

        public List<KeyValuePair<string, bool>> ValidateCheckinCheckoutDateAndDuration(PlaceToStay placeToStay)
        {
            var node = CreateStepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            try
            {
                string actualCheckinCheckout = LblCheckinCheckoutDate.Text;
                string actualDuration = LblDuration.Text;
                string expectedCheckinCheckout = Constant.checkinCheckoutDate;
                bool isCheckinCheckoutCorrect = actualCheckinCheckout.Equals(expectedCheckinCheckout);
                bool isDurationCorrect = actualDuration.Contains(placeToStay.Duration.ToString());
                if (isCheckinCheckoutCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateCheckinCheckoutDate));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateCheckinCheckoutDate, expectedCheckinCheckout, actualCheckinCheckout));
                if (isDurationCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateDuration));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateDuration, placeToStay.Duration.ToString(), actualDuration));
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateCheckinCheckoutDateAndDuration, e));
            }
            EndStepNode(node);
            return validations;
        }

        public List<KeyValuePair<string, bool>> ValidateOccupancy(PlaceToStay placeToStay, Room room)
        {
            var node = CreateStepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            try
            {
                string actualRoomType = LblRoomType.Text;
                string actualRoomAndPerson = LblOccupancy.Text;
                int actualRoomQuantity;
                int actualAdultQuantity;
                string[] tempActualRoomAndPersonSplit = actualRoomAndPerson.Split(',');
                if(tempActualRoomAndPersonSplit.Contains(" "))
                {
                    List<int> listValue = new List<int>();
                    for (int i = 0; i < tempActualRoomAndPersonSplit.Count(); i++)
                    {
                        string[] tempValue = (tempActualRoomAndPersonSplit[i].Trim()).Split(' ');
                        listValue.Add(Int32.Parse(tempValue[0]));
                    }
                    int[] value = listValue.ToArray();
                    actualRoomQuantity = value[0];
                    actualAdultQuantity = value[1];
                }
                else
                {
                    actualRoomQuantity = Int32.Parse(tempActualRoomAndPersonSplit[0].Trim().Substring(0, 1));
                    actualAdultQuantity = Int32.Parse(tempActualRoomAndPersonSplit[1].Trim().Substring(0, 1));
                }
                bool isRoomTypeCorrect = actualRoomType.Contains(room.RoomType);
                bool isRoomQuantityCorrect = placeToStay.Room.Equals(actualRoomQuantity);
                bool isAdultQuantityCorrect = placeToStay.Adults.Equals(actualAdultQuantity);
                if (isRoomTypeCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateOccupancyRoomType));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateOccupancyRoomType, room.RoomType, actualRoomType));
                if(isRoomQuantityCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateOccupancyRoomQuantity));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateOccupancyRoomQuantity, placeToStay.Room.ToString(), actualRoomQuantity.ToString()));
                if (isAdultQuantityCorrect)
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateOccupancyAduldQuantity));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateOccupancyAduldQuantity, placeToStay.Adults.ToString(), actualAdultQuantity.ToString()));
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateOccupancy, e));
            }
            EndStepNode(node);
            return validations;
        }

        public List<KeyValuePair<string, bool>> ValidateCustomerInfo(CustomerInformation customerInfo)
        {
            var node = CreateStepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            try
            {
               if(customerInfo.FullName != null)
                {
                    string actualFullName = TxtFullName.GetAttribute("value");
                    if (actualFullName.Equals(customerInfo.FullName))
                        validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoFullName));
                    else
                        validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoFullName, customerInfo.FullName, actualFullName));
                }
                else
                {
                    string actualFirstName = TxtFirstName.GetAttribute("value");
                    string actualLastName = TxtLastName.GetAttribute("value");
                    if (actualFirstName.Equals(customerInfo.FirstName))
                        validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoFirstName));
                    else
                        validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoFirstName, customerInfo.FirstName, actualFirstName));
                    if (actualLastName.Equals(customerInfo.LastName))
                        validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoLastName));
                    else
                        validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoLastName, customerInfo.LastName, actualLastName));
                }
                string actualEmail = TxtPaymentElement(Payment.Email.ToDescription()).GetAttribute("value");
                string actualRetypeEmail = TxtPaymentElement(Payment.RetypeEmail.ToDescription()).GetAttribute("value");
                string actualCountry = CboPaymentElement(Payment.ResidenceCountry.ToDescription()).GetSelectedValueInCombobox();
                
                if (actualEmail.Equals(customerInfo.Email))
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoEmail));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoEmail, customerInfo.Email, actualEmail));
                if (actualRetypeEmail.Equals(customerInfo.RetypeEmail))
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoRetypeEmail));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoRetypeEmail, customerInfo.RetypeEmail, actualRetypeEmail));
                if (customerInfo.Country != null)
                {
                    if (actualCountry.Equals(customerInfo.Country))
                        validations.Add(SetPassValidation(node, ValidationMessage.ValidateCustomerInfoCountry));
                    else
                        validations.Add(SetFailValidation(node, ValidationMessage.ValidateCustomerInfoCountry, customerInfo.Country, actualCountry));
                }
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateCustomerInfo, e));
            }
            EndStepNode(node);
            return validations;
        }

        public AgodaPayment BackToBookingDetails()
        {
            var node = CreateStepNode();
            node.Info("Click Button Back ToBooking Details.");
            IWebElement element = ScrollToElement(_btnBackToBookingDetails);
            BtnBackToBookingDetails.Click();
            EndStepNode(node);
            return this;
        }
        private static class ValidationMessage
        {
            public static string ValidateRoomPriceCurency = "Validate that price currency is correct.";
            public static string ValidateRoomPriceValue = "Validate that room price is correct.";
            public static string ValidateHotelInformation = "Validate that hotel information is correct.";
            public static string ValidateCheckinCheckoutDateAndDuration = "Validate that checkin date, checkout date and staying duration are correct.";
            public static string ValidateCheckinCheckoutDate = "Validate that checkin and checkout date are correct.";
            public static string ValidateDuration = "Validate that staying duration is correct.";
            public static string ValidateOccupancy = "Validate that occupancy details are correct.";
            public static string ValidateOccupancyRoomType = "Validate that room type is correct.";
            public static string ValidateOccupancyRoomQuantity = "Validate that rooms quantity is correct.";
            public static string ValidateOccupancyAduldQuantity = "Validate that adults quantity is correct.";
            public static string ValidateCustomerInfo = "Validate that customer information is correct.";
            public static string ValidateCustomerInfoFullName = "Validate that customer full name is correct.";
            public static string ValidateCustomerInfoEmail = "Validate that customer email is correct.";
            public static string ValidateCustomerInfoRetypeEmail = "Validate that customer retype email is correct.";
            public static string ValidateCustomerInfoCountry = "Validate that customer country is correct.";
            public static string ValidateCustomerInfoFirstName = "Validate that customer first name is correct.";
            public static string ValidateCustomerInfoLastName = "Validate that customer last name is correct.";
        }

        #endregion
    }
}
