using JokerGho5t.MessageSystem;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    protected abstract void Alive();
    protected abstract void Use(Collider2D collider);
    protected abstract void Use(Collision2D collider);

    private void Start()
    {
        Message.AddListener("RestartLevel", Restart);
    }

    protected void Restart()
    {
        if (gameObject.activeInHierarchy)
            PoolManager.ReleaseObject(gameObject);
    }

    private void OnEnable()
    {
        Alive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Use(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Use(collision);
    }
}
