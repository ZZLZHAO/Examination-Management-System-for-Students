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
    public class CompInfoController : ControllerBase
    {
        private readonly CompInfoService _CompInfoService;

        public CompInfoController(CompInfoService CompInfoService)
        {
            this._CompInfoService = CompInfoService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<CompInfo> List = await _CompInfoService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any CompInfos information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] CompInfo value)
        {
            var data = await _CompInfoService.FindItem(c => c.compname == value.compname);
            if (data != null) return ApiResultHelper.Error("This CompInfo information does not exist.");
            bool res = await _CompInfoService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromBody] CompInfo value)
        {
            bool res = await _CompInfoService.DeleteItem(c => c.compname == value.compname);
            if (!res) return ApiResultHelper.Error("This CompInfo information does not exist.");

            await _CompInfoService.InserItem(value);
            return ApiResultHelper.Success(res);
        }


        [HttpPost("GetByName")]
        public async Task<ActionResult<ApiResult>> GetByName(Helper value)
        {
            List<CompInfo> data = await _CompInfoService.FindItemList(c => c.compname == value.compname);
            if (data == null) return ApiResultHelper.Error("don't exist");
            return ApiResultHelper.Success(data);
        }
        [HttpDelete("DeleteByName")]
        public async Task<ActionResult<ApiResult>> DeleteByName(Helper value)
        {
            CompInfo data = (await _CompInfoService.FindItem(c => c.compname == value.compname));
            if (data == null) return ApiResultHelper.Error("This CompInfo information does not exist.");

            bool res = await _CompInfoService.DeleteItem(c => c.compname == value.compname);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }


    }
}
