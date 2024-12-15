using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Mancala.PL.Entities
{
    public class tblPit : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
       public Guid PlayerId { get; set; }
        public int Stones { get; set; }
        public bool IsMancala { get; set; }
        public int PitPosition { get; set; }
        public virtual tblPlayer Player { get; set; }
        public virtual tblGame Game { get; set; }

        public string SortField => throw new NotImplementedException();


    }
}
