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
            StudentFilter sf = new StudentFilter() {FirstName = "emre"};

            var student = applicationDbContext.Students.AsQueryable();

            if (!string.IsNullOrEmpty(sf.FirstName))
                student = student.Where(i => i.FirstName == sf.FirstName);
            if (!string.IsNullOrEmpty(sf.LastName))
                student = student.Where(i => i.LastName == sf.LastName);
            if (sf.Number.HasValue)
                student = student.Where(i => i.Number == sf.Number);

            var list =await student.ToListAsync();



            var allStudents = await applicationDbContext.Students.ToListAsync();



            var lastNameFilter = await applicationDbContext.Students
                .Where(i => i.LastName == "bayrak" /*&& i.FirstName!="fatma"*/)
                .Where(i=>i.FirstName=="fatma")
                .OrderByDescending(i=>i.Number)
                .ToListAsync();



            var studentsFilter = await applicationDbContext.Students
                .Select(i => i.FirstName)
                .ToListAsync();



            var students = await applicationDbContext.Students.ToListAsync();
            return Ok();
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await applicationDbContext.Students.FirstOrDefaultAsync(i => i.Id == id);
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
