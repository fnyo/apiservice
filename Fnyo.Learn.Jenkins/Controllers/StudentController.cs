using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Entity;
using Fnyo.Learn.Jenkins.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController:ControllerBase
    {
        private readonly TmsDbContext _dbContext;
        public StudentController(TmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("list")]
        public async Task<List<Student>> GetList()
        {
            try
            {
                return await _dbContext.Students.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        [HttpGet("get")]
        public async Task<List<Student>> GetStudentsByName([FromQuery]string name)
        {
            var students =await _dbContext.Students.Where(x => x.Name.Contains(name)).ToListAsync();
            return students;
        }

 
        [HttpPost("add")]
        public async Task<ApiResult>  Add(Student student)
        {
           var res= await _dbContext.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = "添加成功"
            };
        }

        [HttpPut("update")]
        public async Task<ApiResult> Update(Student student)
        {
            var isDuplicate = await _dbContext.Students.CountAsync(x =>x.Id!=student.Id && x.Number.Equals(student.Number)) > 0;
            if(isDuplicate)
            {
                return new ApiResult()
                {
                    Message = $"学号：{student.Number}已经存在！",
                    Success = false
                };
            }


            var res =  _dbContext.Update<Student>(student);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new ApiResult() { Message = "更新成功", Success = true });
        }



        [HttpDelete("delete/{id}")]
        public async Task<ApiResult> Delete(int id)
        {
             var entity = _dbContext.Students.Where(x=>x.Id.Equals(id)).FirstOrDefault();
             if(entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(new ApiResult() { Message = "删除成功", Success = true });
        }
    }
}
