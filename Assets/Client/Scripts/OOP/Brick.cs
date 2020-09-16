using UnityEngine;

using JokerGho5t.MessageSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    #region Private Variables

    BlockComponent data;
    SpriteRenderer sprite;

    int curHP;

    #endregion

    #region Public Function

    public void Init(BlockComponent data)
    {
        this.data = data;

        sprite = GetComponent<SpriteRenderer>();

        sprite.sprite = data.sprite;

        Restart();

        Message.AddListener("RestartLevel", Restart);
    }

    public void Hit()
    {
        curHP--;

        if (curHP == 0)
        {
            PoolManager.ReleaseObject(gameObject);
        }
    }

    #endregion

    #region Private Function

    private void Restart()
    {
        curHP = data.life;
    }

    #endregion

    #region Physic Event

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            Hit();
        }

        Message.Send("RemoveBrick");
    }

    #endregion

}
