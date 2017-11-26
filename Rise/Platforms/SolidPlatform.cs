using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise.Platforms
{
    enum SolidPlatformType
    {
        Stone,
        Lava
    }
    class SolidPlatform : Platform
    {
        private Texture2D _texture;
        private SolidPlatformType _type;

        public SolidPlatform(Texture2D texture, Vector2 position, PlatformSize size, SolidPlatformType type)
            : base(position, size)
        {
            _texture = texture;
            _type = type;
        }

        public SolidPlatform(Texture2D texture, float x, float y, PlatformSize size, SolidPlatformType type)
            : this(texture, new Vector2(x, y), size, type)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10;

            if (Move)
            {
                _position.Y += CurrentDownSpeed * delta;

                _bounds.Y = (int)_position.Y;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public SolidPlatformType Type
        {
            get { return _type; }
        }
    }
}
