using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class PlayerHudComponent:ComponentBase
    {
        ScreenNode hudBackground;

        public PlayerHudComponent(LDGame game):base(game)
        {

        }

        public int Lives { get; private set; }
        public void ResetLives()
        {
            Lives = 3;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            hudBackground = new ScreenNode(this.Game);

            for (int i = 0; i < 4; i++)
            {
                var topCloud = new SpriteScreenNode(this.Game, "Sprites\\TopCloud");
                topCloud.Scale = 2f;
                topCloud.Update(new Microsoft.Xna.Framework.GameTime());
                topCloud.Offset = new Microsoft.Xna.Framework.Vector2(i * topCloud.Sprite.Rectangle.Width, 0);
                this.hudBackground.AddChild(topCloud);
            }

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            hudBackground.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            hudBackground.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
