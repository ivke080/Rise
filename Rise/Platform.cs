using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    enum PlatformSize
    {
        Short,
        Long
    }
    abstract class Platform
    {
        protected Vector2 _position;
        protected Rectangle _bounds;
        protected PlatformSize _size;

        protected const int Height = 48;

        public static float DownSpeed = 3f;
        public static float CurrentDownSpeed = 3f;

        public Platform(Vector2 position, PlatformSize size)
        {
            _position = position;
            _size = size;

            int width = (size == PlatformSize.Short) ? 72 : 144;

            _bounds = new Rectangle((int)position.X, (int)position.Y, width, Height);
        }
        public Platform(float x, float y, PlatformSize size)
            : this(new Vector2(x, y), size)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public Rectangle Bounds
        {
            get { return _bounds; }
        }        
    }
}
