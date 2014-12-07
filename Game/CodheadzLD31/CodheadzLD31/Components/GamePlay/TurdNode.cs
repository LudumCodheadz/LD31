using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components.GamePlay
{
    public class TurdNode : ScreenNode
    {
        private const float stopRate = 0.0f;
        private const float droppingRate = 0.00075f;
        private const float chuteRate = 0.00075f;
        private float gravityRate = 0.0f;
        private SpriteScreenNode turdBody;
        private SpriteScreenNode chute;
        private Vector2 chuteCutDriftRate;
        private float velocity = 0f;
        private ChuteState chuteState;


        public TurdNode(Game game)
            : base(game)
        {
            new ScreenNode(this.Game);

            turdBody = new SpriteScreenNode(Game, "Sprites\\Turd");
            turdBody.Scale = 1f;
            this.AddChild(turdBody);

            chute = new SpriteScreenNode(Game, "Sprites\\Chute");
            chute.Scale = 1f;
            this.AddChild(chute);


            this.Scale = 1.5f;

            this.Update(new GameTime());

            ResetPlayerPosition();
        }

        public void ResetPlayerPosition()
        {
            int x = (Game.GraphicsDevice.PresentationParameters.BackBufferWidth - turdBody.Sprite.Rectangle.Width) / 2;
            this.Offset = new Vector2(x, 0);

            chute.Offset = new Vector2(-(chute.Sprite.Rectangle.Width - turdBody.Sprite.Rectangle.Width) / 2, -35);
            chute.IsVisible = false;

            this.IsEnabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsEnabled) return;

            float chuteVelocity = 0f;

            velocity += gameTime.ElapsedGameTime.Milliseconds * gravityRate;

            if (velocity > 0.95)
                velocity = 0.95f;

            if (chuteState == ChuteState.Cut)
            {
                chute.Offset += chuteCutDriftRate;
            }
            else
            {
                if (chuteState == ChuteState.Opened && velocity > 0.05)
                    velocity -= gameTime.ElapsedGameTime.Milliseconds * 0.002f;

                chuteVelocity = velocity;
                chute.Offset += new Vector2(0, chuteVelocity * gameTime.ElapsedGameTime.Milliseconds);
            }

            turdBody.Offset += new Vector2(0, gameTime.ElapsedGameTime.Milliseconds * velocity);

        }

        public void Launch(Vector2 position)
        {
            this.IsEnabled = true;
        }

        internal void StartDropping()
        {
            this.Offset = new Vector2(this.ExhaustPort.X, 0);
            chuteCutDriftRate = Vector2.Zero;
            gravityRate = droppingRate;
            chuteState = ChuteState.Unopened;
        }

        internal void OpenChute()
        {
            chuteState = ChuteState.Opened;
            chute.IsVisible = true;
            chuteCutDriftRate = Vector2.Zero;
            gravityRate = chuteRate;
        }

        internal void CutChute()
        {
            if (chuteState != ChuteState.Opened) return;

            chuteState = ChuteState.Cut;

            gravityRate = droppingRate;

            float x = 0.5f;
            if (chute.Sprite.Position.X < Game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2)
            {
                x = x * -1f;
            }

            chuteCutDriftRate = new Vector2(x, 0.7f);
        }

        public Vector2 ExhaustPort { get; set; }
        public SpriteScreenNode Body { get { return turdBody; } }
        public float Velocity { get { return velocity; } }

        internal void Down()
        {
            CutChute();

            gravityRate = 0f;
            velocity = 0f;

        }

        internal void Dead()
        {
            CutChute();

            gravityRate = 0f;
            velocity = 0f;
        }
    }
}
