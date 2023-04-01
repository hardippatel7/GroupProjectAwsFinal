using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace AdminBooksPanel.Models
{
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey]
        [Display(Name = "userName")]
        public string userName { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "firstname")]
        public string firstname { get; set; }
        [Display(Name = "lastname")]
        public string lastname { get; set; }
        [Display(Name = "isAdmin")]
        public bool isAdmin { get; set; }
    }

    /*public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int isAdmin { get; set; }
    }*/
}
