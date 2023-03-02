using SqlSugar;
namespace Model
{
    public class Student
    {
       
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public string classid { get; set; }
    }
}
