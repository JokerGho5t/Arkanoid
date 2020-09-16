using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;


[System.Serializable]
public struct BlockComponent
{
    public BlockComponent(Sprite sprite, int life)
    {
        this.sprite = sprite;

        this.life = (life < 1) ? 1 : life;
    }

    [Min(1)]
    public int life;
    public Sprite sprite;
}

namespace JokerGho5t.ScriptableObjects
{
    public class LevelData : SerializedScriptableObject
    {
        public int CountLine => _countLine;
        [SerializeField, Min(1)]
        [OnValueChanged("ChangeField")]
        private int _countLine = 1;

        public float SpacingOnUp => _spacingOnUp;
        [SerializeField]
        [Min(0)]
        private float _spacingOnUp = 20;

        public GameObject PrefabBlock => _prefabBlock;
        [SerializeField]
        private GameObject _prefabBlock = null;

        public List<BlockComponent> Blockdata => _blockdata;
        [TitleGroup("Level Setting")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        [SerializeField]
        private List<BlockComponent> _blockdata = new List<BlockComponent>();

        private void ChangeField()
        {
            _blockdata.Clear();

            for (int i = 0; i < _countLine; i++)
            {
                _blockdata.Add(new BlockComponent(null, 1));
            }
        }
    }
}
