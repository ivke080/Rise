using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise.Platforms
{
    class PlatformManager
    {
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

                if (_platforms[i].Bounds.Y > Game1.HEIGHT)
                {
                    _platforms[i] = CreatePlatform(i);
                    _numberPassedPlatforms++;
                }
            }

            if (_numberPassedPlatforms > 200)
            {
                _numberPassedPlatforms = 0;
                Platform.DownSpeed+=2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform p in _platforms)
            {
                p.Draw(spriteBatch);
            }
        }

        private Platform CreatePlatform(int index)
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
                return new SolidPlatform(_textures["Jump_Medium"], new Vector2(x, y), PlatformSize.Medium, SolidPlatformType.Lava);
            }

            return new SolidPlatform(_textures[textureKey], new Vector2(x, y), size, SolidPlatformType.Stone);
        }
        private void Init()
        {
            /*
             *  TODO:
             *  Remove or fix this function, after you add different types of platforms.
             *  Maybe you can use CreatePlatform somehow, instead code repetition
             */
            _platforms[0] = new SolidPlatform(_textures["Dirt_Ground_Bottom"], new Vector2(0, Game1.HEIGHT - 48), PlatformSize.None, SolidPlatformType.Stone);
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
                _platforms[i] = new SolidPlatform(_textures[textureKey], new Vector2(x, y), size, SolidPlatformType.Stone);
                _lastPlatform = i;
            }
        }
        public void Unload()
        {
            _content.Unload();
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
