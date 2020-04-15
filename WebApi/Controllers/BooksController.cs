using BookStore.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{    
    [Authorize]
    public class BooksController : ODataController
    {
        private readonly IDatabase _redis;
        private readonly BookService _bookService;

        public BooksController(BookService bookService,RedisHelper redisHelper)
        {
            _redis = redisHelper.GetDatabase();
            _bookService = bookService;
        }
         

        [EnableQuery]
        public List<Book> GetBooks()
        {
            return _bookService.Get();
        }
        [EnableQuery]
        public Book GetBook([FromODataUri] int key)
        {
            string strBook = _redis.StringGet("book" + key.ToString());
            Book book = JsonSerializer.Deserialize<Book>(strBook);
            if (book != null)
            {
                return book;
            }

            book = _bookService.Get(key);

            if (book == null)
            {
                return null;
            }
            string json = JsonSerializer.Serialize<Book>(book);
            _redis.StringSet("book" + key.ToString(), json);
            return book;
        }

        public Book Post(Book book)
        {
            return _bookService.Create(book); 
            
        }


        public IActionResult Delete([FromODataUri] int key)
        {
            var book = _bookService.Get(key);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }


    }
}
