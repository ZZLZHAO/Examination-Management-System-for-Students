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
    public class ProblemController : ControllerBase
    {
        private readonly ProblemService _ProblemService;

        public ProblemController(ProblemService ProblemService)
        {
            this._ProblemService = ProblemService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Problem> list = await _ProblemService.FindItemList();
            if (list == null)
                return ApiResultHelper.Error("There is no any Problems information in DB");
            list.Sort();
            return ApiResultHelper.Success(list);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _ProblemService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Problem information does not exist.");
            data.Sort();
            return ApiResultHelper.Success(data);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Helper value)
        {
            Problem data = (await _ProblemService.FindItem(c => c.quesid == value.quesid));
            if (data == null) return ApiResultHelper.Error("This Problem information does not exist.");

            data.likeNum += 1;
            await _ProblemService.DeleteItem(c => c.quesid == value.quesid);
            await _ProblemService.InserItem(data);
            return ApiResultHelper.Success(true);
        }

        [HttpPost("Insert")]       // 自增序列解决
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Problem value)
        {
            bool res = await _ProblemService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }


    }
}
