using System.Collections.Generic;
using Indiv0.BlockSimulator.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Indiv0.BlockSimulator.Cameras;
using Indiv0.BlockSimulator.Physics;
using JigLibX.Physics;

namespace Indiv0.BlockSimulator.Blocks
{
    class BlockManager : Manager
    {
        #region Private Fields
        private List<Block> _blocks = new List<Block>();
        private BasicEffect _effect;
        private short _spacing = 64;

        private string _resourceDir = "res";
        private string _blockGrassDir = "res/blocks/grass";
        private string _blockBedrockDir = "res/blocks/bedrock";
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

            _previousKeyboardState = _currentKeyboardState;
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            _game.GraphicsDevice.BlendState = BlendState.Opaque;
            _game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (Block block in _blocks)
            {
                block.Draw(gameTime);
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

            Vector3 size = new Vector3(16, 16, 16);
            Vector3 position = new Vector3();
            for (int y = 0; y < 3; y++)
            {
                position.Y = y * _spacing;
                for (int x = 0; x < 3; x++)
                {
                    position.X = x * _spacing;
                    for (int z = 0; z < 3; z++)
                    {
                        position.Z = z * _spacing;
                        if (y == 0)
                        {
                            _blocks.Add(new BedrockBlock(_game, position, size, _blockBedrockDir));
                        }
                        else
                        {
                            _blocks.Add(new GrassBlock(_game, position, size, _blockGrassDir));
                        }
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
