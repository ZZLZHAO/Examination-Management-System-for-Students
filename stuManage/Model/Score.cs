using System;
using SqlSugar;
namespace Model
{
    public class Score : IComparable<Score>
    {
        public string id { get; set; }
        public string examid { get; set; }
        public string name { get; set; }

        public float chinese { get; set; }
  
        public float math { get; set; }

        public float english { get; set; }

        public float physics { get; set; }

        public float chemistry { get; set; }

        public float biology { get; set; }

        public float politics { get; set; }

        public float history { get; set; }

        public float geography { get; set; }

        [SugarColumn(IsIgnore = true)]
        public float totalGrade { get; set; }
        [SugarColumn(IsIgnore = true)]
        public float rank { get; set; }
        public int CompareTo(Score other)
        {
            float myTotal = this.chinese + this.math + this.english + this.physics
                + this.chemistry + this.biology + this.politics + this.history + this.geography;
            float otherTotal = other.chinese + other.math + other.english + other.physics
                + other.chemistry + other.biology + other.politics + other.history + other.geography;

            if (this.examid.CompareTo(other.examid) > 0)
                return -1;
            else if (this.examid.CompareTo(other.examid) < 0)
                return 1;
            else if (myTotal > otherTotal)
                return -1;
            else if (myTotal < otherTotal)
                return 1;
            else
                return 0;
        }
    }
}