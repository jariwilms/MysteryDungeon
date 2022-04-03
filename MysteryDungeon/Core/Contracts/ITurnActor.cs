using System;

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
