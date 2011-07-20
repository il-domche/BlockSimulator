using System.Collections.Generic;
using Indiv0.BlockSimulator.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BlockSimulator.Utilities
{
    class UtilityManager : Manager
    {
        #region Constants
        // Coordinates of string positions
        private const float FPS_X = 20;
        private const float FPS_Y = 20;
        private const float POS_X = 20;
        private const float POS_Y = 40;
        #endregion

        #region Private Fields
        private List<Utility> _utilityList;
        private bool _displayingUtils = false;
        #endregion

        #region Public Enums
        public enum UtilityOptions
        {
            FPSUtility
        }
        #endregion

        #region Initialization
        public UtilityManager(BlockSimulatorGame game)
            : base(game)
        {
            FPSUtility fpsUtility = new FPSUtility(_game, FPS_X, FPS_Y);
            PositionUtility positionUtility = new PositionUtility(_game, POS_X, POS_Y);
            _utilityList = new List<Utility>();

            _utilityList.Add(fpsUtility);
            _utilityList.Add(positionUtility);
        }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleInput();
            if (_displayingUtils == true)
            {
                foreach (Utility utility in _utilityList)
                {
                    //FPSUtility is a special case which requires the Update() method to be performed during Draw() for correct operation.
                    //Thus, FPSUtility.Update() is called from within BoxGame.cs (In the Draw() method).
                    if (utility is FPSUtility)
                    { }
                    else
                    {
                        utility.Update(gameTime);
                    }
                }
            }

            _previousKeyboardState = _currentKeyboardState;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (_displayingUtils == true)
            {
                foreach (Utility utility in _utilityList)
                {
                    if (utility is FPSUtility)
                    {
                        utility.Update(gameTime);
                    }
                }
                foreach (Utility utility in _utilityList)
                {
                    utility.Draw(spriteBatch);
                }
            }
        }
        #endregion

        #region Private Methods
        private void HandleInput()
        {
            _currentKeyboardState = Keyboard.GetState();
            if (_currentKeyboardState != _previousKeyboardState)
            {
                if (_currentKeyboardState.IsKeyDown(Keys.F3))
                {
                    _displayingUtils = !_displayingUtils;
                }
            }
        }
        #endregion
    }
}
