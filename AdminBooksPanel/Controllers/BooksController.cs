﻿using AdminBooksPanel.Models;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shortid;
using shortid.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBooksPanel.Controllers
{
    public class BooksController : Controller
    {
        private readonly IDynamoDBContext _context;
        private readonly IAmazonS3 _s3Client;

        public BooksController(IDynamoDBContext context, IAmazonS3 s3Client)
        {
            _context = context;
            _s3Client = s3Client;
        }

        public async Task<IActionResult> Buy(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();
            return View(book);
        }


        [HttpGet("/books")]
        public async Task<IActionResult> BooksAsync()
        {
            var books = await _context.ScanAsync<Books>(default).GetRemainingAsync();

            return View(books);
        }

        [HttpGet("/admin")]
        public async Task<IActionResult> IndexAsync()
        {
            var books = await _context.ScanAsync<Books>(default).GetRemainingAsync();

            return View(books);
        }

        public IActionResult Create()
        {
            var options = new GenerationOptions(useSpecialCharacters: false, useNumbers: true);
            ViewBag.bookId = ShortId.Generate(options);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Books book,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = file.FileName;
                string objectKey = $"{fileName}";
                string books3url;
                try
                {

                    using (Stream fileToUpload = file.OpenReadStream())
                    {
                        var putObjectRequest = new PutObjectRequest();
                        putObjectRequest.BucketName = "bookstore-storage";
                        putObjectRequest.Key = objectKey;
                        putObjectRequest.InputStream = fileToUpload;
                        putObjectRequest.ContentType = file.ContentType;

                        var response = await _s3Client.PutObjectAsync(putObjectRequest);
                        books3url = GeneratePreSignedURL(objectKey);
                    }

                    book.thumbnail = books3url;
                    await _context.SaveAsync(book);
                    Console.WriteLine("Book Saved with book Id: " + book.bookId);
                    ViewBag.successmsg = $"Book added successfully with id {book.bookId}";
                    return RedirectToAction("Details", new { id = book.bookId });

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                };
            }
            return View(book);
        }

        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "bookstore-storage",
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            string url = _s3Client.GetPreSignedURL(request);
            return url;
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Books book)
        {
            if (id != book.bookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bookdata = await _context.LoadAsync<Books>(id);
                    if (bookdata == null) return NotFound();
                    await _context.SaveAsync(book);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();
            await _context.DeleteAsync(book);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();


            return View(book);
        }

        public async Task<IActionResult> CDetail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();


            return View(book);
        }

        public async Task<IActionResult> Success(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.LoadAsync<Books>(id);
            if (book == null) return NotFound();


            return View(book);
        }
    }
}
