using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Rise.Gui;

namespace Rise.GameStates
{
    class MenuState : GameState
    {
        private SpriteFont _font;
        private Texture2D _button;
        private Texture2D _buttonPressed;

        private TextButton _btnStart;
        private TextButton _btnExit;

        public MenuState(ContentManager content)
            : base(content)
        {
            
        }

        public override void Initialize()
        {
            _btnStart = new TextButton(new Rectangle((int)(Game1.WIDTH / 2 - _button.Bounds.Width / 2), (int)(Game1.HEIGHT / 2 - _button.Bounds.Height), _button.Bounds.Width, _button.Bounds.Height), 
                                       _button, _buttonPressed, _font, "Start Game");
            _btnExit = new TextButton(new Rectangle((int)(Game1.WIDTH / 2 - _button.Bounds.Width / 2), (int)(Game1.HEIGHT / 2 + _button.Bounds.Height - 20), _button.Bounds.Width, _button.Bounds.Height),
                                       _button, _buttonPressed, _font, "Exit Game");
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("medieval");
            _button = _content.Load<Texture2D>("btn_long_brown");
            _buttonPressed = _content.Load<Texture2D>("btn_long_brown_pressed");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _btnStart.Draw(spriteBatch);
            _btnExit.Draw(spriteBatch);
        }
    }
}
