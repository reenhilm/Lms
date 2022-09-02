namespace Lms.Api.Dto
{
    public class ModuleDto
    {
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get
            {
                return StartDate.AddMonths(1);
            }
        }
    }
}
