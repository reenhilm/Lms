using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Common.Dto
{
    public class ModuleInsertDto
    {
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddMonths(1);
            }
        }
        //FK
        public int CourseId { get; set; }
    }
}
