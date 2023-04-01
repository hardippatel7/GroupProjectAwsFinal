using AdminBooksPanel.Models;
using AdminBooksPanel.Services;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBooksPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDynamoDBContext _context;
        private readonly IBookService _bookService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDynamoDBContext context, IBookService bookService)
        {
            _bookService = bookService;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var books = await _context.ScanAsync<Books>(default).GetRemainingAsync();
            var books = await _bookService.GetAll();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
