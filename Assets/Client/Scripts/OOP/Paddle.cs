using UnityEngine;

using JokerGho5t.MessageSystem;

public class Paddle : MonoBehaviour
{
    #region Private Variables

    private PaddleComponent data;
    private Rigidbody2D rb;

    private float inputX = 0;

    #endregion

    #region Public Function

    public void Init(PaddleComponent data)
    {
        this.data = data;
        rb = GetComponent<Rigidbody2D>();

        Message.AddListener("RestartLevel", Restart);
        Message.AddListener("StartLevel", StartLevel);
    }

    #endregion

    #region Private Function

    private void OnFixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        rb.velocity = Vector2.right * inputX * data.SpeedPaddle;
    }

    private void StartLevel()
    {
        Message.AddListener("OnFixedUpdate", OnFixedUpdate);
    }

    private void Restart()
    {
        Message.RemoveListener("OnFixedUpdate", OnFixedUpdate);

        rb.velocity = Vector2.zero;

        var pos = transform.position;
        pos.x = 0;

        transform.position = pos;

    }

    #endregion
}
