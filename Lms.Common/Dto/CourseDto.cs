namespace Lms.Api.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get
            {
                return StartDate.AddMonths(3);
            }
        }
    }
}
