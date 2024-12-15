using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Mancala.PL.Entities
{
    public class tblGame : IEntity
    {

        public Guid Id { get; set; }
        public Guid Player1Id { get; set; }
        public Guid Player2Id { get; set; }
        public Guid CurrentTurn { get; set; }
        public string Status { get; set; }
        public bool IsGameOver { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<tblMove> Moves { get; set; } = new List<tblMove>();
        public virtual ICollection<tblPit> Pits { get; set; } = new List<tblPit>();
        public string SortField { get { return Status; } }

    }
}
