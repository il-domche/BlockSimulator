using Indiv0.BlockSimulator.Common;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indiv0.BlockSimulator.GUI
{
    class FontManager : Manager
    {
        #region Public Fields
        public static SpriteFont KOOTENAY_FONT;
        #endregion

        #region Initialization
        public FontManager(BlockSimulatorGame game)
            : base(game)
        {
            KOOTENAY_FONT = _game.Content.Load<SpriteFont>("res/fonts/kootenay");
        }
        #endregion
    }
}
