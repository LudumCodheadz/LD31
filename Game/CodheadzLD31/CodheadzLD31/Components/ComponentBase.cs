using CodheadzLD31.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CodheadzLD31.Components
{
    public class ComponentBase:DrawableGameComponent
    {
        protected Texture2D pixel;
        protected SpriteFont normalFont;
        protected SpriteFont largeFont;
        protected SpriteBatch spriteBatch;
        private TinyMessenger.TinyMessageSubscriptionToken onGameStateChange;

        public ComponentBase(LDGame game)
            :base(game)
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            onGameStateChange = Messages.Messenger.Default.Subscribe<Messages.GameStateChangeMessage>(OnGameStateChange);
            GameStateManager = game.Services.GetService<GameStateManager>();
        }
        protected virtual void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
        
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            pixel = Game.Content.Load<Texture2D>("Pixel");
            normalFont = Game.Content.Load<SpriteFont>("Fonts\\Normal");
            largeFont = Game.Content.Load<SpriteFont>("Fonts\\Large");
        }

        public Color FontColor { get { return Color.OrangeRed; } }
        public GameStateManager GameStateManager { get; private set; }
    }
}
