using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Domain.DTOs
{
    public class BattleDto
    {
        public Guid Tank1Id { get; set; }
        public Guid Tank2Id { get; set; }
    }
}
