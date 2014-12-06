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
        private SpriteScreenNode toiletNode;

        public LevelComponent(LDGame game):base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            worldRoot = new ScreenNode(this.Game);
            var groundNodes = new ScreenNode(this.Game);
            int y = Game.GraphicsDevice.PresentationParameters.BackBufferHeight - (20 * 2);
            groundNodes.Offset = new Vector2(0, y);
            worldRoot.AddChild(groundNodes);
            for (int i = 0; i < 20; i++)
            {
                var floor = new SpriteScreenNode(this.Game, "sprites/floor");
                floor.Offset = new Microsoft.Xna.Framework.Vector2(i * 40, 0);
                floor.Scale = 2;
                floor.Color = Color.LimeGreen;
                groundNodes.AddChild(floor);
            }

            toiletNode = new SpriteScreenNode(this.Game, "sprites/Toilet");
            toiletNode.Scale = 2;
            y = (this.GraphicsDevice.PresentationParameters.BackBufferWidth - (28 * 2)) / 2;
            toiletNode.Offset = new Vector2(y, groundNodes.Offset.Y - (46 * 2));
            this.worldRoot.AddChild(toiletNode);
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
