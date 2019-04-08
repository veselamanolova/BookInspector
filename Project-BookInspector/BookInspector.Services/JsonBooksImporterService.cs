﻿namespace BookInspector.Services
{
    using BookInspector.Data.Models;
    using BookInspector.Services.Contracts;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class JsonBooksImporterService : IJsonBooksImporterService
    {
        private readonly IBookService _bookService;

        public JsonBooksImporterService(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public List<Book> ImportBooks(string filePath)
        {
            List<Book> result = new List<Book>();

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist", nameof(filePath));
            }
            string fileContent = File.ReadAllText(filePath);
            JObject jObject = JObject.Parse(fileContent);
            foreach (JToken bookToken in jObject["items"].Children())
            {
                string title = FindTitle(bookToken);
                if (string.IsNullOrEmpty(title)) // skip importing this book without title
                    continue;

                List<string> authors = FindAuthors(bookToken);
                if (authors == null || authors.Count == 0) // skip importing this book without authors
                    continue;

                string isbn = FindIsbn(bookToken);
                if (string.IsNullOrEmpty(isbn)) // skip importing this book without ISBN
                    continue;


             //   var book = _bookService.AddBook(title, authors);
             //  result.Add(book);
            }

            return result;
        }

        private static string FindTitle(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.title").ToString();
        }

        private static List<string> FindAuthors(JToken bookToken)
        {
            var authors = new List<string>();
            foreach (JToken authorToken in bookToken.SelectToken("volumeInfo.authors"))
            {
                authors.Add(authorToken.ToString());
            }
            return authors;
        }

        private static string FindIsbn(JToken bookToken)
        {
            string isbn = null;
            foreach (JToken identifierToken in bookToken.SelectToken("volumeInfo.industryIdentifiers"))
            {
                string identifierType = identifierToken["type"].ToString();
                if (identifierType == "ISBN_13")
                {
                    isbn = identifierToken["identifier"].ToString();
                    break;
                }
            }
            return isbn;
        }
    }
}
