using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Mancala.PL.Entities
{
    public class tblPlayer :IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }

        public virtual ICollection<tblMove> Moves { get; set; }
        public virtual ICollection<tblPit> Pits { get; set; }
        public string SortField { get { return Username; } }


    }
}
