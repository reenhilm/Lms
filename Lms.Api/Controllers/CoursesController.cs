using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Core.Repositories;
using Lms.Api.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Lms.Data.Data.Repositories;

namespace Lms.Api.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public CoursesController(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await uow.CourseRepository.GetAllCourses();
            var dto = mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(dto);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await uow.CourseRepository.FindAsync(id);
            var dto = mapper.Map<CourseDto>(course);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            uow.CourseRepository.Update(course);

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto insertCourse)
        {
            var enitityCourse = mapper.Map<Course>(insertCourse);
            uow.CourseRepository.Add(enitityCourse);
            await uow.CompleteAsync();

            //halvonödig mappning tillbaka? skulle vara om CourseRepository ändrat mer än Id
            var dto = mapper.Map<CourseDto>(enitityCourse);

            return CreatedAtAction("GetCourse", new { id = enitityCourse.Id }, dto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await uow.CourseRepository.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            uow.CourseRepository.Remove(course);
            await uow.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> CourseExists(int id)
        {
            return await uow.CourseRepository.AnyAsync(id);
        }
    }
}
