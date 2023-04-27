using Microsoft.EntityFrameworkCore;

namespace Kurs_EF.Entities
{
    public class Adress
    {
        public Guid Id { get; set; }
        public string Country {get; set; }
        public string City { get; set; }    
        public string Street { get; set; }
        public string PostalCode { get; set; } 

        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public Coordinate Coordinates { get; set; }
    }

    // [Owned]  //Pierwszy sposób na stworzenie powiązania KEYLESS - drugi przez konfigurację w MyBoardsContext
    public class Coordinate
    {
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
