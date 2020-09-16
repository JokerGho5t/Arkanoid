using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JokerGho5t.MessageSystem;

namespace JokerGho5t.MessageSystem
{
    public class SendMessage : MonoBehaviour
    {
        public void Send(string NameMessag)
        {
            Message.Send(NameMessag);
        }
    }
}
