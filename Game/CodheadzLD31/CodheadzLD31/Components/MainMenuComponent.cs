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
        private string playText = "Press Space to Play";
        private Vector2 playPosition;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;

        public MainMenuComponent(LDGame game):base(game)
        {
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
                //TODO: should decomplese these and fire a message
                var levelManager = Game.Services.GetService<LevelManagerComponent>();
                var playerHud = Game.Services.GetService<PlayerHudComponent>();
                playerHud.ResetLives();
                levelManager.SetLevel(1);
                Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.Playing));
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            int x = (int)(GraphicsDevice.PresentationParameters.BackBufferWidth - largeFont.MeasureString(playText).X)/2;
            int y = GraphicsDevice.PresentationParameters.BackBufferHeight/2;
            playPosition = new Vector2(x, y);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            spriteBatch.DrawString(largeFont, playText, playPosition, Color.DarkBlue);
            spriteBatch.End();
        }

    }
}
