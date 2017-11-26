using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Rise.Gui
{
    class TextButton : Button
    {
        private string _text;
        private SpriteFont _font;
        private Vector2 _textPosition;

        public TextButton(Rectangle bounds, Texture2D backSprite, Texture2D backSpritePressed, SpriteFont font, string text)
            : base(bounds, backSprite, backSpritePressed)
        {
            _font = font;
            _text = text;

            Vector2 _textSize = _font.MeasureString(_text);

            _textPosition = new Vector2(_bounds.X+_bounds.Width / 2 - _textSize.X / 2,_bounds.Y+_bounds.Height / 2 - _textSize.Y / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_pressed)
            {
                spriteBatch.Draw(_backSpritePressed, _position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_backSprite, _position, Color.White);
            }
            spriteBatch.DrawString(_font, _text, _textPosition, Color.White);
        }
    }
}
