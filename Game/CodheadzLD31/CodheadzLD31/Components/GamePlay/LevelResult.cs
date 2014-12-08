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
                return (int)((200 - Math.Abs(TurdCenter.X - ToiletCenter.X)) * factor * WindSpeed);
            }
        }

        public float WindSpeed { get; set; }

        public int TotalScore { get; set; }

        public float FinalVelocity { get; set; }
    }
}
