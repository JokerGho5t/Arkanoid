using UnityEngine;

using JokerGho5t.MessageSystem;

[RequireComponent(typeof(Rigidbody2D))]
public sealed  class Bomb : PowerUp
{
    [SerializeField]
    private float speed = 5;

    protected override void Alive() 
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    protected override void Use(Collider2D collider)
    {
        if(collider.tag == "Paddle")
        {
            Message.Send("RestartLevel");
            PoolManager.ReleaseObject(gameObject);
        }
        else if(collider.tag == "Fail")
        {
            PoolManager.ReleaseObject(gameObject);
        }
        
    }

    protected override void Use(Collision2D collider) { }
}
