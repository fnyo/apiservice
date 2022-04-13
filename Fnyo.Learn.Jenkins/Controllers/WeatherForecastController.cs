using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Entity;
using Fnyo.Learn.Jenkins.Services;
using Fnyo.Learn.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly TmsDbContext _dbcontext;
        private readonly StudentService _studentService;
        private readonly HelloAopService _aopService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            StudentService studentService,
            TmsDbContext dbContext,
            HelloAopService aopService)
        {
            _logger = logger;
            _studentService = studentService;
            _dbcontext = dbContext;
            _aopService = aopService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("students")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return  await _dbcontext.Students.ToListAsync();
        }

        [HttpGet("name")]
        public async  Task<string> GetName()
        {
            return await _aopService.Castle();
            //return await _aopService.SayHello();
            //return StudentService.GetName();
        }
    }
}
