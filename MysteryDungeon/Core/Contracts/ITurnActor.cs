using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Contracts
{
    public interface ITurnActor
    {
        public void StartTurn();
        public void EndTurn();

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public bool IsPerforming { get; set; }
    }
}
