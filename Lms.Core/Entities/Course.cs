using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; }

        //Nav prop
        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
