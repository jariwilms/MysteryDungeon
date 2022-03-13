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
