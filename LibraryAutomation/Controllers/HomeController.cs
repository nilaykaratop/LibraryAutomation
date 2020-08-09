using LibraryAutomation.Models.Dto;
using LibraryAutomation.Models.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Controllers
{
    public class HomeController : Controller
    {
        BooksEntities db = new BooksEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            BookList bl = db.BookList.FirstOrDefault(b => b.ID == id);
            var result = new Book
            {
                ID = bl.ID,
                BookTitle = bl.Book_Title,
                Publisher = bl.Publisher,
                Summary = bl.Summary,
                Language = bl.Language,
                PageCount = bl.PageCount
                //PublishDate=Convert.ToDateTime(bl.PublisDate)

            };
            return View(result);
        }
    }
}