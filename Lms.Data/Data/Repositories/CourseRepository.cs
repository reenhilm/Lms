using Lms.Core.Entities;
using Lms.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;
        public CourseRepository(LmsApiContext db)
        {
            this.db = db;
        }
        public void Add(Course course)
        {
            db.Add(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Course.AnyAsync(c => c.Id == id);
        }

        public async Task<Course> FindAsync(int? id)
        {
            return await db.Course.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await db.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? id)
        {
            return await db.Course.FirstAsync(c => c.Id == id);
        }

        public void Remove(Course course)
        {
            db.Course.Remove(course);
        }

        public void Update(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
        }
    }
}
