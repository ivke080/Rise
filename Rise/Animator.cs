using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Rise
{
    class Animator
    {
        private Texture2D texture;
        private Vector2 position; // position of the animation in global space
        private Vector2 frameSize;

        private int frames; // number of frames for current animation

        private int startFrame; // animation start frame

        private float time; // duration of current animation in seconds
        private float elapsedTime;

        private int currentX; // current frame X
        private int currentY; // current frame Y
        private Rectangle currentFrame;

        private bool singleFrame;

        public Animator(Texture2D texture, Vector2 frameSize, Vector2 position, int startFrame)
        {
            this.texture = texture;
            this.frameSize = frameSize;
            this.position = position;
            this.startFrame = startFrame;

            currentX = 0;
            currentY = -1;
            currentFrame = new Rectangle(0, 0, (int)frameSize.X, (int)frameSize.Y);
        }

        public void PlaySingle(int frameX, int frameY)
        {
            // commented for player to move properly while mid-air
            //if (!singleFrame)
            //{
                singleFrame = true;
                currentFrame.X = frameX * (int)frameSize.X;
                currentFrame.Y = frameY * (int)frameSize.Y;

                currentY = -1;
                currentX = 0;
            //}
        }
        public void Play(int row, int frames, float time)
        {
            if (currentY != row)
            {
                this.frames = frames;
                this.time = time;
                singleFrame = false;
                currentY = row;
                currentX = startFrame;
                currentFrame.X = startFrame;
                currentFrame.Y = row * (int)frameSize.Y;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (singleFrame)
            {
                return;
            }

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= time)
            {
                currentX++;
                if (currentX >= frames)
                {
                    currentX = 0;
                }
                elapsedTime = 0;
                currentFrame.X = currentX * (int)frameSize.X;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentFrame, Color.White);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 FrameSize
        {
            get { return frameSize; }
        }
    }
}
