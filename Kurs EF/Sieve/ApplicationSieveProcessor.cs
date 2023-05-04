using Kurs_EF.Entities;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Kurs_EF.Sieve
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Epic>(e => e.Priority)
                .CanSort()
                .CanFilter(); 

            mapper.Property<Epic>(e=>e.Area)
                .CanSort()
                .CanFilter();

            mapper.Property<Epic>(e => e.StartDate)
                .CanSort()
                .CanFilter();

            mapper.Property<Epic>(e => e.Author.FullName)
                .CanSort()
                .CanFilter()
                .HasName("authorFullName"); // konwertuje pole z EpicDto na konkretny link do właściwości
                                            // zrozumiałej dla Sieve "Author.FullName" -> "authorFullName"

            return mapper;
        }
    }
}
