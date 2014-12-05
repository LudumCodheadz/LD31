using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public class GameStateChangeMessage:TinyMessenger.GenericTinyMessage<GameStates.GameStates>
    {
        public GameStateChangeMessage(object sender, GameStates.GameStates content):base(sender, content)
        {

        }
    }
}
