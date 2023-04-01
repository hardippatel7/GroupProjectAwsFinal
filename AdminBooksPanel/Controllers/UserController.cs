using AdminBooksPanel.Models;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdminBooksPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly IDynamoDBContext _context;
        public UserController(IDynamoDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginVerify(User user)
        {
            if (user.userName == "" || user.password == "")
            {
                return NotFound();
            }
            Debug.WriteLine(user.userName, user.password);
            var userData = await _context.LoadAsync<User>(user.userName);

            if (userData == null)
            {
                return NotFound();
            }
            else
            {
                if (userData.password == user.password)
                {
                    if (userData.isAdmin == true)
                    {
                        return RedirectToAction("Index", "Books");
                    }
                    return RedirectToAction( "Books", "Books");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View("Registration");
        }

        
        [HttpPost]
        public async Task<IActionResult> RegistrationVerify(User user)
        {
            user.isAdmin = false;
            await _context.SaveAsync(user);
            return View("Login");
        }
    }
}
