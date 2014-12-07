using CodheadzLD31.Graphics.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodheadzLD31.Components.GamePlay
{
    public class ToiletNode:ScreenNode
    {

        private SpriteScreenNode toiletNode;
        public ToiletNode(Game game):base(game)
        {
            toiletNode = new SpriteScreenNode(game, "Sprites\\Toilet");
            toiletNode.Scale = 2;
            this.AddChild(toiletNode);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public Point ToiletCenter
        {
            get
            {
                return this.toiletNode.Sprite.Rectangle.Center;
            }
        }
    }
}
