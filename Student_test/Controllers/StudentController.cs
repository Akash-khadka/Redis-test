using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache _distributedCache; //to set and get data to and from cache
        public StudentController(IStudentRepository studentRepository, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _studentRepository = studentRepository;
            this.memoryCache = memoryCache;
           _distributedCache = distributedCache;   
        }
        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                var cacheKey = "studentsList";
                string serializedStudentsList;
                var studentList = new List<StudentsModel>();
                var redisStudentsList = await _distributedCache.GetAsync(cacheKey);
                if (redisStudentsList != null)
                {
                    serializedStudentsList = Encoding.UTF8.GetString(redisStudentsList);
                    studentList = JsonConvert.DeserializeObject<List<StudentsModel>>(serializedStudentsList);
                }
                else
                {
                    studentList = await _studentRepository.GetStudents();
                    serializedStudentsList = JsonConvert.SerializeObject(studentList);
                    studentList = JsonConvert.DeserializeObject<List<StudentsModel>>(serializedStudentsList);
                    redisStudentsList = Encoding.UTF8.GetBytes(serializedStudentsList);
                    //var options = new DistributedCacheEntryOptions();
                    //    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))//expiration time of cached object
                    //    .SetSlidingExpiration(TimeSpan.FromMinutes(2));//expiration time of cached object if not being requested
                    //await _distributedCache.SetAsync(cacheKey, redisStudentsList, options);
                    await _distributedCache.SetAsync(cacheKey, redisStudentsList/*, options*/);
                }
                return Ok(studentList);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
