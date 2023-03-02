using SqlSugar;
using System;

namespace Model
{
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string id { get; set; }
        public string name { get; set; }
        public string subject { get; set; }

    }

}
