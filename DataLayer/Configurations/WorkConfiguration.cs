using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configurations
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works");
            builder.HasOne(x => x.User).WithMany(x => x.Works).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Enterprise).WithMany().OnDelete(DeleteBehavior.ClientCascade);
            builder.Property(x => x.StartDate).HasDefaultValueSql("GetDate()");

        }
    }
}
