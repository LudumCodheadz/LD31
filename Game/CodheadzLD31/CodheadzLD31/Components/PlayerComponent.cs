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
        
        public PlayerComponent(LDGame game):base(game)
        {
            inputManager = game.Services.GetService<InputManager>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            playerNode = new SpriteScreenNode(Game, "Sprites\\Player");
            playerNode.Offset = new Microsoft.Xna.Framework.Vector2(400, 100);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if(inputManager.IsKeyDown(Keys.A) || inputManager.IsKeyDown(Keys.Left))
            {
                playerNode.Offset = playerNode.Offset - new Vector2(gameTime.ElapsedGameTime.Milliseconds * moveRate, 0);
            }

            if (inputManager.IsKeyDown(Keys.D) || inputManager.IsKeyDown(Keys.Right))
            {
                playerNode.Offset = playerNode.Offset + new Vector2(gameTime.ElapsedGameTime.Milliseconds * moveRate, 0);
            }

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
