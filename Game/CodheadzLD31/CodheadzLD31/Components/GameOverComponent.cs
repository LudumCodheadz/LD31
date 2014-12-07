using CodheadzLD31.Messages;
using CodheadzLD31.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class GameOverComponent : ComponentBase
    {
        private string gameOverText = "Game Over";
        private Vector2 gameOverTextPosition;

        private Utils.Timers.Timer displayHoldTimer;
        private bool allowNextState = false;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;

        public GameOverComponent(LDGame game)
            : base(game)
        {
            this.DrawOrder = 200;
            inputToken = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInputChange);
            displayHoldTimer = new Utils.Timers.Timer(new TimeSpan(0, 0, 1), Utils.Timers.TimerMode.Single);
            displayHoldTimer.TimeReached += displayHoldTimer_TimeReached;

            gameOverTextPosition = new Vector2(100, 100);
        }

        private void OnInputChange(InputChangeStateMessage obj)
        {
            if (GameStateManager.CurrentState == GameStates.GameStates.GameOver)
            {
                if (allowNextState)
                {
                    Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.MainMenu));
                }
            }
        }

        void displayHoldTimer_TimeReached(object sender, EventArgs e)
        {
            allowNextState = true;
        }

        protected override void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
            base.OnGameStateChange(obj);
            if (obj.Content == GameStates.GameStates.GameOver)
            {
                this.Enabled = true;
                this.Visible = true;
                allowNextState = false;
                displayHoldTimer.Start();
            }
            else
            {
                this.Enabled = false; 
                this.Visible = false;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.BeginPixel();
            spriteBatch.DrawString(largeFont, gameOverText, gameOverTextPosition, Color.Yellow);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            displayHoldTimer.Update(gameTime);
        }
    }

}
