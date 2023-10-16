namespace App.Core.Domain.Entities
{
    public class PermissionType: Auditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }

}
