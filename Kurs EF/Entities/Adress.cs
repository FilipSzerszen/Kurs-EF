﻿namespace Kurs_EF.Entities
{
    public class Adress
    {
        public Guid Id { get; set; }
        public string Country {get; set; }
        public string City { get; set; }    
        public string Street { get; set; }
        public string PostalCode { get; set; } 

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
