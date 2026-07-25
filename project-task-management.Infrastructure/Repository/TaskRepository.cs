using Microsoft.EntityFrameworkCore;
using project_task_management.Application.Interface.Repository;
using project_task_management.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_task_management.Infrastructure.Repository
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task> , ITaskRepository
    {
        private readonly DbSet<Domain.Entities.Task> _tasks;
        public TaskRepository (ApplicationDbContext dbContext) : base(dbContext)
        {
            _tasks = dbContext.Set<Domain.Entities.Task>() ;
        } 
    }
}
