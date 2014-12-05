using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CodheadzLD31.Graphics.SceneGraph
{
    public class ScreenNode
    {
        private Game game;
        public Game Game { get { return game; } }
        public ScreenNode(Game game)
        {
            this.game = game;
            Children = new List<ScreenNode>();
            IsEnabled = true;
            IsVisible = true;
            Scale = 1f;
            NodeScale = 1f;
        }

        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        
        public ScreenNode Parent { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Offset { get; set; }

        public float Scale { get; set; }
        public float NodeScale { get; private set; }


        public List<ScreenNode> Children { get; private set; }
        public virtual void AddChild(ScreenNode screenNode)
        {
            this.Children.Add(screenNode);
            screenNode.Parent = this;
        }

        public virtual void Update(GameTime gameTime)
        {

            if (this.Parent != null)
                NodeScale = this.Parent.NodeScale * Scale;
            else
                NodeScale = Scale;

            if (this.Parent != null)
                this.Position = Parent.Position + (this.Offset);
            else
                this.Position = (this.Offset);

            foreach (var node in Children)
            {
                node.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
                foreach (var node in Children)
                {
                    node.Draw(gameTime, spriteBatch);
                }
        }

    }
}
