using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise.Platforms
{
    class PlatformManager
    {
        public static float DownSpeed = 8f; // platform speed, normal when player is standing still
        public static float CurrentDownSpeed = 0f; // platform speed, different when player jumps etc
        public static bool MoveDownward = false; // should move platforms downward
        public static float HorizontalMovementSpeed = 5f;
        private Platform[] _platforms;
        private const int Gap = 200;
        private Random _rand;
        private const int NumberOfPlatforms = 6;
        private ContentManager _content;

        Dictionary<String, Texture2D> _textures;

        private int _lastPlatform = 0; // index of the last platform added (highest platform)

        int _numberPassedPlatforms = 0;

        public PlatformManager(ContentManager content)
        {
            _content = content;
            _rand = new Random();
            _platforms = new Platform[NumberOfPlatforms];
            _textures = new Dictionary<string, Texture2D>();

            Load();
            Init();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _platforms.Length; i++)
            {
                _platforms[i].Update(gameTime);

                if (_platforms[i].Bounds.Y > Game1.HEIGHT + 100) // +100 if there is any collectable on the platform
                {
                    CreatePlatform((StaticPlatform)_platforms[i], i);
                    _numberPassedPlatforms++;
                }
            }

            if (_numberPassedPlatforms > 80)
            {
                _numberPassedPlatforms = 0;
                DownSpeed += 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform p in _platforms)
            {
                p.Draw(spriteBatch);
            }
        }

        /*private Platform CreatePlatform(int index)
        {
            PlatformSize size = (PlatformSize)_rand.Next(0, 3);
            string textureKey = "Solid_" + size.ToString(); // there will be more platforms, so it should be another random

            int x;

            if (size == PlatformSize.Short)
            {
                x = _rand.Next(220, Game1.WIDTH - 220); // short platform should be closer to the middle in order to jump on it
            }
            else
            {
                x = _rand.Next(130, Game1.WIDTH - 216);
            }

            int y = _platforms[_lastPlatform].Bounds.Y - Gap;

            _lastPlatform = index;

            int typeNum = _rand.Next(0, 100);

            if (typeNum > 90)
            {
                // if random bigger than 80 then lava, else regular
                return new StaticPlatform(_textures["Jump_Medium"], new Vector2(x, y), PlatformSize.Medium, StaticPlatformType.Lava);
            }

            return new StaticPlatform(_textures[textureKey], new Vector2(x, y), size, StaticPlatformType.Stone);
        }*/

        private void CreatePlatform(StaticPlatform platform, int index)
        {
            PlatformSize size = (PlatformSize)_rand.Next(0, 3);
            string textureKey = "Solid_" + size.ToString(); // there will be more platforms, so it should be another random

            bool horizontalMovement = false;

            int x;

            if (size == PlatformSize.Short)
            {
                x = _rand.Next(220, Game1.WIDTH - 220); // short platform should be closer to the middle in order to jump on it
            }
            else
            {
                x = _rand.Next(130, Game1.WIDTH - 216);
            }

            int y = _platforms[_lastPlatform].Bounds.Y - Gap;

            _lastPlatform = index;

            int typeNum = _rand.Next(0, 100);

            if (typeNum > 70)
            {
                horizontalMovement = true;
            }
            if (typeNum > 92)
            {
                // if random bigger than 90 then lava, else regular
                platform.RecreatePlatform(_textures["Jump_Medium"], x, y, PlatformSize.Medium, StaticPlatformType.Lava, horizontalMovement);
                return;
            }
            
            platform.RecreatePlatform(_textures[textureKey], x, y, size, StaticPlatformType.Stone, horizontalMovement);
        }
        
        private void Init()
        {
        
        _platforms[0] = new StaticPlatform(_textures["Dirt_Ground_Bottom"], new Vector2(0, Game1.HEIGHT - 48), PlatformSize.Ground, StaticPlatformType.Stone, false);
        for (int i = 1; i < NumberOfPlatforms; i++)
        {
            PlatformSize size = (PlatformSize)_rand.Next(0, 2);
            string textureKey = "Solid_" + size.ToString();

            int x = _rand.Next(100, Game1.WIDTH - 200);
            int y;
            if (i == 0)
            {
                y = Game1.HEIGHT - Gap;
            }
            else
            {
                y = _platforms[i - 1].Bounds.Y - Gap;
            }
            _platforms[i] = new StaticPlatform(_textures[textureKey], new Vector2(x, y), size, StaticPlatformType.Stone, false);
            _lastPlatform = i;
        }
     }

         private void Load()
         {
             _textures.Add("Solid_Long", _content.Load<Texture2D>("stone_ground_long"));
             _textures.Add("Solid_Short", _content.Load<Texture2D>("stone_ground_short"));
             _textures.Add("Solid_Medium", _content.Load<Texture2D>("stone_ground_medium"));
             _textures.Add("Dirt_Ground_Bottom", _content.Load<Texture2D>("dirt_ground_bottom"));
             _textures.Add("Jump_Medium", _content.Load<Texture2D>("jump_ground_medium"));
         }

         public Platform[] Platforms
         {
             get { return _platforms; }
         }
    }
}
