using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    class SolidPlatform : Platform
    {
        private Texture2D _texture;

        public SolidPlatform(Texture2D texture, Vector2 position, PlatformSize size)
            : base(position, size)
        {
            _texture = texture;
        }

        public SolidPlatform(Texture2D texture, float x, float y, PlatformSize size)
            : this(texture, new Vector2(x, y), size)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10;

            _position.Y += CurrentDownSpeed * delta;

            _bounds.Y = (int)_position.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
