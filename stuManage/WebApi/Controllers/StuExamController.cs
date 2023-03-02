using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Result;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;



namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class StuExamController : ControllerBase
    {
        private static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST='127.0.0.1')(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##wyf;Password=test;";
        #region 执行SQL语句,返回受影响行数
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        #region 执行SQL语句,返回DataTable;只用来执行查询结果比较少的情况
        public static DataTable ExecuteDataTable(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    return datatable;
                }
            }
        }
        #endregion


        private readonly StuExamService _StuExamService;

        public StuExamController(StuExamService StuExamService)
        {
            this._StuExamService = StuExamService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<StuExam> List = await _StuExamService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any StuExams information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] StuExam value)
        {
            StuExam data = await _StuExamService.FindItem(c => c.id == value.id && c.examid == value.examid && c.subject == value.subject);
            if (data != null)
                return ApiResultHelper.Error("Same information exits");
            bool res = await _StuExamService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpPost("GetByNameAndId")]
        public String GetByNameAndId([FromBody] Helper value)
        {
            string sql = "select * from(" + "select * " +
                "from examinfo natural join stuexam " +
                "where examid=" + "'" + value.examid + "'" +
                "and " + "id=" + "'" + value.id + "')";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());
            return JsonConvert.SerializeObject(t);
        }
        [HttpPost("GetById")]
        public String GetById([FromBody] Helper value)
        {
            string sql = "select * from(" + "select * " +
                "from examinfo natural join stuexam " +
                "where " + "id=" + "'" + value.id + "')";
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());
            Console.WriteLine(t);
            return JsonConvert.SerializeObject(t);
        }
        [HttpGet("GetProblem")]
        public String GetP()
        {
            string sql = "select * from(" + "select quesid,issuedate,id,likeNum,content,name " +
                "from Problem natural join Customer )";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());

            return JsonConvert.SerializeObject(t);
        }

        [HttpGet("GetReply")]
        public String GetR()
        {
            string sql = "select * from(" + "select replyid,quesid,date,id,likeNum,content,name " +
                "from Reply natural join Customer) ";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());

            return JsonConvert.SerializeObject(t);
        }


        [HttpGet("GetAll")]
        public String GetAll()
        {
            string sql = "select * from(" + "select * " +
                "from （(" + "select replyid,quesid,rdate,id,likeNum,content,name " +
                "from Reply natural join Customer)） right outer join (" + "select quesid,qdate,id,likeNum,content,name " +
                "from Problem natural join Customer ) using(quesid) ) ";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());

            return JsonConvert.SerializeObject(t);
        }

        [HttpPost("GetProblemId")]
        public String GetPid([FromBody] Helper value)
        {
            string sql = "select * from(" + "select * " +
                "from ((" + "select replyid,quesid,rdate,id,likeNum,content,name " +
                "from Reply natural join Customer)) right outer join (" + "select quesid,qdate,id,likeNum,content,name " +
                "from Problem natural join Customer ) using(quesid)  "+"where id=" + "'" + value.id + "'" + ") ";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());

            return JsonConvert.SerializeObject(t);
        }

        [HttpPost("GetReplyId")]
        public String GetRid([FromBody] Helper value)
        {

            string sql = "select * from(" + "select * " +
                "from ((" + "select replyid,quesid,rdate,id,likeNum,content,name " +
                "from Reply natural join Customer)) right outer join (" + "select quesid,qdate,id,likeNum,content,name " +
                "from Problem natural join Customer ) using(quesid)  " + "where id=" + "'" + value.id + "'" + ") ";
            Console.WriteLine(sql);
            DataTable t = StuExamController.ExecuteDataTable(sql, new OracleParameter());

            return JsonConvert.SerializeObject(t);
        }
    }
}
