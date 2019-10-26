using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CarInsuranceQuoteApp.Models;

namespace CarInsuranceQuoteApp.Controllers
{
    public class HomeController : Controller
    { 

        public ActionResult Index()
        {
        return View();
        }
        //Use the following rules to calculate a quote:
        public ActionResult Quote(string FirstName, string LastName, string EmailAddress, DateTime Age,
            string vYear, string vMake, string vModel, string DUI, string Speeder, string Coverage)
        {
            //try
            // {

            decimal Price = 50; //Start with a base of $50 / month.
            var quotes = new List<Register2>();
            var Q = new CarInsuranceQuoteApp.Models.Register2();

            Q.FirstName = FirstName;
            Q.LastName = LastName;
            Q.EmailAddress = EmailAddress;
            Q.Price = 50;


            var AgeCheck = DateTime.Now.Year - Age.Year;

            if (AgeCheck < 18) //If the user is under 18, add $100 to the monthly total.
            {
                Price += 100;
            }

            else if (AgeCheck < 25) //If the user is under 25, add $25 to the monthly total.
            {
                Price += 25;
            }

            else if (AgeCheck > 100)
            {
                Price += 25;
            }

            if (Convert.ToInt32(vYear) < 2000) //If the car's year is before 2000, add $25 to the monthly total.
            {
                Price += 25;
            }

            else if (Convert.ToInt32(vYear) > 2015) //If the car's year is after 2015, add $25 to the monthly total.
            {
                Price += 25;
            }

            else
            {

            }

            var make = vMake.ToLower();
                if (make == "porche") //If the car's Make is a Porsche, add $25 to the Price.
            {
                    Price += 25;
                }
                var model = vModel.ToLower();

                if (make == "porche" && model == "911 carrera") //If the car's Make is a Porsche and its model is a 911 Carrera, add an additional $25 to the Price.
            {
                    Price += 25;
                }

                for (int i = 0; i < Convert.ToInt32(Speeder); i++) //Add $10 to the monthly total for every speeding ticket the user has.
                {
                    Price += 10;
                }
                var dui = DUI.ToLower(); 
                if (dui == "yes") //If the user has ever had a DUI, add 25 % to the total.
            {
                    Price = Price + (Price / 4);
                }

                var coverage = Coverage.ToLower();
                if (coverage == "a") //If it's full coverage, add 50% to the total.
                {
                    Price = Price + (Price / 2);
                }
                Price = TruncateDecimal(Price, 2);
                Q.Price = Convert.ToDouble(Price);
                quotes.Add(Q);

                using (InsuranceQuoteEntities3 db = new InsuranceQuoteEntities3())
                {
                    Q = new Register2();
                    Q.FirstName = FirstName;
                    Q.LastName = LastName;
                    Q.EmailAddress = EmailAddress;

                    db.Register2.Add(Q);
                    db.SaveChanges();
                }
                return View(quotes);
            // }
            //catch
            // {
            //return View("Error");
            //}

        }
        private decimal TruncateDecimal(decimal h, int c)
        {
            decimal v = (decimal)Math.Pow(10, c);
            decimal z = Math.Truncate(v * h);
            return z / v;
        }
    }
}