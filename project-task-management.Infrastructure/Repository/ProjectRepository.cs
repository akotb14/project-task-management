using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Interface.Repository;
using project_task_management.Domain.Entities;
using project_task_management.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Infrastructure.Repository
{
    public class ProjectRepository  : GenericRepository<Project> , IProjectRepository
    {
        private readonly DbSet<Project> _projects;

        public ProjectRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _projects = dbContext.Set<Project>() ;
        }
    }
}
