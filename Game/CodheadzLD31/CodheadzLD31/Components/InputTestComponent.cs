using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodheadzLD31.Utils;

namespace CodheadzLD31.Components
{
    public class InputTestComponent:ComponentBase
    {
        private string state = string.Empty;
        private TinyMessenger.TinyMessageSubscriptionToken inputMessage;
        public InputTestComponent(LDGame game):base(game)
        {
            inputMessage = Messages.Messenger.Default.Subscribe<Messages.InputChangeStateMessage>(OnInputChanged);
        }
        private void OnInputChanged(Messages.InputChangeStateMessage obj)
        {
            var keys = string.Empty;
            foreach(var k in obj.Content.PressedKeys)
            {
                keys += k.ToString();
            }
            state = string.Format("Left:{0} Right{1} Position:{2} Keys:{3}", obj.Content.LeftMouseClicked, obj.Content.RightMouseClicked, obj.Content.Position.ToString(), keys);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.BeginPixel();
            spriteBatch.DrawStringShaddow(normalFont, state, new Vector2(50, 50), Color.Red);
            spriteBatch.End();
        }
        
    }
}
