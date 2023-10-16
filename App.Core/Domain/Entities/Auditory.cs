using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain.Entities
{
    public  class Auditory
    {
        public DateTime DateAdd { get; set; }
        public DateTime DateUpd { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public bool Active { get; set; }

    }


}
