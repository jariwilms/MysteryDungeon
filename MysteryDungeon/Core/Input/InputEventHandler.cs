using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace MysteryDungeon.Core.Input
{
    public static class InputEventHandler
    {
		private static List<Command> _commands;
		private static KeyboardState _keyboardState;

		private static Command _upCommand;
		private static Command _downCommand;

		public static event Action OnUpCommand;

		static InputEventHandler()
		{
			_upCommand = new Command(KeyAction.Up, Keys.W);
			_downCommand = new Command(KeyAction.Down, Keys.S);

			_commands = new List<Command>();
			_commands.Add(_upCommand);
			_commands.Add(_downCommand);
		}

        public static void AddEventListener(KeyAction keyAction, Action action, bool capture = true)
        {
			Command command = keyAction switch
			{
				KeyAction.Up => _upCommand, 
				KeyAction.Down => _downCommand, 
				_ => throw new Exception("The given KeyAction does not exist."), 
			};

			command.Actions += action;
			command.Actions -= action;
        }

		public static void Update()
		{
			_keyboardState = Keyboard.GetState();

			_commands.ForEach(command =>
			{
				if (_keyboardState.IsKeyDown(command.Key))
					command.Execute();
			});
		}
	}
}
