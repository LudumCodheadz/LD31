using CodheadzLD31.Messages;
using CodheadzLD31.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class LevelOverSummaryComponent : ComponentBase
    {
        private List<string> deadSummaryStrings = new List<string>() { "Too Fast", "Died Too Young", "DEAD!" };
        private List<string> aliveSummaryStrings = new List<string>() { "Well Done", "Top Drop", "You live to stink again" };

        private string summaryText = string.Empty;
        private string jumpScore = string.Empty;
        private string totalScore = string.Empty;

        private Vector2 summaryTextPosition;
        private Vector2 jumpScorePosition;
        private Vector2 totalScorePosition;

        private TinyMessenger.TinyMessageSubscriptionToken onLevelEndToken;
        private Random rnd;

        private Utils.Timers.Timer displayHoldTimer;
        private bool allowNextState = false;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;
        private GamePlay.LevelResult levelState;

        public LevelOverSummaryComponent(LDGame game)
            : base(game)
        {
            rnd = new Random();
            onLevelEndToken = Messenger.Default.Subscribe<LevelEndMessage>(OnLevelEnd);
            inputToken = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInputChange);

            displayHoldTimer = new Utils.Timers.Timer(new TimeSpan(0, 0, 1), Utils.Timers.TimerMode.Single);
            displayHoldTimer.TimeReached += displayHoldTimer_TimeReached;
        }

        private void OnInputChange(InputChangeStateMessage obj)
        {
            if (GameStateManager.CurrentState == GameStates.GameStates.LevelOver)
            {
                if (allowNextState)
                {

                    var playerHud = Game.Services.GetService<PlayerHudComponent>();
                    if (playerHud.Lives == 0)
                    {
                        Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.GameOver));
                    }
                    else 
                    {
                        var levelManager = Game.Services.GetService<LevelManagerComponent>();
                        levelManager.NextLevel();
                        Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.Playing));
                    }
                }

                

            }
        }

        void displayHoldTimer_TimeReached(object sender, EventArgs e)
        {
            allowNextState = true;
        }

        private void OnLevelEnd(LevelEndMessage obj)
        {
            levelState = obj.Content;

            if (obj.Content.Dead)
                this.summaryText = GetRandomString(this.deadSummaryStrings);
            else
                this.summaryText = GetRandomString(this.aliveSummaryStrings);

            jumpScore = string.Format("Jump Score:   {0}", obj.Content.JumpScore);
            totalScore = string.Format("Total Score: {0}", obj.Content.TotalScore);

            summaryTextPosition = new Vector2(100, 200);
            jumpScorePosition = summaryTextPosition + new Vector2(0, largeFont.LineSpacing + 10);
            totalScorePosition = jumpScorePosition + new Vector2(0, largeFont.LineSpacing + 10);
        }

        private string GetRandomString(List<string> strings)
        {
            return strings[rnd.Next(strings.Count())];
        }

        protected override void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
            base.OnGameStateChange(obj);
            if (obj.Content == GameStates.GameStates.LevelOver)
            {
                this.Enabled = true;
                this.Visible = true;
                allowNextState = false;
                displayHoldTimer.Start();
            }
            else if (obj.Content == GameStates.GameStates.GameOver)
            {
                this.Enabled = true;
                this.Visible = true;
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
            spriteBatch.DrawString(largeFont, summaryText, summaryTextPosition, Color.DarkGreen);
            spriteBatch.DrawString(largeFont, jumpScore, jumpScorePosition, Color.DarkGreen);
            spriteBatch.DrawString(largeFont, totalScore, totalScorePosition, Color.DarkGreen);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            displayHoldTimer.Update(gameTime);
        }
    }

}
