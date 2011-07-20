using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Indiv0.BlockSimulator.Blocks
{
    public class BedrockBlock : Block
    {
        #region Initialization
        public BedrockBlock(BlockSimulatorGame game, Vector3 position, Vector3 scale, string modelDir) 
            : base(game, position, scale, modelDir)
        {
            Body.Immovable = true;
        }
        #endregion
    }
}
