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
    public class StudentController : ControllerBase
    {
        private readonly StudentService _StudentService;

        public StudentController(StudentService StudentService)
        {
            this._StudentService = StudentService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Student> List = await _StudentService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Students information in DB");
            return ApiResultHelper.Success(List);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Student value)
        {
            Student data = await _StudentService.FindItem(t => value.id == t.id);
            if (data != null)
                return ApiResultHelper.Error("Fail to add: same student exits");
            bool res = await _StudentService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteById([FromBody] Helper value)
        {
            bool res = await _StudentService.DeleteItem(c => c.id == value.id);
            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            Student data = await _StudentService.FindItem(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Student information does not exist.");
            return ApiResultHelper.Success(data);
        }

    }
}
