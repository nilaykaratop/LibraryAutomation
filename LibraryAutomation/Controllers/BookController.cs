using LibraryAutomation.Models;
using LibraryAutomation.Models.Dto;
using LibraryAutomation.Models.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAutomation.Controllers
{
    public class BookController : ApiController
    {
        BooksEntities db = new BooksEntities();
        public IEnumerable<BookListViewModel> GetAllBooks()
        {
            IEnumerable<BookListViewModel> bookList = db.BookList.ToList().Select(x => new BookListViewModel()
            {
                BookTitle = x.Book_Title,
                Publisher = x.Publisher,
                ID = x.ID,
                Summary = x.Summary,
                Language = x.Language,
                PageCount = x.PageCount,
                AuthorName = GetAuthorName(x.AuthorID)

                //PublishDate=Convert.ToDateTime(x.PublisDate)

            });

            return bookList;

        }
        private string GetAuthorName (int? authorId) {
            if (authorId.HasValue)
            {
                var author = db.AuthorList.FirstOrDefault(x => x.ID == authorId);
                return author?.Author_Name;
            }
            return string.Empty;

            
        }
        [HttpGet]
        public Book BookByID(int id)
        {
            BookList bl = db.BookList.FirstOrDefault(b => b.ID == id);
            return new Book
            {
                ID = bl.ID,
                BookTitle = bl.Book_Title,
                Publisher = bl.Publisher,
                Summary = bl.Summary,
                Language = bl.Language,
                PageCount = bl.PageCount
                //PublishDate=Convert.ToDateTime(bl.PublisDate)

            };

        }

        [HttpPost]
        public IHttpActionResult AddBook(Book _model)
        {
            BookList bl = new BookList()
            {
                Book_Title = _model.BookTitle,
                Publisher = _model.Publisher,
                Summary = _model.Summary,
                Language = _model.Language,
                PageCount = _model.PageCount

            };
            db.BookList.Add(bl);
            if (db.SaveChanges() > 0)
            {
                return Ok<string>("Kitap kaydedildi...");

            }

            return BadRequest("bir hata oluştu");
        }
        [HttpPut]
        public IHttpActionResult Edit(int? id, Book book)
        {
            if (id != book.ID)
            {
                return BadRequest();
            }
            BookList updated = db.BookList.FirstOrDefault(b => b.ID == id.Value);

            updated.Book_Title = book.BookTitle;
            updated.Publisher = book.Publisher;
            updated.Summary = book.Summary;
            updated.Language = book.Language;
            updated.PageCount = book.PageCount;
            db.SaveChanges();
            return Ok(updated);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return Ok(HttpStatusCode.NotFound);
            }
            BookList deleted = db.BookList.FirstOrDefault(b => b.ID == id);
            if (deleted is null)
            {
                return Ok(HttpStatusCode.NotFound);
            }
            db.BookList.Remove(deleted);
            db.SaveChanges();

            return Ok(HttpStatusCode.OK);
        }

    }
}

