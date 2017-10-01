using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rise
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Animator animator;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            animator = new Animator(this.Content.Load<Texture2D>("character"), new Vector2(96, 64), new Vector2(100, 100));
            animator.Play(0, 4, 0.1f);
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
            animator.Update(gameTime);
            base.Update(gameTime);
        }
        
        /// This is called when the game should draw itself.
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            animator.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
