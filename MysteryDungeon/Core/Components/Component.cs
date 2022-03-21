namespace MysteryDungeon.Core.Components
{
    public class Component
    {
        public GameObject Parent { get; protected set; }
        public Transform Transform;

        public bool Enabled;

        public const int UnitSize = 24;

        public Component()
        {
            Transform = new Transform();
        }

        public Component(Transform transform) : this()
        {
            Transform = transform;
        }
    }
}
