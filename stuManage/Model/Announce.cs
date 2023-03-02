using SqlSugar;
using System;

namespace Model
{
    public class Announce : IComparable<Announce>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string title { get; set; }
        
        public string text { get; set; }
        
        public string startDate { get; set; }
        public int CompareTo(Announce other)
        {
            if (this.startDate == other.startDate)
                return 0;
            else if (this.startDate.CompareTo(other.startDate) > 0)
                return -1;
            else
                return 1;
        }
    }
}
