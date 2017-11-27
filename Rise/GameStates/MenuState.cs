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
        private Texture2D _buttonTexture;
        private Texture2D _buttonPressedTexture;
        private Texture2D _background;
        private Texture2D _cursor;

        private TextButton[] _buttons;

        private Vector2 _bgPosition = new Vector2(0, -Game1.HEIGHT);
        private Vector2 _mousePosition;

        MouseState _previousMouse;

        public MenuState(ContentManager content)
            : base(content)
        {
            
        }

        public override void Initialize()
        {
            _buttons = new TextButton[2];
            _buttons[0] = new TextButton(new Rectangle((int)(Game1.WIDTH / 2 - _buttonTexture.Bounds.Width / 2), (int)(Game1.HEIGHT / 2 - _buttonTexture.Bounds.Height), _buttonTexture.Bounds.Width, _buttonTexture.Bounds.Height),
                                       _buttonTexture, _buttonPressedTexture, _font, "Start Game");
            _buttons[1] = new TextButton(new Rectangle((int)(Game1.WIDTH / 2 - _buttonTexture.Bounds.Width / 2), (int)(Game1.HEIGHT / 2 + _buttonTexture.Bounds.Height - 30), _buttonTexture.Bounds.Width, _buttonTexture.Bounds.Height),
                                       _buttonTexture, _buttonPressedTexture, _font, "Exit Game");

            _previousMouse = Mouse.GetState();
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("medieval");
            _buttonTexture = _content.Load<Texture2D>("btn_long_brown");
            _buttonPressedTexture = _content.Load<Texture2D>("btn_long_brown_pressed");
            _background = _content.Load<Texture2D>("background");
            _cursor = _content.Load<Texture2D>("cursor");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            _mousePosition.X = mouse.Position.X;
            _mousePosition.Y = mouse.Position.Y;
            
            foreach (TextButton btn in _buttons)
            {
                if (btn.Bounds.Contains(mouse.Position))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (_previousMouse.LeftButton == ButtonState.Released)
                        {
                            btn.Pressed = true;
                        }
                    }
                }

                if (mouse.LeftButton == ButtonState.Released)
                {
                    if (btn.Pressed)
                    {
                        btn.Pressed = false;
                        if (btn.Text == "Exit Game")
                        {
                            GameStateManager.Instance.RemoveAllStates();
                        }
                    }
                }
            }

            _previousMouse = mouse;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _bgPosition, Color.White);
            foreach (TextButton btn in _buttons)
            {
                btn.Draw(spriteBatch);
            }
            spriteBatch.Draw(_cursor, _mousePosition, Color.White);
            
        }
    }
}
