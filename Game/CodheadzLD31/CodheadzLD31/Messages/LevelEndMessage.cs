using CodheadzLD31.Components.GamePlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public class LevelEndMessage:TinyMessenger.GenericTinyMessage<LevelResult>
    {
        public LevelEndMessage(object sender,LevelResult result )
            : base(sender, result)
        {

        }
    }
}
