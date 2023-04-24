﻿namespace Kurs_EF.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Adress Adress { get; set; }
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
        
    }
}
