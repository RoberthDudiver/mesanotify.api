using App.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.DTO
{
    public  class PermissionDTO:Auditory
    {

        public string Name { get; set; }

        public string Lastname { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
