namespace BookListCodeSample.Data.Models
{
    public class Favorite
    {
        public int fav_id { get; set; }
        public int fav_bk_id { get; set; }
        public string fav_name { get; set; } = string.Empty;
        public DateTime fav_created { get; set; }
    }
}
