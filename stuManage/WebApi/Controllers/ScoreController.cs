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
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _ScoreService;

        public ScoreController(ScoreService ScoreService)
        {
            this._ScoreService = ScoreService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ApiResult>> GetList()
        {
            List<Score> List = await _ScoreService.FindItemList();
            if (List == null)
                return ApiResultHelper.Error("There is no any Scores information in DB");
            return ApiResultHelper.Success(List);
        }

        [HttpPost("GetByExamid")]
        public async Task<ActionResult<ApiResult>> GetByExamid([FromBody] Helper value)
        {
            var data = await _ScoreService.FindItemList(c => c.id == value.id && c.examid == value.examid);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            return ApiResultHelper.Success(data);
        }
        [HttpPost("GetById")]
        public async Task<ActionResult<ApiResult>> GetById([FromBody] Helper value)
        {
            var data = await _ScoreService.FindItemList(c => c.id == value.id);
            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            data.Sort();
            foreach (Score p in data)
            {
                p.totalGrade = p.chinese + p.math + p.english + p.physics
                + p.chemistry + p.biology + p.politics + p.history + p.geography;
            }
            foreach (Score p in data)
            {
                string examId = p.examid;
                List<Score> scores = await _ScoreService.FindItemList(c => c.examid == examId);
                scores.Sort();
                int perRank = 1;
                foreach (Score score in scores)
                {
                    if (score.id == p.id)
                    {
                        p.rank = perRank;
                        break;
                    }
                    perRank += 1;
                }
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("GetAllByExamid")]
        public async Task<ActionResult<ApiResult>> GetAllByExamid([FromBody] Helper value)
        {
            List<Score> data = await _ScoreService.FindItemList(c => c.examid == value.examid);

            if (data == null)
                return ApiResultHelper.Error("This Score information does not exist.");
            data.Sort();
            foreach (Score p in data)
            {
                p.totalGrade = p.chinese + p.math + p.english + p.physics
                + p.chemistry + p.biology + p.politics + p.history + p.geography;
            }
            return ApiResultHelper.Success(data);
        }
        [HttpDelete("DeleteById")]
        public async Task<ActionResult<ApiResult>> DeleteByScoreId(Helper value)
        {
            bool res = await _ScoreService.DeleteItem(c => c.id == value.id);

            if (!res) return ApiResultHelper.Error("failed to delete");
            return ApiResultHelper.Success(res);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<ApiResult>> Insert([FromBody] Score value)
        {
            Score data = await _ScoreService.FindItem(c => c.id == value.id && c.examid == value.examid);
            if (data != null)
                return ApiResultHelper.Error("Same information exits");
            bool res = await _ScoreService.InserItem(value);
            if (!res)
                return ApiResultHelper.Error("Fail to add: Server error occurred");
            return ApiResultHelper.Success(res);
        }
    }
}
