using CodheadzLD31.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public class InputChangeStateMessage:TinyMessenger.GenericTinyMessage<InputState>
    {
        public InputChangeStateMessage(InputManager sender, InputState state):base(sender, state)
        {

        }
    }
}
