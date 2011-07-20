using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Indiv0.BlockSimulator.Utilities
{
    abstract class Utility
    {
        #region Constants
        protected const int OFFSET_Y = 20;
        protected const int OFFSET_X = 10;
        #endregion

        #region Protected Fields
        protected Vector2 _position;
        protected string _outputText;
        #endregion

        #region Initialization
        public Utility(BlockSimulatorGame game, float x, float y)
        {
            _position = new Vector2();
            _position.X = x;
            _position.Y = y;
        }
        #endregion

        #region Public Methods
        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }
        #endregion
    }
}
