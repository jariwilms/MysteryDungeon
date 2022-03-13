namespace MysteryDungeon.Core.Input
{
    public class InputEventListener
    {
        private static InputEventHandler s_inputHandler;

        public InputEventListener()
        {
            s_inputHandler = InputEventHandler.Instance;
        }
    }
}
