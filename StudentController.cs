using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtToken.DAL;
using JwtToken.Models;

namespace AngularCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : ControllerBase
    {


        private readonly StudentDbContext db;
        public StudentController(StudentDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {

            int MODE = 0;

            object[] param = { new SqlParameter("@MODE", MODE) };

            var students = await db.StudentLists.FromSqlRaw("SP_STUDENTMAS @MODE", param).ToArrayAsync();
            //var studentsa = await db.Database.ExecuteSqlRawAsync("SP_STUDENTMAS @MODE", param);

            if (students.Count() == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpPost]
        //[Route("PostStudents")]
        //public async Task<IActionResult> PostStudents(string StudentNo, string Name, string FatherName, string MobileNo, string Email)
        public async Task<IActionResult> PostStudents(StudentList studentLists)
        {

            int MODE = 1;

            object[] param = { new SqlParameter("@MODE", MODE), new SqlParameter("@CODE", studentLists.Code),
            new SqlParameter("@STUDENTNO", studentLists.StudentNo),new SqlParameter("@NAME", studentLists.Name),
            new SqlParameter("@FATHERNAME", studentLists.FatherName),new SqlParameter("@EMAIL", studentLists.Email),
            new SqlParameter("@MOBILENO", studentLists.MobileNo)};

            await db.Database.ExecuteSqlRawAsync("SP_STUDENTMAS @MODE,@CODE,@STUDENTNO,@NAME,@FATHERNAME,@EMAIL,@MOBILENO", param);
            return Ok();
        }
        [HttpPut]
        //[Route("PutStudents")]
        public async Task<IActionResult> PutStudents(StudentList studentLists)
        {

            int MODE = 2;

            object[] param = { new SqlParameter("@MODE", MODE),new SqlParameter("@CODE", studentLists.Code),
            new SqlParameter("@STUDENTNO", studentLists.StudentNo),new SqlParameter("@NAME", studentLists.Name),
            new SqlParameter("@FATHERNAME", studentLists.FatherName),new SqlParameter("@MOBILENO", studentLists.MobileNo),
            new SqlParameter("@EMAIL", studentLists.Email)};

            await db.Database.ExecuteSqlRawAsync("SP_STUDENTMAS @MODE,@CODE,@STUDENTNO,@NAME,@FATHERNAME,@EMAIL,@MOBILENO", param);
            return Ok();
        }
        [HttpDelete("{code}")]
        //[HttpDelete]
        //[Route("DeleteStudents/Code")]
        public async Task<IActionResult> DeleteStudents(int Code)
        {

            int MODE = 3;

            object[] param = { new SqlParameter("@MODE", MODE), new SqlParameter("@CODE", Code) };

            await db.Database.ExecuteSqlRawAsync("SP_STUDENTMAS @MODE,@CODE", param);
            return Ok();
        }
    } 
}
