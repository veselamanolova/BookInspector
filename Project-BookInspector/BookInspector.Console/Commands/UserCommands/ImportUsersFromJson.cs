namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ImportUsersFromJson : ICommand
    {
        private readonly IJsonUsersImporterService _jsonUsersImporterService;

        public ImportUsersFromJson(IJsonUsersImporterService jsonUsersImporterService)
        {
            _jsonUsersImporterService = jsonUsersImporterService ?? throw new ArgumentNullException(nameof(jsonUsersImporterService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (args.Count < 1)
            {
                throw new ArgumentException("Please provide JSON file path");
            }

            string filePath = args[0];
            var users = _jsonUsersImporterService.ImportUsers(filePath);

            return $"{users.Count} users imported.";
        }
    }
}