namespace BookListCodeSample.Data.Models
{

    public class Book
    {
        public int bk_id { get; set; }
        public string bk_title { get; set; } = string.Empty;
        public string bk_author { get; set; } = string.Empty;


        public async Task SaveAsync(DBConnector db)
        {
            if (bk_id == 0)
            {
                var sql =
                "INSERT INTO Books (bk_title, bk_author) VALUES (@bk_title, @bk_author)";
                bk_id = await db.InsertData(sql, this);
            }
            else
            {
                var sql =
                "UPDATE Books SET bk_title = @bk_title, bk_author = @bk_author WHERE bk_id = @bk_id;";
                await db.SaveData(sql, this);
            }
        }


        public async Task DeleteUser(DBConnector db)
        {
            var sql = "DELETE FROM Books WHERE bk_id = @bk_id";
            await db.SaveData(sql, this);
        }
    }
}
