using CodheadzLD31.Graphics.SceneGraph;
using CodheadzLD31.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class PlayerComponent: ComponentBase
    {
        private float gravityRate = 0.1f;
        private InputManager inputManager;
        private ScreenNode playerRoot;
        private SpriteScreenNode turdBody;
        private TinyMessenger.TinyMessageSubscriptionToken levelStartToken;
        
        public PlayerComponent(LDGame game):base(game)
        {
            inputManager = game.Services.GetService<InputManager>();
            levelStartToken = Messages.Messenger.Default.Subscribe<Messages.LevelStartMessage>(OnLevelStart);
        }

        private void OnLevelStart(Messages.LevelStartMessage obj)
        {
            ResetPlayerPosition();
        }

        private void ResetPlayerPosition()
        {
            int x = (Game.GraphicsDevice.PresentationParameters.BackBufferWidth - turdBody.Sprite.Rectangle.Width) / 2;
            this.playerRoot.Offset = new Vector2(x, 0);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            playerRoot = new ScreenNode(this.Game);
            turdBody = new SpriteScreenNode(Game, "Sprites\\Player");
            playerRoot.AddChild(turdBody);
            playerRoot.Update(new GameTime());
            
            ResetPlayerPosition();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            playerRoot.Update(gameTime);
            float x = playerRoot.Offset.X;
            float y = playerRoot.Offset.Y + gameTime.ElapsedGameTime.Milliseconds * gravityRate;
            playerRoot.Offset = new Vector2(x, y);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            turdBody.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
