using CodheadzLD31.Utils;
using Microsoft.Xna.Framework;

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

            spriteBatch.BeginPixel();
            spriteBatch.DrawString(normalFont, "Whoo hooo", new Vector2(10, 10), Color.Yellow);
            spriteBatch.End();
        }
    }
}
