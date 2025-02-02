using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Domain.Models
{
    public class Tank
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
    }
}
