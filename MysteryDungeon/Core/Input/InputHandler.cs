using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace MysteryDungeon.Core.Input
{
    public sealed class InputHandler
    {
        public static readonly InputHandler Instance = new InputHandler();

		private List<Command> _commands;
		private Dictionary<KeyAction, Keys> _keyTable;

        private KeyboardState _keyboardState;

        static InputHandler()
        {

        }

        private InputHandler()
		{
			_commands = new List<Command>();
			_keyTable = new Dictionary<KeyAction, Keys>();

			foreach (KeyAction key in Enum.GetValues(typeof(KeyAction)))
				_commands.Add(new Command(key));

            _keyTable.Add(KeyAction.Confirm, Keys.None);
            _keyTable.Add(KeyAction.Cancel, Keys.None);
            _keyTable.Add(KeyAction.Escape, Keys.None);

            _keyTable.Add(KeyAction.Up, Keys.W);
            _keyTable.Add(KeyAction.Right, Keys.D);
            _keyTable.Add(KeyAction.Down, Keys.S);
            _keyTable.Add(KeyAction.Left, Keys.A);
        }

        public void AddEventListener(KeyAction keyAction, Action action)
        {
			foreach (var command in _commands)
            {
                if (command.KeyAction == keyAction)
                {
                    command.Actions += action;
                    break;
                }
            }
        }

		public void Update()
		{
            _keyboardState = Keyboard.GetState();

            _commands.ForEach(command =>
            {
                if (_keyboardState.IsKeyDown(_keyTable[command.KeyAction]))
                    command.Execute();
            });
        }
	}
}
