using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Models
{
    public class BookListViewModel
    {
        public int ID { get; set; }
        public string BookTitle { get; set; }
        public string Publisher { get; set; }
        public string Summary { get; set; }
        public string Language { get; set; }
        public string PageCount { get; set; }
        //public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }
    }
}