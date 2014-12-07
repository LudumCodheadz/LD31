using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components.GamePlay
{
    public class LevelResult
    {
        public int Level { get; set; }
        
        public bool Dead { get; set; }
        
        public Microsoft.Xna.Framework.Point TurdCenter { get; set; }
        
        public Microsoft.Xna.Framework.Point ToiletCenter { get; set; }
        
        public int JumpScore 
        { 
            get 
            {
                float factor = 1;
                if (Dead)
                    factor = 0.25f;
                return (int)((1000 - Math.Abs(TurdCenter.X - ToiletCenter.X))*factor); 
            } 
        }
        
        public int TotalScore { get { return 999999; } }
    }
}
