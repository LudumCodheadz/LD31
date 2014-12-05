using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            pixel = Game.Content.Load<Texture2D>("Sprites\\Pixel");
            normalFont = Game.Content.Load<SpriteFont>("Fonts\\Normal");
            largeFont = Game.Content.Load<SpriteFont>("Fonts\\Large");
        }
    }
}
