using Indiv0.BlockSimulator.Cameras;
using Indiv0.BlockSimulator.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BlockSimulator.Utilities
{
    class PositionUtility : Utility
    {
        #region Private Fields
        private static BlockSimulatorGame _game;
        private MouseState currentMouseState;
        private Vector2 _mousePosition;
        private Camera _camera;
        #endregion

        #region Initialization
        public PositionUtility(BlockSimulatorGame game, float x, float y)
            : base(game, x, y)
        {
            _game = game;
            _camera = Camera.ActiveCamera;
        }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentMouseState = Mouse.GetState();
            _camera = Camera.ActiveCamera;

            _mousePosition.X = currentMouseState.X;
            _mousePosition.Y = currentMouseState.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 mouseVector = new Vector2();
            Vector2 positionVector = new Vector2();

            mouseVector = _position;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "Mouse Data: ", mouseVector, Color.White);

            mouseVector.X += OFFSET_X;
            mouseVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "X: " + _mousePosition.X,
                mouseVector, Color.White);

            mouseVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "Y: " + _mousePosition.Y, mouseVector, Color.White);

            positionVector = mouseVector;
            positionVector.X -= OFFSET_X;
            positionVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "Position: ", positionVector, Color.White);

            positionVector.X += OFFSET_X;
            positionVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "X: " + Camera.ActiveCamera.Position.X, positionVector, Color.White);
            positionVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "Y: " + Camera.ActiveCamera.Position.Y, positionVector, Color.White);
            positionVector.Y += OFFSET_Y;
            spriteBatch.DrawString(FontManager.KOOTENAY_FONT, "Z: " + Camera.ActiveCamera.Position.Z, positionVector, Color.White);
        }
        #endregion
    }
}
