using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBooksPanel.Models
{
    [DynamoDBTable("books")]
    public class Books
    {
        [DynamoDBHashKey]
        [Display(Name = "Book Id")]
        public string bookId { get; set; }

        [Display(Name = "Book Image")]
        public string thumbnail { get; set; }

        [Display(Name = "Book Name")]
        public string bookName { get; set; }

        [Display(Name = "Book Author")]
        public string bookAuthor { get; set; }

        [Display(Name = "Book Genre")]
        public string bookGenre { get; set; }

        [Display(Name = "Book Price")]
        public Double Price { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime publishDate { get; set; }

    }
}
