using MysteryDungeon.Core.Controllers;

namespace MysteryDungeon.Core.Test
{
    internal class Test
    {
        public static void Run()
        {
            AnimatorController controller = new AnimatorController();

            controller.AddParameter<float>("Speed", 1.0f);
            controller.AddParameter<bool>("IsOnGround");

            State idleState = controller.AddState("Idle");
            idleState.AddCondition("Speed", v => v == 0);

            State movingState = controller.AddState("Moving");
            movingState.AddCondition("Speed", v => v > 0);

            controller.Update();
        }
    }
}
