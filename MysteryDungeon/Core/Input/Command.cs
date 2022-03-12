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
        public KeyAction KeyAction;
        public Keys Key;
        public event Action Actions;

        public Command(KeyAction keyAction, Keys key)
        {
            KeyAction = keyAction;
            Key = key;
        }

        public void SetKey(Keys newKey)
        {
            Key = newKey;
        }

        public void Execute()
        {
            Actions?.Invoke();
        }
    }
}
