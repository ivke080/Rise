using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(this.Content, new Vector2(100, HEIGHT - 64));

            base.Initialize();
        }
        
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (player.Bounds.Y + player.Bounds.Height >= HEIGHT)
            {
                player.OnGround = true;
                player.Y = HEIGHT - player.Bounds.Height;
            }

            player.Controls(Keyboard.GetState());
            player.Update(gameTime);

            base.Update(gameTime);
        }
        
        /// This is called when the game should draw itself.
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
