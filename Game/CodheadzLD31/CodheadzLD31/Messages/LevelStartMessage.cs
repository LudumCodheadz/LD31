using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public class LevelStartMessage:TinyMessenger.GenericTinyMessage<int>
    {
        public LevelStartMessage(object sender, int level):base(sender, level)
        {

        }
    }
}
