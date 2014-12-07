using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Utils;
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
        private SpriteScreenNode turdChute;
        private Vector2 chuteCutDriftRate;
        private float velocity = 0f;
        private ChuteState chuteState;
        private EnvironmentComponent environment;

        public TurdNode(Game game)
            : base(game)
        {

            environment = Game.Services.GetService<EnvironmentComponent>();

            new ScreenNode(this.Game);

            turdChute = new SpriteScreenNode(Game, "Sprites\\Chute");
            this.AddChild(turdChute);

            turdBody = new SpriteScreenNode(Game, "Sprites\\Turd");
            this.AddChild(turdBody);

            this.Scale = 1.5f;

            this.Update(new GameTime());

            ResetPlayerPosition();
        }

        public void ResetPlayerPosition()
        {
            turdBody.Offset = new Vector2(0, 0);
            SetChutOffset();
            turdChute.IsVisible = false;
            chuteState = ChuteState.Unopened;

            this.IsEnabled = false;
        }

        private void SetChutOffset()
        {
            turdChute.Offset = new Vector2(turdBody.Sprite.Rectangle.Center.X - turdChute.Sprite.Rectangle.Width /2,turdBody.Offset.Y - turdChute.Sprite.Rectangle.Height );
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PlayerState == Components.PlayerState.Turtle)
            {
                turdBody.Offset = this.ExhaustPort - new Vector2(turdBody.Sprite.Rectangle.Width / 2, 0);
                SetChutOffset();
            }

            if (!IsEnabled) return;

            float chuteVelocity = 0f;
            float windDrift = environment.WindSpeed * gameTime.ElapsedGameTime.Milliseconds;

            velocity += gameTime.ElapsedGameTime.Milliseconds * gravityRate;

            if (velocity > 0.75)
                velocity = 0.75f;

            if (chuteState == ChuteState.Cut)
            {
                turdChute.Offset += chuteCutDriftRate;
                turdChute.Offset += new Vector2(windDrift, 0);
            }
            else
            {
                if (chuteState == ChuteState.Opened && velocity > 0.075)
                    velocity -= gameTime.ElapsedGameTime.Milliseconds * 0.003f;

                chuteVelocity = velocity;
                turdChute.Offset += new Vector2(0, chuteVelocity * gameTime.ElapsedGameTime.Milliseconds);
            }

            if(this.PlayerState != Components.PlayerState.Dead
                && this.PlayerState != Components.PlayerState.Down)
            turdBody.Offset += new Vector2(0, gameTime.ElapsedGameTime.Milliseconds * velocity);
            
            if(chuteState == ChuteState.Opened)
            {
                turdBody.Offset += new Vector2(windDrift, 0);
                turdChute.Offset += new Vector2(windDrift, 0);
            }
        }

        public void StartDropping()
        {
            chuteCutDriftRate = Vector2.Zero;
            gravityRate = droppingRate;
            chuteState = ChuteState.Unopened;
        }

        public void OpenChute()
        {
            chuteState = ChuteState.Opened;
            turdChute.IsVisible = true;
            chuteCutDriftRate = Vector2.Zero;
            gravityRate = chuteRate;
        }

        internal void CutChute()
        {
            if (chuteState != ChuteState.Opened) return;

            chuteState = ChuteState.Cut;

            gravityRate = droppingRate;

            float x = 0.5f;
            if (turdChute.Sprite.Position.X < Game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2)
            {
                x = x * -1f;
            }

            chuteCutDriftRate = new Vector2(x, 0.7f);
        }

        public PlayerState PlayerState {get;set;}
        public Vector2 ExhaustPort { get; set; }
        public SpriteScreenNode Body { get { return turdBody; } }
        public float Velocity { get { return velocity; } }

        public void Down(int groundY)
        {
            CutChute();

            gravityRate = 0f;
            velocity = 0f;
            int y = groundY - turdBody.Sprite.Rectangle.Height;
            turdBody.Offset = new Vector2(turdBody.Offset.X, y);

        }

        public void Dead(int groundY)
        {
            CutChute();

            gravityRate = 0f;
            velocity = 0f;
            int y = groundY - turdBody.Sprite.Rectangle.Height;
            turdBody.Offset = new Vector2(turdBody.Offset.X, y);
        }

        public Point TurdCenter
        {
            get
            {
                return this.turdBody.Sprite.Rectangle.Center;
            }
        }

    }
}
