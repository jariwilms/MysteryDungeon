using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Controllers
{
    public class AnimatorController
    {
        public string CurrentState { get; protected set; }

        private List<State> _states;
        private Dictionary<string, dynamic> _parameterValues;

        public AnimatorController()
        {
            _states = new List<State>();
            _parameterValues = new Dictionary<string, dynamic>();
        }

        public State AddState(string stateName)
        {
            State state = new State(stateName);
            _states.Add(state);

            return state;
        }

        public void AddParameter<T>(string parameterName, T value = default)
        {
            _parameterValues.Add(parameterName, value);
        }

        public void SetParameter<T>(string parameterName, T value)
        {
            if (!_parameterValues.ContainsKey(parameterName))
                throw new Exception(String.Format("The given key is not valid: {0}", parameterName));

            _parameterValues[parameterName] = value;
        }

        public void SetDefaultState(string state)
        {
            CurrentState = state;
        }

        public void Update()
        {
            foreach (var state in _states)
            {
                if (!state.IsTrue(_parameterValues))
                    continue;

                CurrentState = state.Name;
                break;
            }
        }
    }
}
