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
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _TeacherService;

        public TeacherController(TeacherService TeacherService)
        {
            this._TeacherService = TeacherService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Teacher> list = await _TeacherService.FindItemList();
            if (list == null)
                return ApiResultHelper.Error("There is no any Teachers information in DB");
            return ApiResultHelper.Success(list);
        }


        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Teacher value)
        {
            Teacher data = await _TeacherService.FindItem(t => value.id == t.id);
            if (data != null)
                return ApiResultHelper.Error("Fail to add: same Teacher exits");
            bool res = await _TeacherService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }


        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteById(Helper value)
        {
            bool res = await _TeacherService.DeleteItem(c => c.id == value.id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            Teacher data = await _TeacherService.FindItem(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Teacher information does not exist.");
            return ApiResultHelper.Success(data);
        }

    }
}
