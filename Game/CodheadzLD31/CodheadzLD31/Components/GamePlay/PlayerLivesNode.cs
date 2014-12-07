using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components.GamePlay
{
    public class PlayerLivesNode:ScreenNode
    {
        ScreenNode[] turds = new ScreenNode[3];

        public PlayerLivesNode(Game game):base(game)
        {
            var offset = new Vector2(0,0);
            for (int i = 0; i < 3; i++)
            {
                var turdNode = new SpriteScreenNode(this.Game, "Sprites\\Turd");
                turdNode.Scale = 1.5f;
                turdNode.Offset = offset;
                turdNode.Update(new GameTime());
                offset += new Vector2(turdNode.Sprite.Rectangle.Width + 4, 0);
                this.turds[i] = turdNode;
                this.AddChild(turdNode);
            }    
        }

        public int Lives { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Lives > 0)
                turds[0].IsVisible = true;
            else
                turds[0].IsVisible = false;
            
            if (Lives > 1)
                turds[1].IsVisible = true;
            else
                turds[1].IsVisible = false;

            if (Lives > 2)
                turds[2].IsVisible = true;
            else
                turds[2].IsVisible = false;
        }

    }
}
