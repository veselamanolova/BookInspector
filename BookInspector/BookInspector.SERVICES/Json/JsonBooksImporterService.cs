﻿namespace BookInspector.SERVICES.Json
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
                //if (categories == null || categories.Count == 0) // skip importing this book without categories                   

                string publisher = FindPublisher(bookToken);
                if (string.IsNullOrEmpty(publisher)) // skip importing this book without publisher
                    continue;

                string isbn = FindIsbn(bookToken);           

              

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
            var previewLink = bookToken.SelectToken("volumeInfo.previewLink");
            if (previewLink != null)
            {
                return bookToken.SelectToken("volumeInfo.previewLink").ToString();
            }
            else
            {
                return ""; 
            }
        }

        private string FindImageUrl(JToken bookToken)
        {
            var thumbnail = bookToken.SelectToken("volumeInfo.imageLinks.thumbnail");
            if (thumbnail != null)
            {
                return bookToken.SelectToken("volumeInfo.imageLinks.thumbnail").ToString();
            }
            else
            {
                return ""; 
            }
            
        }

        private string FindShortDescription(JToken bookToken)
        {
            var shortDescription = bookToken.SelectToken("searchInfo.textSnippet");
            if (shortDescription != null)
            {
                shortDescription.ToString(); 
            }
            return "";
        }

        private string FindPageCount(JToken bookToken)
        {
            var pageCount = bookToken.SelectToken("volumeInfo.pageCount");
            if (pageCount != null)
            {
                return pageCount.ToString();
            }
            else return "0";            
        }

        private string FindDescription(JToken bookToken)
        {
            var description = bookToken.SelectToken("volumeInfo.description");
            if (description != null)
            {
                return bookToken.SelectToken("volumeInfo.description").ToString();
            }
            else
            {
                return ""; 
            }
            
       
        }

        private string FindPublishedDateAsString(JToken bookToken)
        {
            var publishDate = bookToken.SelectToken("volumeInfo.publishedDate");
            if (publishDate != null)
            {
                return bookToken.SelectToken("volumeInfo.publishedDate").ToString();
            }
            else
            {
                return ""; 
            }
           
        }

        private DateTime ConvertStringToDate(string dateAsString)
        {
            int[] dateAsArr = dateAsString.ToString().Split('-').Select(int.Parse).ToArray();
            var dateElements = dateAsArr.Count(); 

            if (dateAsArr.Count() == 1)
            {
                return new DateTime(dateAsArr[0], 1,1);
            }

            else if(dateAsArr.Count() == 2)
            {
                return new DateTime(dateAsArr[0], dateAsArr[1], 1);
            }

            else if (dateAsArr.Count() == 3)
            {
                return new DateTime(dateAsArr[0], dateAsArr[1], 1);
            }

            return new DateTime();

        }

        private string FindPublisher(JToken bookToken)
        {
            var publisher = bookToken.SelectToken("volumeInfo.publisher");
            if (publisher != null)
            {
                return bookToken.SelectToken("volumeInfo.publisher").ToString();
            }
            else
            {
                return ""; 
            }
        }

        private static string FindTitle(JToken bookToken)
        {
            var title = bookToken.SelectToken("volumeInfo.title");
            if (title != null)
            {
                return bookToken.SelectToken("volumeInfo.title").ToString();
            }
            return ""; 
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
            if (authorsToken != null)
            {
                foreach (JToken authorToken in authorsToken)
                {
                    authors.Add(authorToken.ToString());
                }
            }

            return authors;
        }

        private static string FindIsbn(JToken bookToken)
        {
            string isbn = null;
            var identifiersTockens = bookToken.SelectToken("volumeInfo.industryIdentifiers");

            if (identifiersTockens != null)
            {
                foreach (JToken identifierToken in identifiersTockens)
                {
                    string identifierType = identifierToken["type"].ToString();
                    if (identifierType == "ISBN_13")
                    {
                        isbn = identifierToken["identifier"].ToString();
                        break;
                    }
                }
            }
            return isbn;
        }
    }
}
