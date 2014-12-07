using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class EnvironmentComponent:ComponentBase
    {

        private float windSpeed;
        ScreenNode clouds;
        private TinyMessenger.TinyMessageSubscriptionToken onLevelStartToken;
        private Random rnd;
        
        public EnvironmentComponent(LDGame game)
            :base(game)
        {
            rnd = new Random();
            onLevelStartToken = Messages.Messenger.Default.Subscribe<Messages.LevelStartMessage>(OnLevelStart);
        }

        private void OnLevelStart(Messages.LevelStartMessage obj)
        {
            if(rnd.NextDouble()< 0.5f)
                windSpeed = (float)(obj.Content -1 ) * 0.02f;
            else
                windSpeed = (float)(obj.Content - 1) * -0.02f; 

            if(windSpeed <0)
                clouds.Offset = new Microsoft.Xna.Framework.Vector2(0 / 2, 0);
            else
                clouds.Offset = new Microsoft.Xna.Framework.Vector2(100 * -39 / 2, 0);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.clouds = new ScreenNode(this.Game);
            
            for (int i = 0; i < 40; i++)
            {
                var cloud = new SpriteScreenNode(this.Game, "Sprites\\Cloud");
                cloud.Scale = 0.5f + (float)rnd.NextDouble();
                cloud.Offset = new Microsoft.Xna.Framework.Vector2(i * 100, 100 + rnd.Next(400));
                clouds.AddChild(cloud);
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            clouds.Update(gameTime);
            clouds.Offset += new Microsoft.Xna.Framework.Vector2(windSpeed * gameTime.ElapsedGameTime.Milliseconds, 0);

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.BeginPixel();
            clouds.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public float WindSpeed { get { return windSpeed; } }
    }
}
