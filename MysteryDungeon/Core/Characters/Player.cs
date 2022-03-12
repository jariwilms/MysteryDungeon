using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Input;

using Microsoft.Xna.Framework.Input;

namespace MysteryDungeon.Core.Characters
{
    public class Player : Actor
    {
        public Player(ContentManager content, Dungeon dungeon) : base()
        {
            Dungeon = dungeon;

            Sprite = new Sprite(content.Load<Texture2D>("sprites/chikorita"), UnitSize); //move naar constructor
            CreateAnimations();
            Sprite.AnimationPlayer.PlayAnimation("Idle");

            CreateActions();
        }

        protected override void CreateAnimations() //Move dit naar data class met animation data => verschillende player/enemy models
        {
            Sprite.AnimationPlayer.AddAnimation("Idle", new Animation(Sprite.Texture, new Point(98, 20), 1, 0, 16, 21, 1, 2, 0.8f));   //Move spritesheet naar animationplayer? texture verandert niet wrs
            Sprite.AnimationPlayer.AddAnimation("MoveUp", new Animation(Sprite.Texture, new Point(260, 47), 3, 0, 13, 20, 1, 2, 0.4f));
            Sprite.AnimationPlayer.AddAnimation("MoveRight", new Animation(Sprite.Texture, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
            Sprite.AnimationPlayer.AddAnimation("MoveDown", new Animation(Sprite.Texture, new Point(102, 46), 3, 0, 13, 20, 1, 2, 0.4f));
            Sprite.AnimationPlayer.AddAnimation("MoveLeft", new Animation(Sprite.Texture, new Point(175, 49), 3, 0, 20, 18, 1, 2, 0.4f)); //TODO: mogelijkheid om animation frames te stitchen, extra animation class?
        }

        protected override void CreateActions()
        {
            MoveUpAction = () => { MoveTo(MovementDirection.North); };
            MoveRightAction = () => { MoveTo(MovementDirection.East); };
            MoveDownAction = () => { MoveTo(MovementDirection.South); };
            MoveLeftAction = () => { MoveTo(MovementDirection.West); };

            InputHandler.Instance.AddEventListener(KeyAction.Up, MoveUpAction);
            InputHandler.Instance.AddEventListener(KeyAction.Right, MoveRightAction);
            InputHandler.Instance.AddEventListener(KeyAction.Down, MoveDownAction);
            InputHandler.Instance.AddEventListener(KeyAction.Left, MoveLeftAction);
        }
    }
}
