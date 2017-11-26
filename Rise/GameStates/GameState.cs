using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Rise.GameStates
{
    abstract class GameState : IGameState
    {
        protected ContentManager _content;

        public GameState(ContentManager content)
        {
            _content = new ContentManager(content.ServiceProvider, content.RootDirectory);
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public void UnloadContent()
        {
            _content.Unload();
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
