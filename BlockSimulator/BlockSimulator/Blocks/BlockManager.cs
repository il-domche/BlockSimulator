using System.Collections.Generic;
using Indiv0.BlockSimulator.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Indiv0.BlockSimulator.Cameras;

namespace Indiv0.BlockSimulator.Blocks
{
    class BlockManager : Manager
    {
        #region Private Fields
        private List<Block> _blocks = new List<Block>();
        private BasicEffect _effect;
        private short _spacing = 32;
        #endregion

        #region Properties
        public List<Block> Blocks
        {
            get { return _blocks; }
        }
        #endregion

        #region Initialization
        public BlockManager(BlockSimulatorGame game)
            : base(game)
        {
            _effect = new BasicEffect(_game.GraphicsDevice);
            RenderMap();
        }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleInput();
            foreach (Block block in _blocks)
            {
                //block.Update(gameTime);
            }

            _previousKeyboardState = _currentKeyboardState;
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            _effect.View = Camera.ActiveCamera.View;
            _effect.World = Camera.ActiveCamera.World;
            _effect.Projection = Camera.ActiveCamera.Projection;

            _effect.TextureEnabled = true;
            _effect.Texture = _game.Content.Load<Texture2D>("res/blocks/grass");

            _effect.EnableDefaultLighting();

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                foreach (Block block in _blocks)
                {
                    block.RenderToDevice(_game.GraphicsDevice);
                }
            }
        }
        #endregion

        #region Private Methods
        private void RenderMap()
        {
            if (_blocks.Count > 0)
            {
                _blocks.Clear();
            }

            Vector3 size = new Vector3(1, 1, 1);
            Vector3 position = new Vector3();
            Texture2D texture = _game.Content.Load<Texture2D>("res/blocks/grass");
            for (int y = 0; y < 3; y++)
            {
                position.Y = y * _spacing;
                for (int x = 0; x < 3; x++)
                {
                    position.X = x * _spacing;
                    for (int z = 0; z < 3; z++)
                    {
                        position.Z = z * _spacing;
                        _blocks.Add(new GrassBlock(new Vector3(10,10,10), position));
                    }
                }
            }
        }

        private void HandleInput()
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState != _previousKeyboardState)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    _spacing -= 10;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.G))
                {
                    _spacing += 10;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    RenderMap();
                }
            }
        }
        #endregion
    }
}
