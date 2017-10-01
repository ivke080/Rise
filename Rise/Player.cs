using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    enum Movement
    {
        None = -1,
        Right,
        Left,
        Up,
        Down
    }
    class Player
    {
        private Animator animation;
        private Vector2 position;
        private Vector2 velocity;

        private Movement movement = Movement.None;
        private Movement lastMovement = Movement.Right;

        private Rectangle bounds;

        private int boundsOffsetX = 12; // sprite is wider than actual character

        public Player(ContentManager content, Vector2 position)
        {
            animation = new Animator(content.Load<Texture2D>("character"), new Vector2(96, 64), position);
            this.position = position;
            velocity = new Vector2(0, 0);
            bounds = new Rectangle((int)position.X + boundsOffsetX, (int)position.Y, (int)animation.FrameSize.X - 2 * boundsOffsetX, (int)animation.FrameSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            float dleta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10; // delta time
            
            

            animation.Position = position;
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        }
        public Movement Movement
        {
            get { return movement; }
            set { movement = value; }
        }
    }
}
