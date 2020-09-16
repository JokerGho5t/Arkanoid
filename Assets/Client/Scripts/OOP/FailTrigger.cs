using UnityEngine;

using JokerGho5t.MessageSystem;

public class FailTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            Message.Send("RestartLevel");
        }
    }
}
