using MysteryDungeon.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Map
{
    public class TurnHandler
    {
        public ITurnActor CurrentEntity => _entities.ElementAt(_currentEntityIndex);
        private List<ITurnActor> _entities { get; set; }
        private int _currentEntityIndex;

        public TurnHandler()
        {
            _entities = new List<ITurnActor>();
            _currentEntityIndex = 0;
        }

        public void AddActor(ITurnActor actor)
        {
            actor.OnTurnFinished += () => Next();
            _entities.Add(actor);
        }

        public void RemoveActor(ITurnActor actor)
        {
            if (_entities.Contains(actor))
            {
                actor.OnTurnFinished -= () => Next();
                _entities.Remove(actor);
            }
        }

        public void Reset()
        {
            _entities.ForEach(entity => entity.OnTurnFinished -= () => Next());
            _entities.Clear();
        }

        public void Start()
        {
            CurrentEntity.StartTurn();
        }

        public void End()
        {
            _entities.ForEach(entity => entity.OnTurnFinished -= () => Next());
            CurrentEntity.EndTurn();
        }

        public void Next()
        {
            _currentEntityIndex = ++_currentEntityIndex % _entities.Count;
            CurrentEntity.StartTurn();
        }
    }
}
