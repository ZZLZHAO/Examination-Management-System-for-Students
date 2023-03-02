using SqlSugar;
using System;

namespace Model
{
    public class Reply
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string replyid { set; get; }
        public string  quesid { get; set; }
        public string rdate { set; get; }
        public string id { set; get; }
        public int likeNum { set; get; }
        public string content { set; get; }

    }

}
