using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{

    public class Permission: Auditory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }
        public int PermissionTypeId { get; set; }
        public virtual PermissionType PermissionType { get; set; }
    }

}
