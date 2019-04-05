using System;
using BookInspector.Data.Context;
using BookInspector.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using BookInspector.Console.Commands.UserCommands;
using BookInspector.Services;
using BookInspector.Console.Commands.AuthorCommands;
using System.Net.Http;

namespace BookInspector.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //  Command Line Interface
            // new Builder().AppBuilder();

            //var httpClient = new HttpClient();
            //var books = httpClient.GetStringAsync("https://www.googleapis.com/books/v1/volumes?q=asimov&printType=books&maxResults=1");

            //JSON

            //var date = books["items"]["volumeInfo"]["publishedDate"];


            ////context
            //var context = new BookInspectorContext();

            //var list = context.Category.Select(x => x.Name).ToList();


            //context.Dispose();


            List<string> arguments = new List<string>();
            BookInspectorContext bic = new BookInspectorContext();

            //Add User
            //List<string> arguments = new List<string>();
            //BookInspectorContext bic = new BookInspectorContext();

            ////AddUser addUCmd = new AddUser(new UserService(bic));
            ////arguments = new List<string>();
            ////arguments.Add("Edo");

            ////AddUser addUserCmd = new AddUser(new UserService(bic));
            ////System.Console.WriteLine(addUserCmd.Execute(arguments));

            //AddAuthor
            ////IUserService userService = new UserService(bic);

            ////arguments = new List<string>();
            ////arguments.Add("Ernest Hemingway");
            ////AddAuthor addAuth = new AddAuthor(new AuthorService(bic));
            ////System.Console.WriteLine(addAuth.Execute(arguments));


            //Add Publisher

            IPublisherService userService = new PublisherService(bic);

            arguments = new List<string>();
            arguments.Add("Apress");
            AddPublisher addPublisher = new AddPublisher(new PublisherService(bic));
            System.Console.WriteLine(addPublisher.Execute(arguments));



        }
    }
}

