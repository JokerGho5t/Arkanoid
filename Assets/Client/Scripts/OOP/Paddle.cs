using UnityEngine;

using JokerGho5t.MessageSystem;

public class Paddle : MonoBehaviour
{
    private PaddleComponent data;
    private Rigidbody2D rb;

    private float inputX = 0;

    public void Init(PaddleComponent data)
    {
        this.data = data;
        rb = GetComponent<Rigidbody2D>();

        Message.AddListener("OnFixedUpdate", OnFixedUpdate);
        Message.AddListener("RestartLevel", Restart);
    }

    private void OnFixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        rb.velocity = Vector2.right * inputX * data.SpeedPaddle;
    }

    private void Restart()
    {
        var pos = transform.position;
        pos.x = 0;

        transform.position = pos;
    }
}
