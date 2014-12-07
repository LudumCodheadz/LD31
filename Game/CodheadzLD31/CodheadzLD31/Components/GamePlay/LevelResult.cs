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
    }
}
