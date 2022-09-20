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
using AutoMapper;
using Microsoft.Extensions.Logging;
using Lms.Data.Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Lms.Common.Dto;

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
        public async Task<ActionResult<CourseDto>> PutCourse(int id, CourseDto courseDto)
        {
            if (id != courseDto.Id)
            {
                return BadRequest();
            }
            var course = mapper.Map<Course>(courseDto);
            uow.CourseRepository.Update(course);

            CourseDto dto;
            try
            {
                await uow.CompleteAsync();
                dto = mapper.Map<CourseDto>(course);
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

            return Ok(dto);
        }

        // PATCH: api/Courses/5
        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {
            var courseEntity = await uow.CourseRepository.FindAsync(courseId);
            if (courseEntity is null)
            {
                return BadRequest();
            }

            var dto = mapper.Map<CourseDto>(courseEntity);

            patchDocument.ApplyTo(dto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(dto))
                return BadRequest(ModelState);

            mapper.Map(dto, courseEntity);           

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(dto);
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto insertCourse)
        {
            var enitityCourse = mapper.Map<Course>(insertCourse);
            uow.CourseRepository.Add(enitityCourse);
            await uow.CompleteAsync();

            //halvonödig mappning tillbaka? kan bara skicka tillbaka insertCourse, men skulle kunna vara om CourseRepository ändrar mer än Id i framtiden
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
