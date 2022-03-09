using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Input
{
    public enum ActionKeys //Todo key rebinding, gaat niet met enum gaan
    {
        ConfirmKey = Keys.Enter,
        CancelKey = Keys.Back,

        UpKey = Keys.W,
        RightKey = Keys.D,
        DownKey = Keys.S,
        LeftKey = Keys.A,

        EscapeKey = Keys.Escape,
    }

    class InputEventInvoker
    {
        private static Dictionary<ActionKeys, Action> _eventDictionary;

        static InputEventInvoker()
        {
            _eventDictionary = new Dictionary<ActionKeys, Action>();
        }

        public static void RegisterAction(ActionKeys key, Action action)
        {
            _eventDictionary.Add(key, action);
        }

        public static void Update()
        {
            foreach (ActionKeys key in _eventDictionary.Keys)
            {
                if (Keyboard.GetState().IsKeyDown((Keys)key))
                    _eventDictionary[key].Invoke();
            }
        }
    }
}
