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
    public class AnswerController : ControllerBase
    {
        private readonly AnswerService _AnswerService;

        public AnswerController(AnswerService AnswerService)
        {
            this._AnswerService = AnswerService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Answer> List = await _AnswerService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Answers information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Answer value)
        {
            Answer data = await _AnswerService.FindItem(c => c.id == value.id && c.title == value.title);
            if (data != null)
                return ApiResultHelper.Error("Same information exits");
            bool res = await _AnswerService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
        [HttpPost("GetByTitle")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            List<Answer> data = await _AnswerService.FindItemList(c => c.title == value.title);
            if (data == null)
                return ApiResultHelper.Error("This Answer information does not exist.");
            return ApiResultHelper.Success(data);
        }

    }
}
