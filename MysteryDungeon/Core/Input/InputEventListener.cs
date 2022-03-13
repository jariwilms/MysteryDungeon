using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Input
{
    public class InputEventListener
    {
        private static InputEventHandler _inputHandler;

        public InputEventListener()
        {
            _inputHandler = InputEventHandler.Instance;
        }
    }
}
