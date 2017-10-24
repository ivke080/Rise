using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    static class Collision
    {
        public static bool PlayerToPlatform(Player player, Platform platform)
        {
            if (player.Bounds.X + player.Bounds.Width >= platform.Bounds.X)
            {
                if (player.Bounds.X <= platform.Bounds.X + platform.Bounds.Width)
                {
                    if (player.Bounds.Y + player.Bounds.Height >= platform.Bounds.Y)
                    {
                        if (player.Bounds.Y + player.Bounds.Height < platform.Bounds.Y + platform.Bounds.Height)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
