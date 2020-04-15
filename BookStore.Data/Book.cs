using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data
{
    public class Book
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        //public decimal Price { get; set; }

        //public string Category { get; set; }

        //public string Author { get; set; }
    }
}
