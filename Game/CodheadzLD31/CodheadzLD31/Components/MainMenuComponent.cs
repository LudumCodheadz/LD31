using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodheadzLD31.Utils;

namespace CodheadzLD31.Components
{
    public class MainMenuComponent:ComponentBase
    {
        private string playText = "Press Space to Start";
        private string title = "Special Plops";
        private string howToTextA = "1. Press any key to launch.";
        private string howToTextB = "2. Press any key to deploy chute.";
        private string howToTextC = "3. Press any key to drop.";

        private Vector2 playPosition;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;
        private Vector2 titlePosition;
        private Vector2 howToPosition = new Vector2(100, 340);

        public MainMenuComponent(LDGame game):base(game)
        {
            this.DrawOrder = 200;
            inputToken = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInput);
        }

        protected override void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
            base.OnGameStateChange(obj);
            if(obj.Content == GameStates.GameStates.MainMenu)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }

        private void OnInput(Messages.InputChangeStateMessage obj)
        {
            if (GameStateManager.CurrentState == GameStates.GameStates.MainMenu)
            {
                var levelManager = Game.Services.GetService<LevelManagerComponent>();
                var playerHud = Game.Services.GetService<PlayerHudComponent>();
                playerHud.StartSession();
                levelManager.SetLevel(1);
                Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.Playing));
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            int x = (int)(GraphicsDevice.PresentationParameters.BackBufferWidth - largeFont.MeasureString(playText).X)/2;
            int y = 500;
            playPosition = new Vector2(x, y);
            
            x = (int)(GraphicsDevice.PresentationParameters.BackBufferWidth - largeFont.MeasureString(title).X)/2;
            titlePosition = new Vector2(x, 200);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            spriteBatch.DrawStringShaddow(largeFont, title, titlePosition, FontColor);

            spriteBatch.DrawStringShaddow(normalFont, howToTextA, howToPosition, Color.Yellow);
            spriteBatch.DrawStringShaddow(normalFont, howToTextB, howToPosition + new Vector2(0, normalFont.LineSpacing), Color.Yellow);
            spriteBatch.DrawStringShaddow(normalFont, howToTextC, howToPosition + new Vector2(0, normalFont.LineSpacing * 2), Color.Yellow);

            spriteBatch.DrawStringShaddow(largeFont, playText, playPosition, FontColor);
            spriteBatch.End();
        }

    }
}
