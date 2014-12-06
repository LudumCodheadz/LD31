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
        private float moveRate = 0.03f;
        private InputManager inputManager;
        private SpriteScreenNode  playerNode;
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
            int x = (Game.GraphicsDevice.PresentationParameters.BackBufferWidth - playerNode.Sprite.Rectangle.Width) / 2;
            this.playerNode.Offset = new Vector2(x, 0);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            playerNode = new SpriteScreenNode(Game, "Sprites\\Player");
            playerNode.Update(new GameTime());
            ResetPlayerPosition();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        
            playerNode.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            playerNode.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
