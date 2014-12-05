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
        protected SpriteFont debugFont;
        protected SpriteBatch spriteBatch;

        public ComponentBase(LDGame game)
            :base(game)
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            pixel = Game.Content.Load<Texture2D>("Sprites\\Pixel");
            debugFont = Game.Content.Load<SpriteFont>("Fonts\\Normal");
        }
    }
}
