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
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.ToTable("Enterprises");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name);

            builder.HasData(
                    new Enterprise { Id = 1, Name = "Global Tech Solutions" },
                    new Enterprise { Id = 2, Name = "Innovative Minds Inc." },
                    new Enterprise { Id = 3, Name = "Bright Future Holdings" },
                    new Enterprise { Id = 4, Name = "Urban Solutions LLC" },
                    new Enterprise { Id = 5, Name = "Green Energy Partners" },
                    new Enterprise { Id = 6, Name = "NextGen Robotics" },
                    new Enterprise { Id = 7, Name = "PureTech Industries" },
                    new Enterprise { Id = 8, Name = "Skyline Consulting Group" },
                    new Enterprise { Id = 9, Name = "Quantum Innovations" },
                    new Enterprise { Id = 10, Name = "Horizon Financial Corp." },
                    new Enterprise { Id = 11, Name = "Vertex Marketing Solutions" },
                    new Enterprise { Id = 12, Name = "EcoVision Solutions" },
                    new Enterprise { Id = 13, Name = "OptiHealth Pharmaceuticals" },
                    new Enterprise { Id = 14, Name = "BlueWave Enterprises" },
                    new Enterprise { Id = 15, Name = "Dynamic Logistics Ltd." },
                    new Enterprise { Id = 16, Name = "Peak Performance Advisors" },
                    new Enterprise { Id = 17, Name = "Smart Grid Technologies" },
                    new Enterprise { Id = 18, Name = "Luminous Media Group" },
                    new Enterprise { Id = 19, Name = "AlphaTech Labs" },
                    new Enterprise { Id = 20, Name = "Elite Software Solutions" },
                    new Enterprise { Id = 21, Name = "Fusion Global Partners" },
                    new Enterprise { Id = 22, Name = "EcoLite Solutions" },
                    new Enterprise { Id = 23, Name = "Prime Venture Group" },
                    new Enterprise { Id = 24, Name = "Visionary Ventures Inc." },
                    new Enterprise { Id = 25, Name = "NexWave Analytics" },
                    new Enterprise { Id = 26, Name = "StarLink Industries" },
                    new Enterprise { Id = 27, Name = "Core Digital Solutions" },
                    new Enterprise { Id = 28, Name = "Apex Renewable Energy" },
                    new Enterprise { Id = 29, Name = "Infinite Horizons Corp." },
                    new Enterprise { Id = 30, Name = "Phoenix Cybersecurity" },
                    new Enterprise { Id = 31, Name = "TerraNova Biotech" },
                    new Enterprise { Id = 32, Name = "AstroLabs Technologies" },
                    new Enterprise { Id = 33, Name = "Insight Data Solutions" },
                    new Enterprise { Id = 34, Name = "Purewater Holdings" },
                    new Enterprise { Id = 35, Name = "Omega Construction Group" },
                    new Enterprise { Id = 36, Name = "Epicure Food Services" },
                    new Enterprise { Id = 37, Name = "Nova Telecom Solutions" },
                    new Enterprise { Id = 38, Name = "Sapphire Tech Group" },
                    new Enterprise { Id = 39, Name = "BrightPath Consultants" },
                    new Enterprise { Id = 40, Name = "Precision Engineering Inc." },
                    new Enterprise { Id = 41, Name = "Sushimi" },
                    new Enterprise { Id = 42, Name = "Amazon" },
                    new Enterprise { Id = 43, Name = "Google" },
                    new Enterprise { Id = 44, Name = "Costco" },
                    new Enterprise { Id = 45, Name = "Sams Club" },
                    new Enterprise { Id = 46, Name = "Walmart" },
                    new Enterprise { Id = 47, Name = "Bodega Aurrera" }
                );
        }
    }
}
