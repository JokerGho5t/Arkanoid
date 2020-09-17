using UnityEngine;

using TMPro;

using JokerGho5t.MessageSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveLine : PowerUp
{
    [SerializeField]
    private TextMeshPro textHP_L = null;
    [SerializeField]
    private TextMeshPro textHP_R = null;
    [SerializeField]
    private int HP = 1;
    [SerializeField]
    private float speed = 5;

    private int curHP;

    protected override void Alive()
    {
        curHP = HP;

        var pos = transform.position;
        pos.x = 0;
        transform.position = pos;

        textHP_L.text = curHP.ToString();
        textHP_R.text = curHP.ToString();

        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    protected override void Use(Collider2D collider) { }

    protected override void Use(Collision2D collider)
    {
        if (collider.transform.tag == "Ball")
        {
            Hit();
        }
        else if (collider.transform.tag == "Paddle")
        {
            Message.Send("RestartLevel");
            PoolManager.ReleaseObject(gameObject);
        }
    }

    private void Hit()
    {
        curHP--;

        if (curHP == 0)
            PoolManager.ReleaseObject(gameObject);
        else
        {
            textHP_L.text = curHP.ToString();
            textHP_R.text = curHP.ToString();
        }
    }
}
