using MovieCatalog.Data.Entitites;

namespace MovieCatalog.Data.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string TConst => $"tt{Id.ToString().PadLeft(7, '0')}";
        public string PrimaryTitle { get; set; }
        public TitleType TitleType { get; set; }
        public string OriginalTitle { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RunTimeMinutes { get; set; }
    }
}
