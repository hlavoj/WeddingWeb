using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestSvWeb2.Models;

namespace TestSvWeb2.Controllers
{
    public class HomeController : Controller
    {
        string _attempIdName = "AttempId";
        string _weddingIdName = "WeddingId";


        public IActionResult Index(string id)
        {
            // validate id
            // insert data do db and get attempId 
            // set attempId to session
            try
            {
                WeddingDbContext ctx = new WeddingDbContext();
                var weddingList = ctx.Set<Wedding>().Where(w => w.Identifikator == id).ToList();
                if (weddingList.Count != 1)
                {
                    return RedirectToAction("WrongId");
                }
                var wedding =weddingList.First();
                wedding.QrCodeOpend++;
                ctx.Update(wedding);
                var weddingAttemp = ctx.Add(new WeddingAttemp{ IdWedding = wedding.Id, OpenTime = DateTime.Now, });
                ctx.SaveChanges();
                HttpContext.Session.SetInt32(_attempIdName, weddingAttemp.Entity.Id);
                HttpContext.Session.SetString(_weddingIdName, id);

                Invitation invitation = new Invitation {SecondLine = "Rrádi bychom tě pozvali na svatbu."};
                if (wedding.InvitationType == 1)
                {
                    invitation.FirstLine = $"Milý {wedding.DisplayName}";
                }
                if (wedding.InvitationType == 2)
                {
                    invitation.FirstLine = $"Milá {wedding.DisplayName}";
                }
                if (wedding.InvitationType == 3)
                {
                    invitation.FirstLine = $"Milá {wedding.DisplayName}";
                    invitation.SecondLine = "Rrádi bychom más pozvali na svatbu.";
                }

                return View(invitation);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult Contact()
        {
            try
            {
                var attempId = HttpContext.Session.GetInt32(_attempIdName);
                var weddingId = HttpContext.Session.GetString(_weddingIdName);
                WeddingDbContext ctx = new WeddingDbContext();
                var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
                weddingAttemp.Participation = 1;
                var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
                ctx.SaveChanges();

                return View(new Contact{Email = wedding.Email, Number = wedding.Number});
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IActionResult EditContact()
        {
            try
            {
                var attempId = HttpContext.Session.GetInt32(_attempIdName);
                var weddingId = HttpContext.Session.GetString(_weddingIdName);
                WeddingDbContext ctx = new WeddingDbContext();

                var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
                ctx.SaveChanges();

                return View(new Contact { Email = wedding.Email, Number = wedding.Number });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
        public IActionResult WantProgram()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WantProgram([Bind("Email,Number")] Contact contact)
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            weddingAttemp.Email = contact.Email;
            weddingAttemp.Number = contact.Number;
            ctx.SaveChanges();
            return View();
        }

        public IActionResult Program()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Question1()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
            if (!wedding.Questions.Split(";").Contains("1"))
            {
                return RedirectToAction("Thanks");
            }
            return View();
        }

        public IActionResult Question1Yes()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            weddingAttemp.Question1 = 1;
            ctx.SaveChanges();
            var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
            if (!wedding.Questions.Split(";").Contains("2"))
            {
                return RedirectToAction("Thanks");
            }
            return RedirectToAction("Question2");
        }

        public IActionResult Question1No()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            weddingAttemp.Question1 = 0;
            ctx.SaveChanges();
            var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
            if (!wedding.Questions.Split(";").Contains("2"))
            {
                return RedirectToAction("Thanks");
            }
            return RedirectToAction("Question2");
        }

        public IActionResult Question2()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            var wedding = ctx.Set<Wedding>().FirstOrDefault(w => w.Identifikator == weddingId);
            if (!wedding.Questions.Split(";").Contains("2"))
            {
                return RedirectToAction("Thanks");
            }
            return View();
        }

        public IActionResult Question2Yes()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            weddingAttemp.Question2 = 1;
            ctx.SaveChanges();
            return RedirectToAction("Thanks");
        }

        public IActionResult Question2No()
        {
            var attempId = HttpContext.Session.GetInt32(_attempIdName);
            var weddingId = HttpContext.Session.GetString(_weddingIdName);
            WeddingDbContext ctx = new WeddingDbContext();
            var weddingAttemp = ctx.Set<WeddingAttemp>().FirstOrDefault(w => w.Id == attempId);
            weddingAttemp.Question2 = 0;
            ctx.SaveChanges();
            return RedirectToAction("Thanks");
        }

        public IActionResult Thanks()
        {
            return View();
        }

        public IActionResult WrongId()
        {
            return View();
        }

        public IActionResult MissYou()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
