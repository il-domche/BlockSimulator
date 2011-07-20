using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Indiv0.BlockSimulator.Blocks
{
    class GrassBlock : Block
    {
        #region Initialization
        public GrassBlock(BlockSimulatorGame game, Vector3 position, Vector3 scale, string modelDir) 
            : base(game, position, scale, modelDir)
        {
            Body.Immovable = false;
        }
        #endregion
    }
}
