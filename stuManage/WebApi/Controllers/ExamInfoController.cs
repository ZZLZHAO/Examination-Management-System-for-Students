using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Result;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamInfoController : ControllerBase
    {
        private readonly ExamInfoService _ExamInfoService;

        public ExamInfoController(ExamInfoService ExamInfoService)
        {
            this._ExamInfoService = ExamInfoService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<ExamInfo> List = await _ExamInfoService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any ExamInfos information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] ExamInfo value)
        {
            ExamInfo data = await _ExamInfoService.FindItem(c=>c.examid == value.examid && c.subject == value.subject);
            if (data != null)
                return ApiResultHelper.Error("Same information exits");
            bool res = await _ExamInfoService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
        [HttpPost("GetByExamId")]
        public async Task<ActionResult<ApiResult>> GetByExamId([FromBody] Helper value)
        {
            List<ExamInfo> List = await _ExamInfoService.FindItemList(c => c.examid == value.examid);
            if (List == null)
                return ApiResultHelper.Error("There is no any ExamInfos information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] ExamInfo value)
        {
            bool res = await _ExamInfoService.DeleteItem(c => c.subject == value.subject && c.examid == value.examid);
            if (!res) return ApiResultHelper.Error("This ExamInfo information does not exist.");

            await _ExamInfoService.InserItem(value);
            return ApiResultHelper.Success(true);
        }
    }
}
