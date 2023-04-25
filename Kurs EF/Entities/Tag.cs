namespace Kurs_EF.Entities
{
    public class Tag
    {
        public int Id { get; set; } 
        public string Value { get; set; }
        public string Category { get; set; }    
        public List<WorkItem> WorkItems { get; set; }
        //public List<WorkitemTag> WorkitemTags { get; set; } = new List<WorkitemTag>();
    }
}
