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
        private Animator _animation;
        private Vector2 _position;
        private Vector2 _velocity;

        private Platform _platform; // reference for the platform that player is standing on

        private Movement _movement = Movement.None;
        private Movement _lastMovement = Movement.Right;

        private Rectangle _bounds;

        private float _moveSpeed = 20;
        private float _time = 0.1f;
        private int _boundsOffsetX = 30; // sprite is wider than actual character
        private bool _onGround = false;
        private bool _falling = false;

        private const int Frames = 4;

        public Player(ContentManager content, Vector2 position)
        {
            _animation = new Animator(content.Load<Texture2D>("character"), new Vector2(96, 64), position, 2);
            _position = position;
            _velocity = new Vector2(0, 0);
            _bounds = new Rectangle((int)position.X + _boundsOffsetX, (int)position.Y, (int)_animation.FrameSize.X - 2 * _boundsOffsetX, (int)_animation.FrameSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds * 10; // delta time
            

            if (_movement == Movement.None)
            {
                _velocity.X = 0;
            }
            else if (_movement == Movement.Right && _movement != Movement.Left)
            {
                _velocity.X = _moveSpeed;
            }
            else if (_movement == Movement.Left && _movement != Movement.Right)
            {
                _velocity.X = -_moveSpeed;
            }

            if (!_onGround)
            {
                _velocity.Y += Game1.GRAVITY;
            }
            else
            {
                _velocity.Y = 0;
            }
            

            if (_velocity.Y > 0)
            {
                _falling = true;
            }
            else
            {
                _falling = false;
            }

            Animate(_movement);


            //_position += _velocity * delta; old code
            _position.X += _velocity.X * delta;

            // This part of the code is for centering player when Y < Height/2
            // disabling it to go up (lover than Height/2)
            if (_position.Y > Game1.HEIGHT / 2)
                _position.Y += _velocity.Y * delta;
            else
            {
                if (_velocity.Y > 0)
                    _position.Y += _velocity.Y * delta;
            }
            // end of the centering code
            if (_platform != null)
            {
                _position.Y = _platform.Bounds.Y - _bounds.Height + 2;
            }

            _animation.Position = _position;
            _bounds.X = (int)_position.X + _boundsOffsetX;
            _bounds.Y = (int)_position.Y;

            _animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _animation.Draw(spriteBatch);
        }
        private void Jump(float jumpForce = 120)
        {
            if (_onGround)
            {
                _velocity.Y = -jumpForce;
                _onGround = false;
                _platform = null;
            }
        }
        private void Animate(Movement movement)
        {
            if (movement != Movement.None)
            {
                if (_onGround)
                {
                    _animation.Play((int)movement, Frames, _time);
                }
                else
                {
                    _animation.PlaySingle(0, (int)movement);
                }
            }
            else
            {
                _animation.PlaySingle(0, (int)_lastMovement);
            }
        }
        public void Controls(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.D))
            {
                _movement = Movement.Right;
                _lastMovement = Movement.Right;
            }
            else if (state.IsKeyDown(Keys.A))
            {
                _movement = Movement.Left;
                _lastMovement = Movement.Left;
            }
            else if (state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
            {
                _movement = Movement.None;
            }
            
            if (state.IsKeyDown(Keys.Space))
            {
                Jump();
            }
        }
        public void Stop()
        {
            _onGround = true;
            _velocity.Y = 0;
        }

        public bool GoingUp()
        {
            if (!_falling && !_onGround)
                return true;
            return false;
        }

        public Rectangle Bounds
        {
            get { return _bounds; }
        }
        public Movement Movement
        {
            get { return _movement; }
            set { _movement = value; }
        }
        public bool OnGround
        {
            get { return _onGround; }
            set { _onGround = value; }
        }
        public bool Falling
        {
            get { return _falling; }
            set { _falling = value; }
        }
        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        public Platform Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }
        public float VelocityY
        {
            get { return _velocity.Y; }
        }
    }
}
