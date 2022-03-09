using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core
{
    class InputEventInvoker
    {
        private static Dictionary<Keys, Action> _eventDictionary;

        static InputEventInvoker()
        {
            _eventDictionary = new Dictionary<Keys, Action>();
        }

        public static void RegisterAction(Keys key, Action action)
        {
            _eventDictionary.Add(key, action);
        }

        public static void Update()
        {
            foreach (Keys k in _eventDictionary.Keys)
            {
                if (Keyboard.GetState().IsKeyDown(k))
                    _eventDictionary[k].Invoke();
            }
        }
    }
}
