using UnityEngine;

using JokerGho5t.MessageSystem;

public class Ball : MonoBehaviour
{
    private BallComponent data;
    private Rigidbody2D rb;

    public void Init(BallComponent data)
    {
        this.data = data;
        rb = GetComponent<Rigidbody2D>();
        Restart();

        Message.AddListener("RestartLevel", Restart);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Paddle")
        {
            float x = hitFactor(transform.position,
                              collision.transform.position,
                              collision.collider.bounds.size.x);

            Vector2 dir = new Vector2(x, 1).normalized;

            rb.velocity = dir * data.SpeedBall;
        }
    }

    private float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleWidth)
    {
        //              Ball
        //               ||
        //               \/
        // x:  -1..-0.5..0..0.5..1
        //       ---------------         <------- paddle

        return (ballPos.x - paddlePos.x) / paddleWidth;
    }

    private void Restart()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.up * data.SpeedBall;
    }
}
