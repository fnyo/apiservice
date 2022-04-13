using AutoMapper;
using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Dto;
using Fnyo.Learn.Jenkins.Entity;
using Fnyo.Learn.Jenkins.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("api/score")]
    public class ScoreController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TmsDbContext _dbContext;
        public ScoreController(IMapper mapper,
            TmsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        [HttpGet("obscure")]
        public async Task<IEnumerable<StudentScoreDto>> Obscure(string obscure)
        {
            var scores = await _dbContext.StudentScores.Where(score=>
            score.StudentName.Contains(obscure)).ToListAsync();

            return _mapper.Map<List<StudentScoreDto>>(scores);

        }

        [HttpGet("list")]
        public async Task<IEnumerable<StudentScoreDto>> Get()
        {
            var scores = await _dbContext.StudentScores.AsQueryable().ToListAsync();
            return _mapper.Map<List<StudentScoreDto>>(scores);
        }


        [HttpPost("add")]
        public async Task<ApiResult> Add(StudentScore score)
        {
            //var bookEntity = _mapper.Map<Book>(book);
            await _dbContext.StudentScores.AddAsync(score);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Message = "添加成功",
                Success = true
            };
        }


        [HttpPut("update")]
        public async Task<ApiResult> Update(StudentScore score)
        {
            //var scoreEntity = await _dbContext.StudentScores.FirstOrDefaultAsync(x => x.Id.Equals(score.Id));
            //if (scoreEntity == null)
            //{
            //    return new ApiResult()
            //    {
            //        Success = false,
            //        Message = "更新失败"
            //    };
            //}
            //_mapper.Map<StudentScore, StudentScore>(score, scoreEntity);
            _dbContext.Update(score);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = "更新成功"
            };
        }

        [HttpDelete("delete/{id}")]
        public async Task<ApiResult> Delete(int id)
        {
            var scoreEntity = await _dbContext.StudentScores.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (scoreEntity == null)
            {
                return new ApiResult()
                {
                    Success = false,
                    Message = "删除失败"
                };
            }
            _dbContext.StudentScores.Remove(scoreEntity);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = "删除成功"
            };
        }
    }
}
