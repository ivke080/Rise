using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Rise
{
    enum Movement
    {
        None = -1,
        Right = 0,
        Left = 1,
        Jump
    }
    class Player
    {
        private Animator animation;
        private Vector2 position;
        private Vector2 velocity;

        private Movement movement = Movement.None;
        private Movement lastMovement = Movement.Right;

        private Rectangle bounds;

        private float moveSpeed = 20;
        private float jumpForce = -100;
        private float time = 0.1f;
        private int boundsOffsetX = 12; // sprite is wider than actual character
        private bool onGround = false;

        private const int FRAMES = 4;

        public Player(ContentManager content, Vector2 position)
        {
            animation = new Animator(content.Load<Texture2D>("character"), new Vector2(96, 64), position, 2);
            this.position = position;
            velocity = new Vector2(0, 0);
            bounds = new Rectangle((int)position.X + boundsOffsetX, (int)position.Y, (int)animation.FrameSize.X - 2 * boundsOffsetX, (int)animation.FrameSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10; // delta time
            

            if (movement == Movement.None)
            {
                velocity.X = 0;
            }
            else if (movement == Movement.Right && movement != Movement.Left)
            {
                velocity.X = moveSpeed;
            }
            else if (movement == Movement.Left && movement != Movement.Right)
            {
                velocity.X = -moveSpeed;
            }

            if (!onGround)
            {
                velocity.Y += Game1.GRAVITY;
            }
            else
            {
                velocity.Y = Game1.GRAVITY;
            }

            Animate(movement);
            

            position += velocity * delta;

            animation.Position = position;
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;

            animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
        private void Jump()
        {
            if (onGround)
            {
                velocity.Y = jumpForce;
                onGround = false;
            }
        }
        private void Animate(Movement movement)
        {
            if (movement != Movement.None)
            {
                if (onGround)
                {
                    animation.Play((int)movement, FRAMES, time);
                }
                else
                {
                    animation.PlaySingle(0, (int)movement);
                }
            }
            else
            {
                animation.PlaySingle(0, (int)lastMovement);
            }
        }
        public void Controls(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.D))
            {
                movement = Movement.Right;
                lastMovement = Movement.Right;
            }
            else if (state.IsKeyDown(Keys.A))
            {
                movement = Movement.Left;
                lastMovement = Movement.Left;
            }
            else if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
            {
                movement = Movement.None;
            }
            
            if (state.IsKeyDown(Keys.Space))
            {
                Jump();
            }
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
        public bool OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }
        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
    }
}
