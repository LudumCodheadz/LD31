using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class DebugComponent:ComponentBase
    {
        public DebugComponent(LDGame game)
            :base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(normalFont, "Whoo hooo", new Vector2(10, 10), Color.Yellow);
            spriteBatch.End();
        }
    }
}
