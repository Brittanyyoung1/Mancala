using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Mancala.PL.Entities
{
    public class tblMove : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }


        public int MoveNo { get; set; }
        public int SourcePit { get; set; }
        public int StonesMoved { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsExtraTurn { get; set; }

        public virtual tblGame Game { get; set; }
        public virtual tblPlayer Player { get; set; }

        public string SortField => throw new NotImplementedException();

    }
}
