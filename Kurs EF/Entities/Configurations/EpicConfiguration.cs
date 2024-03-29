﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Kurs_EF.Entities.Configurations
{
    public class EpicConfiguration : IEntityTypeConfiguration<Epic>
    {
        public void Configure(EntityTypeBuilder<Epic> eb)
        {
            eb.Property(wi => wi.EndDate).HasPrecision(3);
        }
    }
}
