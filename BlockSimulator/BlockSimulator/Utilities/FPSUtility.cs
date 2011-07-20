using System;
using Indiv0.BlockSimulator.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Indiv0.BlockSimulator.Utilities
{
    class FPSUtility : Utility
    {
        #region Constants
        private const long TICKS_IN_A_SECOND = 10000000;
        #endregion

        #region Private Fields
        private int _framesSoFar = 0;
        private int _framesPerSecond;
        private long _elapsedTicks = 0;
        private long _lastFrameTick = 0;
        #endregion

        #region Initialization
        public FPSUtility(BlockSimulatorGame game, float x, float y)
            : base(game, x, y)
        { }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _elapsedTicks += gameTime.TotalGameTime.Ticks - _lastFrameTick;
            _lastFrameTick = gameTime.TotalGameTime.Ticks;

            if (_elapsedTicks <= TICKS_IN_A_SECOND)
            {
                _framesSoFar++;
            }
            else
            {
                _elapsedTicks = 0;
                _framesPerSecond = _framesSoFar;
                _framesSoFar = 0;
                _outputText = Convert.ToString(_framesPerSecond);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "FPS: " + _outputText, _position, Color.White);
        }
        #endregion
    }
}
