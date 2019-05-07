namespace BookInspector.SERVICES.Json
{
    using BookInspector.DATA.Models;    
    using BookInspector.SERVICES.Contracts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class JsonBooksImporterService : IJsonBooksImporterService
    {
        private readonly IBookService _bookService;
        

        public JsonBooksImporterService(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public List<Book> ImportBooks(string path, bool isFromWeb)
        {
            List<Book> result = new List<Book>();
            string jsonContent; 

            if (isFromWeb)
            {
                using (WebClient httpClient = new WebClient())
                {
                    jsonContent = httpClient.DownloadString(path);       
                }
               
            }

            else
            {
                if (!File.Exists(path))
                {
                    throw new ArgumentException("File does not exist", nameof(path));
                }
                jsonContent = File.ReadAllText(path);

            }
           
            JObject jObject; 
            try
            {
                 jObject = JObject.Parse(jsonContent);
            }
            catch (Exception e)
            {

                throw new ArgumentException($"Json file cannot be parsed. {e.Message}", nameof(path));
            }
            
            foreach (JToken bookToken in jObject["items"].Children())
            {
                string title = FindTitle(bookToken);
                if (string.IsNullOrEmpty(title)) // skip importing this book without title
                    continue;

                List<string> authors = FindAuthors(bookToken);
                if (authors == null || authors.Count == 0) // skip importing this book without authors
                    continue;

                List<string> categories= FindCategories(bookToken);
                if (categories == null || categories.Count == 0) // skip importing this book without categories
                    continue;

                string publisher = FindPublisher(bookToken);
                if (string.IsNullOrEmpty(publisher)) // skip importing this book without publisher
                    continue;

                string isbn = FindIsbn(bookToken);
                if (string.IsNullOrEmpty(isbn)) // skip importing this book without ISBN
                    continue;

              

              string publishedDateAsString = FindPublishedDateAsString(bookToken);
                if (string.IsNullOrEmpty(publishedDateAsString)) // skip importing this book without ISBN
                    continue;
                DateTime publishedDate = ConvertStringToDate(publishedDateAsString);                

                string pageCountAsString = FindPageCount(bookToken);
                if (string.IsNullOrEmpty(publishedDateAsString)) // skip importing this book without publishedDate
                    continue;
                int.TryParse(pageCountAsString, out int pageCount); 


                string description = FindDescription(bookToken);
                if (string.IsNullOrEmpty(description)) // skip importing this book without description
                    continue;

                string shortDescription = FindShortDescription(bookToken);
                if (string.IsNullOrEmpty(description)) // skip importing this book without description
                    continue;

                string imageUrl = FindImageUrl(bookToken);
                if (string.IsNullOrEmpty(description)) // skip importing this book without description
                    continue;

                string previewLink = FindPreviewLink(bookToken);
                if (string.IsNullOrEmpty(description)) // skip importing this book without description
                    continue;

                var book = _bookService.AddBook(title, authors, categories, 
                                                publisher, publishedDate, 
                                                isbn, imageUrl, description,
                                                shortDescription, previewLink);
                result.Add(book);
            }

            return result;
        }

        private string FindPreviewLink(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.previewLink").ToString();
        }

        private string FindImageUrl(JToken bookToken)
        {
            return bookToken.SelectToken("volumeInfo.imageLinks.thumbnail").ToString();
        }

        private string FindShortDescription(JToken bookToken)
        {
            return bookToken.SelectToken("searchInfo.textSnippet").ToString();
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
            var categoriesToken = bookToken.SelectToken("volumeInfo.categories");
            if (categoriesToken != null)
            {
                foreach (JToken categoryToken in categoriesToken)
                {
                    categories.Add(categoryToken.ToString());
                }
            }           
            return categories;
        }

        private static List<string> FindAuthors(JToken bookToken)
        {
            var authors = new List<string>();

            var authorsToken = bookToken.SelectToken("volumeInfo.authors"); 
            foreach (JToken authorToken in authorsToken)
            {
                authors.Add(authorToken.ToString());
            }
            return authors;
        }

        private static string FindIsbn(JToken bookToken)
        {
            string isbn = null;
            var identifiersTockens = bookToken.SelectToken("volumeInfo.industryIdentifiers"); 

            foreach (JToken identifierToken in identifiersTockens)
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
