using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BlockSimulator.Common
{
    abstract class Manager
    {
        #region Protected Fields
        protected static BlockSimulatorGame _game;

        protected KeyboardState _currentKeyboardState;
        protected KeyboardState _previousKeyboardState;
        #endregion

        #region Initialization
        public Manager(BlockSimulatorGame game)
        {
            _game = game;
        }
        #endregion

        #region Public Methods
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        { }
        #endregion
    }
}
