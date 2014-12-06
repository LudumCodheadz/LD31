using CodheadzLD31.Components.GamePlay;
using CodheadzLD31.Input;
using CodheadzLD31.Utils;
using CodheadzLD31.GameStates;
using Microsoft.Xna.Framework;

namespace CodheadzLD31.Components
{
    public class PlayerComponent: ComponentBase
    {
        private TurdNode turdNode;
        private BottomNode bottomRoot;
        private PlayerState state;

        private TinyMessenger.TinyMessageSubscriptionToken levelStartToken;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;
        private bool pressInProgress;
        
        public PlayerComponent(LDGame game):base(game)
        {
            levelStartToken = Messages.Messenger.Default.Subscribe<Messages.LevelStartMessage>(OnLevelStart);
            inputToken = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInputChange);
        }

        private void OnInputChange(Messages.InputChangeStateMessage obj)
        {
            if(GameStateManager.CurrentState == GameStates.GameStates.Playing)
            {
                if (!obj.Content.IsReleased() && !pressInProgress)
                {
                    switch (state)
                    {
                        case PlayerState.Turtle:
                            turdNode.IsEnabled = true;
                            turdNode.StartDropping();
                            state = PlayerState.Dropping;
                            break;
                        case PlayerState.Dropping:
                            turdNode.OpenChute();
                            state = PlayerState.ChuteOpen;
                            break;
                        case PlayerState.ChuteOpen:
                            turdNode.CutChute();
                            state = PlayerState.ChuteCut;
                            break;
                    }
                    pressInProgress = true;
                }
                else
                {
                    pressInProgress = false;
                }
            }
        }

        private void OnLevelStart(Messages.LevelStartMessage obj)
        {
            turdNode.ResetPlayerPosition();
            state = PlayerState.Turtle;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            bottomRoot = new BottomNode(this.Game);
            turdNode = new TurdNode(this.Game);
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            turdNode.ExhaustPort = bottomRoot.ExhaustPort;
            turdNode.Update(gameTime);
            bottomRoot.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            turdNode.Draw(gameTime, spriteBatch);
            bottomRoot.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(largeFont, state.ToString(), new Vector2(50, 300), Color.Red);
            spriteBatch.End();
        }
        
    }
}
