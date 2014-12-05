using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Graphics.SceneGraph
{
    public class SpriteFontScreenNode : ScreenNode
    {
        public Color Color { get; set; }
        private SpriteFont spriteFont;
        public SpriteFont Sprite { get { return spriteFont; } set { spriteFont = value; } }
        public String Text { get; set; }

        public SpriteFontScreenNode(Game game)
            : base(game)
        {
            Color = Color.White;
        }

        public SpriteFontScreenNode(Game game, string assetPath)
            : base(game)
        {
            Color = Color.White;
            this.LoadSprite(assetPath);
        }

        public SpriteFontScreenNode(Game game, string assetPath, bool horizontalFlip)
            : base(game)
        {
            this.Color = Color.White;
            this.LoadSprite(assetPath);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible && !string.IsNullOrWhiteSpace(Text))
                spriteBatch.DrawString(spriteFont, Text, this.Position, this.Color);

            base.Draw(gameTime, spriteBatch);
        }

        public void LoadSprite(string p)
        {
            try
            {
                spriteFont = Game.Content.Load<SpriteFont>(p);
            }
            catch (System.IO.FileNotFoundException fnf)
            {
                System.Diagnostics.Debug.WriteLine("Could not find sprite font:" + p);
            }
        }
    }
}
