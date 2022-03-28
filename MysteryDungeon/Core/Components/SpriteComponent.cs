using MysteryDungeon.Core.Animations;

namespace MysteryDungeon.Core.Components
{
    public class SpriteComponent : Component
    {
        public Sprite Sprite { get; set; }

        public SpriteComponent(GameObject parent) : base(parent)
        {
            Sprite = new Sprite();
        }
    }
}
