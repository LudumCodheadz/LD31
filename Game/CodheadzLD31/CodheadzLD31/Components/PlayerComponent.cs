using CodheadzLD31.Components.GamePlay;
using CodheadzLD31.Input;
using CodheadzLD31.Utils;
using CodheadzLD31.GameStates;
using Microsoft.Xna.Framework;
using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Messages;

namespace CodheadzLD31.Components
{
    public class PlayerComponent : ComponentBase
    {
        private TurdNode turdNode;
        private BottomNode bottomRoot;
        private PlayerState state;
        private bool pressInProgress;

        private LevelManagerComponent levelManagerComponent;
        private LevelComponent levelComponent;

        private TinyMessenger.TinyMessageSubscriptionToken levelStartToken;
        private TinyMessenger.TinyMessageSubscriptionToken inputToken;

        public PlayerComponent(LDGame game)
            : base(game)
        {
            levelComponent = Game.Services.GetService<LevelComponent>();
            levelManagerComponent = Game.Services.GetService<LevelManagerComponent>();
            levelStartToken = Messages.Messenger.Default.Subscribe<Messages.LevelStartMessage>(OnLevelStart);
            inputToken = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInputChange);
        }

        private void OnInputChange(Messages.InputChangeStateMessage obj)
        {
            if (GameStateManager.CurrentState == GameStates.GameStates.Playing)
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

            if (state == PlayerState.ChuteOpen || state == PlayerState.ChuteCut || state == PlayerState.Dropping)
                CheckTurdForFloorHit();

        }

        private void CheckTurdForFloorHit()
        {
            for (int i = 0; i < levelComponent.GroundNode.Children.Count; i++)
            {
                var node = levelComponent.GroundNode.Children[i] as SpriteScreenNode;
                if (turdNode.Body.Sprite.Rectangle.Bottom > node.Sprite.Rectangle.Top)
                {
                    var result = new LevelResult();
                    result.Level = levelManagerComponent.CurrentLevel;
                     
                    if (turdNode.Velocity < 0.3f)
                    {
                        state = PlayerState.Down;
                        turdNode.Down(node.Sprite.Rectangle.Top);
                        result.Dead = false;
                    }
                    else
                    {
                        state = PlayerState.Dead;
                        turdNode.Dead(node.Sprite.Rectangle.Top);
                        result.Dead = true;
                    }

                    result.TurdCenter = turdNode.TurdCenter;
                    result.ToiletCenter = levelComponent.ToiletNode.ToiletCenter;
                    
                    Messages.Messenger.Default.Publish(new LevelEndMessage(this, result));
                    Messages.Messenger.Default.Publish(new Messages.GameStateChangeMessage(this, GameStates.GameStates.LevelOver));
                    break;
                }
            }
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
