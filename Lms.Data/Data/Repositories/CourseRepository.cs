using Lms.Core.Entities;
using Lms.Core.Repositories;
using System;
using System.Collections.Generic;
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

        public Task<bool> AnyAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> FindAsync(int? id)
        {
            return await db.Course.FindAsync(id);
        }

        public Task<IEnumerable<Course>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetCourse(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Course course)
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
