using UnityEngine;

using JokerGho5t.MessageSystem;
using JokerGho5t.ScriptableObjects;

public class Blocks : MonoBehaviour
{
    #region Private Variables

    private float width = 13;
    private LevelData level;
    private Bounds field;

    #endregion

    #region Public Function

    public void Init(LevelData level, Bounds field)
    {
        this.field = field;
        this.level = level;

        Message.AddListener("RestartLevel", Restart);
        Message.AddListener("RemoveBrick", RemoveBrick);

        CreateLevel();
    }

    #endregion

    #region Private Function

    private void CreateLevel()
    {
        //-------------------------------------------
        // Create pool objects

        PoolManager.logStatus = false;
        PoolManager.SetRoot(transform);
        PoolManager.WarmPool(level.PrefabBlock, level.CountLine * level.CountLine);

        var sizeBrick = level.PrefabBlock.GetComponent<Renderer>().bounds.size;

        float shagY = sizeBrick.y;
        float shagX = sizeBrick.x;

        Vector2 curPosition = new Vector2(field.min.x + (shagX / 2), field.max.y - level.SpacingOnUp);

        //-------------------------------------------
        for (int y = 0; y < level.CountLine; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var brick = PoolManager.SpawnObject(level.PrefabBlock).GetComponent<Brick>();
                
                brick.Init(level.Blockdata[y]);

                brick.transform.position = curPosition;

                curPosition += Vector2.right * shagX;
            }

            curPosition = new Vector2(field.min.x + (shagX / 2), curPosition.y - shagY);
        }
    }

    private void Restart()
    {
        int count = PoolManager.GetCountObjectInPool(level.PrefabBlock);

        for (int i = 0; i < count; i++)
        {
            PoolManager.SpawnObjectOriginalTransform(level.PrefabBlock);
        }
    }

    private void RemoveBrick()
    {
        if(PoolManager.GetCountObjectInPool(level.PrefabBlock) == level.CountLine * width)
        {
            Message.Send("WinLevel");
        }
    }

    #endregion
}
