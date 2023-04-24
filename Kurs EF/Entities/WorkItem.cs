using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurs_EF.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        public string Area { get; set; }
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        //EPIC
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //ISSUE
        public decimal Effort { get; set; }
        //TASK 
        [MaxLength(200)]
        public string Acctivity { get; set; }
        [Precision(14,2)]
        public decimal RemainingWork { get; set; }

        public string Type { get; set; }

        public List<Comment> Comments { get; set;} = new List<Comment>();
        public User Author { get; set; }
        public Guid AuthorID { get; set; }



        public List<Tag> Tags { get; set; }
        //public List<WorkitemTag> WorkitemTags { get; set; } = new List<WorkitemTag>();
    }
}
