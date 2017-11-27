using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rise.Platforms;
using Rise.GameStates;

namespace Rise
{
    public class Game1 : Game
    {

        public const float GRAVITY = 5f;
        public const int WIDTH = 640;
        public const int HEIGHT = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        PlatformManager platformManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = WIDTH,
                PreferredBackBufferHeight = HEIGHT
            };
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(this.Content, new Vector2(100, HEIGHT - 200));
            platformManager = new PlatformManager(new Microsoft.Xna.Framework.Content.ContentManager(Content.ServiceProvider, Content.RootDirectory));
            GameStateManager.Instance.AddState(new MenuState(Content));
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
            platformManager.Unload();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GameStateManager.Instance.Count == 0)
            {
                Exit();
            }

            // TODO: Add your update logic here


            //platformManager.Update(gameTime);
            //player.Controls(Keyboard.GetState());
            //player.Update(gameTime);

            GameStateManager.Instance.Update(gameTime);

            Collision.ManyPlatforms(player, platformManager.Platforms);

            if (player.GoingUp())
            {
                if (player.Y < Game1.HEIGHT/2)
                {
                    Platform.CurrentDownSpeed = -player.VelocityY;
                }
            }
            else
            {
                Platform.CurrentDownSpeed = Platform.DownSpeed;
            }

            if (player.Bounds.Y + player.Bounds.Height >= HEIGHT-10)
            {
                player.Stop();
                player.Y = HEIGHT - player.Bounds.Height;
            }

            base.Update(gameTime);
        }
        
        /// This is called when the game should draw itself.
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //platformManager.Draw(spriteBatch);
            //player.Draw(spriteBatch);
            GameStateManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
