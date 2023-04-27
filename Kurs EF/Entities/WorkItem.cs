using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kurs_EF.Entities
{
    public class Epic : WorkItem
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class Issue : WorkItem
    {
        public decimal Effort { get; set; }
    }
    public class Task : WorkItem
    {
        [MaxLength(200)]
        public string Acctivity { get; set; }
        [Precision(14, 2)]
        public decimal RemainingWork { get; set; }
    }
    public abstract class WorkItem
    {
        public int Id { get; set; }
        public virtual WorkItemState State { get; set; }
        public int StateId { get; set; }
        public string Area { get; set; }
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual User Author { get; set; }
        public Guid AuthorID { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
