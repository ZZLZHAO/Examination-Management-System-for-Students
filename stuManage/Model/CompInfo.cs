using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Model
{
    public class CompInfo
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string compname { set; get; }
   
        public string compremark { set; get; }
        public int compnumber { set; get; }
        
        public string compstarttime { set; get; }
       
        public string signddltime { set; get; }
 
        public string compendTime { set; get; }



    }
}
