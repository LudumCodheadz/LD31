using CodheadzLD31.Messages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.GameStates
{
    public class GameStateManager:Components.ComponentBase
    {
        private TinyMessenger.TinyMessageSubscriptionToken gameStateToken;
        public GameStateManager(LDGame game):base(game)
        {
            gameStateToken = Messenger.Default.Subscribe<Messages.GameStateChangeMessage>(OnGameStateChanged);
            Messenger.Default.Publish(new GameStateChangeMessage(this, GameStates.MainMenu));
        }

        private void OnGameStateChanged(GameStateChangeMessage obj)
        {
            this.CurrentState = obj.Content;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
#if DEBUG
            var x = this.GraphicsDevice.PresentationParameters.BackBufferWidth - 100;
            spriteBatch.BeginPixel();
            spriteBatch.DrawString(this.normalFont, this.CurrentState.ToString(), new Vector2(x, 10), Color.Yellow);
            spriteBatch.End();
#endif
        }

        public GameStates CurrentState { get; private set; }

    }
}
