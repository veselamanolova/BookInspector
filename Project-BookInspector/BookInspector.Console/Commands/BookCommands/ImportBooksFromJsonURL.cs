namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ImportBooksFromJsonURL : ICommand
    {
        private readonly IJsonBooksImporterService _jsonBooksImporterService;

        public ImportBooksFromJsonURL(IJsonBooksImporterService jsonBooksImporterService)
        {
            _jsonBooksImporterService = jsonBooksImporterService ?? throw new ArgumentNullException(nameof(jsonBooksImporterService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (args.Count < 1)
            {
                throw new ArgumentException("Please provide JSON file path");
            }

            string filePath = args[0];
            var books = _jsonBooksImporterService.ImportBooks(filePath, true);

            return $"{books.Count} books imported.";
        }
    }
}