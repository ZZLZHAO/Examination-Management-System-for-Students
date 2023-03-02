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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _QuestionService;

        public QuestionController(QuestionService QuestionService)
        {
            this._QuestionService = QuestionService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Question> List = await _QuestionService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Questions information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Question value)
        {
            var data = await _QuestionService.FindItem(c => c.title == value.title);
            if (data != null) return ApiResultHelper.Error("This Question information does not exist.");
            bool res = await _QuestionService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }


        [HttpGet("GetLatest")]
        public async Task<ActionResult<ApiResult>> GetLatest()
        {
            List<Question> list = await _QuestionService.FindItemList();
            list.Sort();
            return ApiResultHelper.Success(list[0]);
        }



    }
}
