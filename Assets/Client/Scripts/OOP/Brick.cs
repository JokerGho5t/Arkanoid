using UnityEngine;

using JokerGho5t.MessageSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    BlockComponent data;
    SpriteRenderer sprite;

    int curHP;

    public void Init(BlockComponent data)
    {
        this.data = data;

        sprite = GetComponent<SpriteRenderer>();

        sprite.sprite = data.sprite;

        Restart();

        Message.AddListener("RestartLevel", Restart);
    }

    private void Restart()
    {
        curHP = data.life;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            Hit();
        }

        Message.Send("RemoveBrick");
    }

    public void Hit()
    {
        curHP--;

        if (curHP == 0)
        {
            PoolManager.ReleaseObject(gameObject);
        }
    }
}
