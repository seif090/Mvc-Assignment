using Company.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.context.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.Salary)
              .HasColumnType("decimal(18, 2)");
        }
    }
}
