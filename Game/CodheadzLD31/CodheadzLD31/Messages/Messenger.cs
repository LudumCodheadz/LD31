using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Messages
{
    public static class Messenger
    {
        static Messenger()
        {
            Default = new TinyMessenger.TinyMessengerHub();
        }

        public static TinyMessenger.TinyMessengerHub Default { get; private set; }
    }
}
