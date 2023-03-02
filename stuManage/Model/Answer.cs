using SqlSugar;
using System;

namespace Model
{
    public class Answer
    {
        
        public string id { get; set; }
        
        public string title { get; set; }
        public int answer1 { get; set; }
        public int answer2 { get; set; }
        public int answer3 { get; set; }
        public int answer4 { get; set; }
    }

}
