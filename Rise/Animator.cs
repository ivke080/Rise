using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    class Animator
    {
        private Texture2D _texture;
        private Vector2 _position; // position of the animation in global space
        private Vector2 _frameSize;

        private int _frames; // number of frames for current animation

        private int _startFrame; // animation start frame

        private float _time; // duration of current animation in seconds
        private float _elapsedTime;

        private int _currentX; // current frame X
        private int _currentY; // current frame Y
        private Rectangle _currentFrame;

        private bool _singleFrame;

        public Animator(Texture2D texture, Vector2 frameSize, Vector2 position, int startFrame)
        {
            _texture = texture;
            _frameSize = frameSize;
            _position = position;
            _startFrame = startFrame;

            _currentX = 0;
            _currentY = -1;
            _currentFrame = new Rectangle(0, 0, (int)frameSize.X, (int)frameSize.Y);
        }

        public void PlaySingle(int frameX, int frameY)
        {
            // commented for player to move properly while mid-air
            //if (!singleFrame)
            //{
                _singleFrame = true;
                _currentFrame.X = frameX * (int)_frameSize.X;
                _currentFrame.Y = frameY * (int)_frameSize.Y;

                _currentY = -1;
                _currentX = 0;
            //}
        }
        public void Play(int row, int frames, float time)
        {
            if (_currentY != row)
            {
                _frames = frames;
                _time = time;
                _singleFrame = false;
                _currentY = row;
                _currentX = _startFrame;
                _currentFrame.X = _startFrame;
                _currentFrame.Y = row * (int)_frameSize.Y;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (_singleFrame)
            {
                return;
            }

            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedTime >= _time)
            {
                _currentX++;
                if (_currentX >= _frames)
                {
                    _currentX = 0;
                }
                _elapsedTime = 0;
                _currentFrame.X = _currentX * (int)_frameSize.X;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _currentFrame, Color.White);
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Vector2 FrameSize
        {
            get { return _frameSize; }
        }
    }
}
