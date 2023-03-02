using SqlSugar;
using System;

namespace Model
{
    public class Classes
    {
       
        [SugarColumn(IsPrimaryKey = true)]
        public string classid { get; set; }
     
        public string id { get; set; }   
    }

}
