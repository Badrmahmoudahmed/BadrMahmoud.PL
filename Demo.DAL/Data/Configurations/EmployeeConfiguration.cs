using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Id).IsRequired();
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(E => E.Gender).HasConversion(
                    (Gender) => Gender.ToString(),
                    (GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString)
                );
        }
    }
}
