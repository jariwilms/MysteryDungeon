using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Input
{
    public sealed class InputEventHandler
    {
        public static readonly InputEventHandler Instance = new InputEventHandler();

        private List<Command> _commands;
        private Dictionary<KeyAction, Keys> _keyTable;

        private KeyboardState _keyboardState;
        private KeyboardState _lastKeyboardState;

        private MouseState _mouseState;
        private MouseState _lastMouseState;

        static InputEventHandler()
        {

        }

        private InputEventHandler()
        {
            _commands = new List<Command>(); //move commands naar inputeventlistener? + maak list aan van listeners => iterate top to bottom voor event capturing
            _keyTable = new Dictionary<KeyAction, Keys>();

            foreach (KeyAction key in Enum.GetValues(typeof(KeyAction)))
                _commands.Add(new Command(key));

            _keyTable.Add(KeyAction.Confirm, Keys.None);
            _keyTable.Add(KeyAction.Cancel, Keys.None);

            _keyTable.Add(KeyAction.Up, Keys.W);
            _keyTable.Add(KeyAction.Right, Keys.D);
            _keyTable.Add(KeyAction.Down, Keys.S);
            _keyTable.Add(KeyAction.Left, Keys.A);

            _keyTable.Add(KeyAction.Escape, Keys.Escape);
            _keyTable.Add(KeyAction.None, Keys.None);

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Rebind a key to a new action
        /// </summary>
        /// <param name="keyAction"></param>
        /// <param name="newKey"></param>
        public void RebindKeyAction(KeyAction keyAction, Keys newKey, bool swap = true)
        {
            KeyAction oldAction = KeyAction.None;

            if (swap)
            {
                foreach (var k in _keyTable)
                {
                    if (k.Value == newKey)
                    {
                        oldAction = k.Key;
                        break;
                    }
                }
            }

            if (oldAction != KeyAction.None)
                _keyTable[oldAction] = _keyTable[keyAction];

            _keyTable[keyAction] = newKey;
        }

        public void AddEventListener(KeyAction keyAction, Action action)
        {
            foreach (Command command in _commands)
            {
                if (command.KeyAction == keyAction)
                {
                    command.Actions += action;
                    break;
                }
            }
        }

        public void RemoveEventListener(Action action)
        {

        }

        public bool IsKeyPressed(Keys key)
            => _keyboardState.IsKeyDown(key);

        public bool IsKeyPressedOnce(Keys key)
            => _keyboardState.IsKeyDown(key) && !_lastKeyboardState.IsKeyDown(key);

        public Vector2 GetMousePosition()
            => new Vector2(_mouseState.X, _mouseState.Y);

        public bool MouseScrollUp()
            => _mouseState.ScrollWheelValue > _lastMouseState.ScrollWheelValue;

        public bool MouseScrollDown()
            => _mouseState.ScrollWheelValue < _lastMouseState.ScrollWheelValue;

        public void Update()
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            _commands.ForEach(command =>
            {
                if (_keyboardState.IsKeyDown(_keyTable[command.KeyAction]))
                    command.Execute();
            });
        }
    }
}
