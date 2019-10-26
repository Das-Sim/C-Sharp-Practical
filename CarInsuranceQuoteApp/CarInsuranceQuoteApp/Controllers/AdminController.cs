using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsuranceQuoteApp.Models;

namespace CarInsuranceQuoteApp.Controllers
{
    public class AdminController : Controller
    {
        //There must also be an Admin view for a site administrator.This page must:
         
        //GET: Admin
        public ActionResult Index()
        {

            using (InsuranceQuoteEntities3 db = new InsuranceQuoteEntities3())
            {
                var dbquotes = db.Register2.ToList();
                var QuoteList = new List<Register2>();
                foreach (var q in dbquotes) //Show all quotes issued
                {
                    var quotes = new Register2();
                    quotes.FirstName = q.FirstName; //along with the user's first name
                    quotes.LastName = q.LastName; //last name
                    quotes.EmailAddress = q.EmailAddress; //and email address.
                    QuoteList.Add(quotes); 
                }

                return View(QuoteList);

            }

        }
    }
}