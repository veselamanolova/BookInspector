
namespace BookInspector.App.Commands.Exporting
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    class ListBooksToPDF : ICommand
    {
        private IExportService _exportService;

        public ListBooksToPDF(IExportService exportService)
        {
            _exportService = exportService;
        }

        public string Execute(IReadOnlyList<string> args)
        {
            _exportService.ListBooksToPDF();

            return "Successfully created PDF!";
        }
    }
}
