using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Graphics.SceneGraph
{
    public class SpriteScreenNode : ScreenNode
    {
        public Color Color { get; set; }
        private Sprite sprite;
        public Sprite Sprite { get { return sprite; } set { sprite = value; } }
        public bool HorizontalFlip { get; set; }
        public bool VertitalFlip { get; set; }
        public Vector2 Origin { get { return sprite.Origin; } set { sprite.Origin = value; } }

        public SpriteScreenNode(Game game)
            : base(game)
        {
            HorizontalFlip = false;
            Color = Color.White;
        }

        public SpriteScreenNode(Game game, string assetPath)
            : base(game)
        {
            Color = Color.White;
            HorizontalFlip = false;
            this.LoadSprite(assetPath);
        }

        public SpriteScreenNode(Game game, string assetPath, bool horizontalFlip)
            : base(game)
        {
            this.Color = Color.White;
            this.HorizontalFlip = horizontalFlip;
            this.LoadSprite(assetPath);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsEnabled)
            {
                sprite.Color = this.Color;
                sprite.Position = this.Position;
                sprite.Scale = NodeScale;
                sprite.Update(gameTime);
                if (HorizontalFlip && !VertitalFlip)
                    sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                else if (!HorizontalFlip && VertitalFlip)
                    sprite.SpriteEffect = SpriteEffects.FlipVertically;
                else if (HorizontalFlip && VertitalFlip)
                    sprite.SpriteEffect = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
                sprite.Draw(gameTime, spriteBatch);

            base.Draw(gameTime, spriteBatch);
        }

        public void LoadSprite(string p)
        {
            try
            {
                sprite = new Sprite();
                sprite.Texture = Game.Content.Load<Texture2D>(p);
                sprite.DebugColor = new Color(Color.OrangeRed.R, Color.OrangeRed.G, Color.OrangeRed.B, 0.01f);
                sprite.DebugTexture = Game.Content.Load<Texture2D>("pixel");
            }
            catch (System.IO.FileNotFoundException fnf)
            {
                System.Diagnostics.Debug.WriteLine("Could not find texture:" + p);
            }
        }


    }
}
