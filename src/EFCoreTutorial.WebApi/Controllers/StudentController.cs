using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreTutorial.Data.Context;
using EFCoreTutorial.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var students= await applicationDbContext.Students.ToListAsync();
           return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            //StudentAddress sa = new StudentAddress()
            //{
            //    City = "cOr",
            //    District = "dEn",
            //    Country = "dEn",
            //    FullAddress = "fullAddress"
            //};
            //await applicationDbContext.Students.AddAsync(sa);
            //await applicationDbContext.SaveChangesAsync();

            Student st = new Student()
            {
                FirstName = "eSra",
                LastName = "bAy",
                Address = new StudentAddress()
                {
                    City = "cOr",
                    District = "dEn",
                    Country = "dEn",
                    FullAddress = "fullAddress"
                },
                BirthDate = DateTime.Now,
                Number = 1
            };
            await applicationDbContext.Students.AddAsync(st);
            await applicationDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await applicationDbContext.Students.FirstOrDefaultAsync(i => i.Id ==id);
            //var student = await applicationDbContext.Students.FindAsync(id);
            //var student = await applicationDbContext.Students.Where(i => i.Id == id).SingleOrDefaultAsync();
            
            applicationDbContext.Students.Remove(student);
            await applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update()
        {
            var student = await applicationDbContext.Students.FirstOrDefaultAsync();

            
            student.FirstName = "Updated";
            student.LastName = "Updated";


            
            await applicationDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
