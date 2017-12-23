using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise.Platforms
{
    enum StaticPlatformType
    {
        Stone,
        Lava
    }
    class StaticPlatform : Platform
    {
        private Texture2D _texture;
        private StaticPlatformType _type;

        public StaticPlatform(Texture2D texture, Vector2 position, PlatformSize size, StaticPlatformType type, bool horizontalMovement)
            : base(position, size, horizontalMovement)
        {
            _texture = texture;
            _type = type;
        }

        public StaticPlatform(Texture2D texture, float x, float y, PlatformSize size, StaticPlatformType type, bool horizontalMovement)
            : this(texture, new Vector2(x, y), size, type, horizontalMovement)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10;

            if (_horizontalMovement)
            {
                _position.X += _horizontalMovementDir * PlatformManager.HorizontalMovementSpeed * delta;

                if ((int)_position.X + _bounds.Width >= Game1.WIDTH - Platform.HorizontalMovementEndGap || (int)_position.X <= Platform.HorizontalMovementEndGap)
                {
                    _horizontalMovementDir *= -1;
                }

                _bounds.X = (int)_position.X;
            }

            if (PlatformManager.MoveDownward) // promeniti da bude PlatformManager.Move
            {
                _position.Y += PlatformManager.CurrentDownSpeed * delta;

                _bounds.Y = (int)_position.Y;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        // Test
        public void RecreatePlatform(Texture2D texture, int x, int y, PlatformSize size, StaticPlatformType type, bool horizontalMovement)
        {
            _texture = texture;
            _position.X = x;
            _position.Y = y;
            _size = size;
            _type = type;
            _horizontalMovement = horizontalMovement;
            CalculateBounds();
        }
        // End test

        public StaticPlatformType Type
        {
            get { return _type; }
        }
    }
}
