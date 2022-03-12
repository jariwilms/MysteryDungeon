using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace MysteryDungeon.Core.Input
{
    public class Command
    {
        public event Action Actions;
        public KeyAction KeyAction;

        public bool Capture;

        public Command(KeyAction keyAction, bool capture = true)
        {
            KeyAction = keyAction;
            Capture = capture;
        }

        public void Execute()
        {
            Actions?.Invoke();
        }
    }
}
