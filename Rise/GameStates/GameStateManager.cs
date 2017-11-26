using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace Rise.GameStates
{
    class GameStateManager
    {
        private static GameStateManager _instance;
        private Stack<GameState> _states = new Stack<GameState>();
        //private ContentManager _content;

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public void AddState(GameState state)
        {
            try
            {
                _states.Push(state);
                _states.Peek().LoadContent();
                _states.Peek().Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemoveState()
        {
            if (_states.Count > 0)
            {
                try
                {
                    GameState state = _states.Peek();
                    state.UnloadContent();
                    _states.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_states.Count > 0)
            {
                try
                {
                    _states.Peek().Update(gameTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_states.Count > 0)
            {
                try
                {
                    _states.Peek().Draw(spriteBatch);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /*public void RemoveAllStates()
        {
            while (_states.Count > 0)
            {
                _states.Peek().UnloadContent();
                _states.Pop();
            }
        }

        public void ChangeGameState(GameState state)
        {
            try
            {
                RemoveAllStates();
                AddState(state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/
    }
}
