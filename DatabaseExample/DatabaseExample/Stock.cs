using SQLite;

namespace DatabaseExample
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Symbol { get; set; }
    }
}