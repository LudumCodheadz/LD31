using CodheadzLD31.Components.GamePlay;
using CodheadzLD31.Input;
using CodheadzLD31.Utils;

namespace CodheadzLD31.Components
{
    public class PlayerComponent: ComponentBase
    {
        private TurdNode turdNode;
        private BottomNode bottomRoot;
        
        private InputManager inputManager;
        private TinyMessenger.TinyMessageSubscriptionToken levelStartToken;
        
        public PlayerComponent(LDGame game):base(game)
        {
            inputManager = game.Services.GetService<InputManager>();
            levelStartToken = Messages.Messenger.Default.Subscribe<Messages.LevelStartMessage>(OnLevelStart);
        }

        private void OnLevelStart(Messages.LevelStartMessage obj)
        {
            turdNode.ResetPlayerPosition();
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

            turdNode.Update(gameTime);
            bottomRoot.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            turdNode.Draw(gameTime, spriteBatch);
            bottomRoot.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
