using MovieCatalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.Service.Dto
{
    public class TitleDto
    {
        public int Id { get; set; }
        public string TConst { get; set; }
        public string PrimaryTitle { get; set; }
        public string? OriginalTitle { get; set; }
        public string TitleType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public float? AverageRating { get; set; }
        public int NumberOfVotes { get; set; }
        public List<int>? GenreIds { get; set; }
    }
}
