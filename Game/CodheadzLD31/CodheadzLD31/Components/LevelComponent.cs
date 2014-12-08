using CodheadzLD31.Components.GamePlay;
using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Utils;
using Microsoft.Xna.Framework;

namespace CodheadzLD31.Components
{
    public class LevelComponent:ComponentBase
    {
        private ScreenNode worldRoot;
        private ToiletNode toiletNode;
        private ScreenNode groundNodes;

        public LevelComponent(LDGame game):base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            worldRoot = new ScreenNode(this.Game);
            groundNodes = new ScreenNode(this.Game);
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

            toiletNode = new ToiletNode(this.Game);
            toiletNode.Update(new GameTime());
            var x = (this.GraphicsDevice.PresentationParameters.BackBufferWidth - (28 * 2)) / 2;
            toiletNode.Offset = new Vector2(x, groundNodes.Offset.Y - toiletNode.Height);

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
            spriteBatch.BeginPixel();
            worldRoot.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public ScreenNode GroundNode { get { return groundNodes; } }
        public ToiletNode ToiletNode { get { return toiletNode; } }
    }
}
