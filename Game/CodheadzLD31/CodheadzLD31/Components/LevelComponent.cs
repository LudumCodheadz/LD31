using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class LevelComponent:ComponentBase
    {
        private ScreenNode worldRoot;

        public LevelComponent(LDGame game):base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            worldRoot = new ScreenNode(this.Game);
            for (int i = 0; i < 40; i++)
            {
                var block = new SpriteScreenNode(this.Game, "pixel");
                block.Offset = new Microsoft.Xna.Framework.Vector2(i * 20, 300);
                block.Scale = 10f;
                block.Color = Color.LimeGreen;
                worldRoot.AddChild(block);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            worldRoot.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            worldRoot.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

    }
}
