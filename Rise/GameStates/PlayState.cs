using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Rise.Platforms;

namespace Rise.GameStates
{
    sealed class PlayState : GameState
    {

        private Player _player;
        private PlatformManager _platformManager;

        private Texture2D _bgImage;
        private Vector2 _bgImagePosition = new Vector2(0, -720);

        public PlayState(ContentManager content)
            : base(content)
        {

        }

        public override void Initialize()
        {
           _player = new Player(_content, new Vector2(100, Game1.HEIGHT - 200));
           _platformManager = new PlatformManager(_content);
        }

        public override void LoadContent()
        {
            _bgImage = _content.Load<Texture2D>("background");
        }

        public override void Update(GameTime gameTime)
        {

            _platformManager.Update(gameTime);
            _player.Controls(Keyboard.GetState());
            _player.Update(gameTime);

            Collision.ManyPlatforms(_player, _platformManager.Platforms);

            if (_player.GoingUp())
            {
                if (_player.Y < Game1.HEIGHT / 2)
                {
                    PlatformManager.CurrentDownSpeed = -_player.VelocityY;
                }
            }
            else
            {
                PlatformManager.CurrentDownSpeed = PlatformManager.DownSpeed;
            }

            if (_player.Bounds.Y + _player.Bounds.Height >= Game1.HEIGHT - 10)
            {
                _player.Stop();
                _player.Y = Game1.HEIGHT - _player.Bounds.Height;
            }

            if (PlatformManager.MoveDownward)
            {
                _bgImagePosition.Y += PlatformManager.DownSpeed * 0.01f; // scrolling background image only if platforms are moving
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_bgImage, _bgImagePosition, Color.White);
            _platformManager.Draw(spriteBatch);
            _player.Draw(spriteBatch);
        }
    }
}
