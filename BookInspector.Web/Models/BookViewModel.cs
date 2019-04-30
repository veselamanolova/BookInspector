
namespace BookInspector.Web.Models
{
    using System.Collections.Generic;

    public class BookViewModel
    {
        public int _id { get; set; }

        public string _title { get; set; }

        //public List<string> _author { get; set; }

        //public List<string> _category { get; set; }

        //public double _rating { get; set; }

        public string _ISBN { get; set; }

        public string _description { get; set; }

        public IReadOnlyList<BookViewModel> _results { get; set; } = new List<BookViewModel>();
    }
}

