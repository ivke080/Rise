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
        Platform solid, solidShort;
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
        
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(this.Content, new Vector2(100, HEIGHT - 64));
            //solid = new SolidPlatform(this.Content.Load<Texture2D>("stone_ground_long"), new Vector2(300, HEIGHT - 500), PlatformSize.Long);
            //solidShort = new SolidPlatform(this.Content.Load<Texture2D>("stone_ground_short"), new Vector2(120, HEIGHT - 320), PlatformSize.Short);
            platformManager = new PlatformManager(new Microsoft.Xna.Framework.Content.ContentManager(Content.ServiceProvider, Content.RootDirectory));
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
            Content.Unload();
            platformManager.Unload();
        }
        
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            player.Controls(Keyboard.GetState());
            player.Update(gameTime);
            platformManager.Update(gameTime);

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
            /*solid.Update(gameTime);
            solidShort.Update(gameTime);

            if (Collision.SinglePlatform(player, solid))
            {
                if (player.Falling)
                {
                    player.Stop();
                    player.Falling = false;
                    //player.Y = solid.Bounds.Y - player.Bounds.Height - 5;
                    player.Platform = solid;
                }
            }
            else if (Collision.SinglePlatform(player, solidShort))
            {
                if (player.Falling)
                {
                    player.Stop();
                    player.Falling = false;
                    //player.Y = solidShort.Bounds.Y - player.Bounds.Height - 5;
                    player.Platform = solidShort;
                }
            }
            else
            {
                player.OnGround = false;
                player.Platform = null;
            }*/

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

            //solid.Draw(spriteBatch);
            //solidShort.Draw(spriteBatch);
            platformManager.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
