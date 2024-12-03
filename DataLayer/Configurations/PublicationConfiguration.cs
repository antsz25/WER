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
    public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable("Publications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GetDate()");
            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Enterprise);
            builder.HasMany(x => x.Comments).WithOne(x => x.Publication).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(x => x.Attachments).WithOne(x => x.Publication).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
