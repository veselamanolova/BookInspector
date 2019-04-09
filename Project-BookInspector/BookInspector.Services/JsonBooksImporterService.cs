namespace BookInspector.Services
{
    using BookInspector.Data.Models;
    using BookInspector.Services.Contracts;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

                List<string> categories= FindCategories(bookToken);
                if (categories == null || categories.Count == 0) // skip importing this book without authors
                    continue;

                string publisher = FindPublisher(bookToken);
                if (string.IsNullOrEmpty(publisher)) // skip importing this book without ISBN
                    continue;

                string isbn = FindIsbn(bookToken);
                if (string.IsNullOrEmpty(isbn)) // skip importing this book without ISBN
                    continue;

              

              string publishedDateAsString = FindPublishedDateAsString(bookToken);
                if (string.IsNullOrEmpty(publishedDateAsString)) // skip importing this book without ISBN
                    continue;
                DateTime publishedDate = ConvertStringToDate(publishedDateAsString);

                int volumeId = 1;

                string pageCountAsString = FindPageCount(bookToken);
                if (string.IsNullOrEmpty(publishedDateAsString)) // skip importing this book without ISBN
                    continue;
                int.TryParse(pageCountAsString, out int pageCount); 


                string description = FindDescription(bookToken);
                if (string.IsNullOrEmpty(description)) // skip importing this book without ISBN
                    continue;


                var book = _bookService.AddBook(title, authors, categories, publisher, publishedDate, isbn, volumeId, pageCount, description);
                result.Add(book);
            }

            return result;
        }

        private string FindPageCount(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.pageCount").ToString();
        }

        private string FindDescription(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.description").ToString();
        }

        private string FindPublishedDateAsString(JToken bookToken)
        {
           return bookToken.SelectToken("volumeInfo.publishedDate").ToString();    
           
        }

        private DateTime ConvertStringToDate(string dateAsString)
        {
            int[] dateAsArr = dateAsString.ToString().Split('-').Select(int.Parse).ToArray();
            if (dateAsArr.Count() == 1)
            {
                return new DateTime(dateAsArr[0], 1,1);
            }

            return new DateTime(dateAsArr[0], dateAsArr[1] , dateAsArr[2] );

        }

        private string FindPublisher(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.publisher").ToString();
        }

        private static string FindTitle(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.title").ToString();
        }

        private static List<string> FindCategories(JToken bookToken)
        {
            var categories = new List<string>();
            foreach (JToken categoryToken in bookToken.SelectToken("volumeInfo.categories"))
            {
                categories.Add(categoryToken.ToString());
            }
            return categories;
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
