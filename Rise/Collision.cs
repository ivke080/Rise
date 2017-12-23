using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Rise.Platforms;

namespace Rise
{
    static class Collision
    {
        public static bool SinglePlatform(Player player, Platform platform)
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

        public static void ManyPlatforms(Player player, Platform[] platforms)
        {
            bool collision = false;
            for (int i = 0; i < platforms.Length; i++)
            {
                if (SinglePlatform(player, platforms[i]))
                {
                    if (player.Falling)
                    {
                        player.Stop();
                        player.Falling = false;
                        player.Platform = platforms[i];

                        if (platforms[i] is StaticPlatform)
                        {
                            StaticPlatform sp = (StaticPlatform)platforms[i];
                            if (sp.Type == StaticPlatformType.Lava)
                                player.Jump(300);
                        }
                    }
                    collision = true;
                }
            }
            if (!collision)
            {
                player.OnGround = false;
                player.Platform = null;
            }
        }
    }
}
