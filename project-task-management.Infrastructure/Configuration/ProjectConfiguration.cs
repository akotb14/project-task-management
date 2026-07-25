using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_task_management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Infrastructure.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.UserId).IsRequired();

            //Relations
            builder.HasOne(e => e.User)
                .WithMany(e => e.Projects)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(t=>t.Tasks)
                .WithOne(p=>p.Project)
                .HasForeignKey(e=>e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
