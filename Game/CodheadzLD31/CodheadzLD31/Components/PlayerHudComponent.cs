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
        private Microsoft.Xna.Framework.Vector2 scorePosition;

        public PlayerHudComponent(LDGame game):base(game)
        {
            this.DrawOrder = 200;
            onLevelEndToken = Messages.Messenger.Default.Subscribe<Messages.LevelEndMessage>(OnLevelEnd);
        }

        private void OnLevelEnd(Messages.LevelEndMessage obj)
        {
            TotalScore += obj.Content.JumpScore;
            if(obj.Content.Dead)
            {
                ReduceLives();
            }
        }

        private void ReduceLives()
        {
            Lives--;
        }

        public int TotalScore { get; private set; }

        public int Lives { get; private set; }
        public void StartSession()
        {
            Lives = 3;
            TotalScore = 0;
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

            int x = Game.GraphicsDevice.PresentationParameters.BackBufferWidth - (int)largeFont.MeasureString( TotalScore.ToString()).X -15;
            scorePosition = new Microsoft.Xna.Framework.Vector2(x, 3);
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            hudBackground.Draw(gameTime, spriteBatch);
            playerLivesNode.Draw(gameTime, spriteBatch);
            spriteBatch.DrawStringShaddow(largeFont, TotalScore.ToString(), scorePosition, FontColor);
            spriteBatch.End();
        }

    }
}
