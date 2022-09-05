using System.ComponentModel.DataAnnotations;

namespace Lms.Api.Dto
{
    public class ModuleDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get
            {
                return StartDate.AddMonths(1);
            }
        }
    }
}
