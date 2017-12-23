using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise.Platforms
{
    enum PlatformSize
    {
        Short,
        Medium,
        Long,
        Ground
    }
    abstract class Platform
    {
        protected Vector2 _position;
        protected Rectangle _bounds;
        protected PlatformSize _size;

        protected bool _horizontalMovement;
        protected int _horizontalMovementDir = 1;

        public static int HorizontalMovementEndGap = 100;
        
        protected const int Height = 48;

        public Platform(Vector2 position, PlatformSize size, bool horizontalMovement)
        {
            _position = position;
            _size = size;

            CalculateBounds();

            _horizontalMovement = horizontalMovement;
        }
        public Platform(float x, float y, PlatformSize size, bool horizontalMovement)
            : this(new Vector2(x, y), size, horizontalMovement)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public Rectangle Bounds
        {
            get { return _bounds; }
        }
        
        protected void CalculateBounds()
        {
            int width;
            if (_size == PlatformSize.Short)
                width = 72;
            else if (_size == PlatformSize.Medium)
                width = 144;
            else if (_size == PlatformSize.Long)
                width = 216;
            else
                width = Game1.WIDTH; // bottom dirt ground

            if (_bounds == null)
            {
                _bounds = new Rectangle((int)_position.X, (int)_position.Y, width, Height);
            }
            else
            {
                _bounds.X = (int)_position.X;
                _bounds.Y = (int)_position.Y;
                _bounds.Width = width;
                _bounds.Height = Height;
            }
        }

        public bool HorizontalMovement
        {
            get { return _horizontalMovement; }
        }

        public int HorizontalMovementDir
        {
            get { return _horizontalMovementDir; }
        }

    }
}
