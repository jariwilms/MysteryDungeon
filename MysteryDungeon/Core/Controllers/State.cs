using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Controllers
{
    public class State
    {
        public string Name { get; set; }

        private Dictionary<string, Func<dynamic, bool>> _predicates;

        public State(string stateName)
        {
            Name = stateName;
            _predicates = new Dictionary<string, Func<dynamic, bool>>();
        }

        public void AddCondition(string parameterName, Func<dynamic, bool> condition)
        {
            _predicates.Add(parameterName, condition);
        }

        public bool IsTrue(Dictionary<string, dynamic> parameterValues)
        {
            bool b = true;

            foreach (var predicate in _predicates)
            {
                var value = parameterValues[predicate.Key];

                if (!predicate.Value.Invoke(value))
                {
                    b = false;
                    break;
                }
            }

            return b;
        }
    }
}
