using Microsoft.AspNetCore.Identity;


namespace project_task_management.Domain.Entities.Identity
{
    public class User :IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Project>? Projects { get; set; } 
    }
}
