using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Input
{
    public class InputState
    {
        public InputState()
        {
            LeftMouseClicked = false;
            RightMouseClicked = false;
            Position = new Microsoft.Xna.Framework.Point(-1, -1);
            PressedKeys = new Microsoft.Xna.Framework.Input.Keys[0];
        }
        
        public bool LeftMouseClicked;
        
        public bool RightMouseClicked;
        
        public Microsoft.Xna.Framework.Point Position { get; set; }
        
        public Microsoft.Xna.Framework.Input.Keys[] PressedKeys { get; set; }
        
        public bool IsKeyPressed(Keys key)
        {
            return PressedKeys.Contains(key);
        }

        public bool IsReleased()
        {
            return PressedKeys.Count() == 0 && !LeftMouseClicked && !RightMouseClicked;
        }
    }
}
