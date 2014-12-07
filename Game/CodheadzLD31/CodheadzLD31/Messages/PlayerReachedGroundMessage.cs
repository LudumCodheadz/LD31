using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public class PlayerReachedGroundMessage:TinyMessenger.GenericTinyMessage<float>
    {
        public PlayerReachedGroundMessage(object sender, float velocity)
            :base(sender, velocity)
        {

        }
    }
}
