using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components.GamePlay
{
    public class TurdNode:ScreenNode
    {
        private const float stopRate = 0.0f;
        private const float droppingRate = 0.3f;
        private const float chuteRate = 0.15f;
        private float gravityRate = 0.0f;
        private SpriteScreenNode turdBody;

        public TurdNode(Game game)
            : base(game)
        {
            new ScreenNode(this.Game);
            turdBody = new SpriteScreenNode(Game, "Sprites\\Turd");
            turdBody.Scale = 1.5f;
            this.AddChild(turdBody);
            this.Update(new GameTime());
            ResetPlayerPosition();
        }

        public  void ResetPlayerPosition()
        {
            int x = (Game.GraphicsDevice.PresentationParameters.BackBufferWidth - turdBody.Sprite.Rectangle.Width) / 2;
            this.Offset = new Vector2(x, 0);
            this.IsEnabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (!IsEnabled) return;

            float x = this.Offset.X;
            float y = this.Offset.Y + gameTime.ElapsedGameTime.Milliseconds * gravityRate;
            this.Offset = new Vector2(x, y);
        }

        public void Launch(Vector2 position)
        {
            this.IsEnabled = true;
        }

        internal void StartDropping()
        {
            gravityRate = droppingRate;
        }

        internal void OpenChute()
        {
            gravityRate = chuteRate;
        }

        internal void CutChute()
        {
            gravityRate = droppingRate;
        }
    }
}
