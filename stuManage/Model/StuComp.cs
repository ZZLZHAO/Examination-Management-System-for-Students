using SqlSugar;
namespace Model
{
    public class StuComp
    {
        [SugarColumn(IsPrimaryKey = true, OracleSequenceName = "seq_id")]
        public int  itemId { get; set; }
        public string id { get; set; }
        public string compname { get; set; }
        public string signstatus { get; set; }

        [SugarColumn(IsIgnore = true)]
        public CompInfo compInfo { get; set; }
    }
}
