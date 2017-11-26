using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Rise.Gui
{
    abstract class Button
    {
        protected Texture2D _backSprite;
        protected Texture2D _backSpritePressed;
        protected Rectangle _bounds;
        protected Vector2 _position; // need this for spriteBatch.Draw(...)
        protected bool _pressed = false;

        public Button(Rectangle bounds, Texture2D backSprite, Texture2D backSpritePressed)
        {
            _bounds = bounds;
            _position = new Vector2(bounds.X, bounds.Y);
            _backSprite = backSprite;
            _backSpritePressed = backSpritePressed;
        }
        
        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
        }
        public bool Pressed
        {
            get
            {
                return _pressed;
            }
            set
            {
                _pressed = value;
            }
        }
    }
}
