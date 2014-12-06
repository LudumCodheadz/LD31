using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components.GamePlay
{
    public class BottomNode:ScreenNode
    {
        private int leftCheckBound = 0;
        private int rightCheckBound = 0;
        private float moveRate = 0.10f;
        private SpriteScreenNode bottomSprite;

        public BottomNode(Game game):base(game)
        {
            this.Offset = new Vector2(0, -5f);
            bottomSprite = new SpriteScreenNode(this.Game, "sprites/Bottom");
            bottomSprite.Scale = 0.5f;
            this.AddChild(bottomSprite);
            this.Update(new GameTime());

            leftCheckBound = -20;
            rightCheckBound = game.GraphicsDevice.PresentationParameters.BackBufferWidth - bottomSprite.Sprite.Rectangle.Width + 20;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var newOffset = new Vector2(this.Offset.X + moveRate * gameTime.ElapsedGameTime.Milliseconds, this.Offset.Y);
            this.Offset = newOffset;

            if (this.Offset.X < leftCheckBound || this.Offset.X > rightCheckBound)
                moveRate = moveRate * -1;
        }

    }
}
