using CodheadzLD31.Components.GamePlay;
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
        private ScreenNode hudBackground;
        private PlayerLivesNode playerLivesNode;
        private TinyMessenger.TinyMessageSubscriptionToken onLevelEndToken;

        public PlayerHudComponent(LDGame game):base(game)
        {
            this.DrawOrder = 200;
            onLevelEndToken = Messages.Messenger.Default.Subscribe<Messages.LevelEndMessage>(OnLevelEnd);
        }

        private void OnLevelEnd(Messages.LevelEndMessage obj)
        {
            if(obj.Content.Dead)
            {
                ReduceLives();
            }
        }

        private void ReduceLives()
        {
            Lives--;
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

            playerLivesNode = new PlayerLivesNode(this.Game);
            playerLivesNode.Offset = new Microsoft.Xna.Framework.Vector2(3, 3);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            playerLivesNode.Lives = this.Lives;
            playerLivesNode.Update(gameTime);
            hudBackground.Update(gameTime);
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            hudBackground.Draw(gameTime, spriteBatch);
            playerLivesNode.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
