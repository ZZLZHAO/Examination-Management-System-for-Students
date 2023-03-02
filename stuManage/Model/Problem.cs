using SqlSugar;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Problem : IComparable<Problem>
    {
        
        [SugarColumn(IsPrimaryKey = true)]
        public string quesid { set; get; }
      
        public string qdate { set; get; }
      
        public string id { set; get; }
        public int likeNum { set; get; }
        public string content { set; get; }

        public int CompareTo(Problem other)
        {
            if (this.likeNum == other.likeNum)
                return 0;
            else if (this.likeNum > other.likeNum)
                return -1;
            else
                return 1;
        }
    }

}
