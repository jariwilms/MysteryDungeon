using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MysteryDungeon.Core
{
    class Utility
    {
        public static Vector2 GetMousePositionFromPoint(int y, int x)
        {
            return new Vector2(Mouse.GetState().X - x / 2, -Mouse.GetState().Y + y / 2);
        }

        public static double GetMouseAngleFromPoint(int y, int x) //Y moet soms ge-invert worden => Fix
        {
            Vector2 mousePosition = GetMousePositionFromPoint(y, x);
            return Math.Atan2(-mousePosition.Y, mousePosition.X);
        }

        public static bool KeyPressedOnce(Keys key)
        {
            return MysteryDungeon.KeyboardState.IsKeyDown(key) && !MysteryDungeon.LastkeyboardState.IsKeyDown(key);
        }
    }
}
