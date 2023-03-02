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
    public class ReplyController : ControllerBase
    {
        private readonly ReplyService _ReplyService;

        public ReplyController(ReplyService ReplyService)
        {
            this._ReplyService = ReplyService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Reply> List = await _ReplyService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Replys information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] Helper value)
        {
            Reply data = (await _ReplyService.FindItem(c => c.replyid == value.replyid));
            if (data == null) return ApiResultHelper.Error("This Reply information does not exist.");

            data.likeNum += 1;
            await _ReplyService.DeleteItem(c => c.replyid == value.replyid);
            await _ReplyService.InserItem(data);
            return ApiResultHelper.Success(true);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Reply value)
        {
            bool res = await _ReplyService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _ReplyService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Reply information does not exist.");

            return ApiResultHelper.Success(data);
        }

    }
}
