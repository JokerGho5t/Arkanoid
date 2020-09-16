using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace JokerGho5t.ScriptableObjects
{
    public class LevelData : SerializedScriptableObject
    {
        public Vector2Int SizeField => _sizeField;
        [SerializeField]
        [OnValueChanged("ChangeField")]
        private Vector2Int _sizeField = new Vector2Int(1, 1);

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
            if (_sizeField.x < 1)
                _sizeField.x = 1;
            if (_sizeField.y < 1)
                _sizeField.y = 1;

            _blockdata.Clear();

            for (int i = 0; i < _sizeField.y; i++)
            {
                _blockdata.Add(new BlockComponent(null, 1));
            }
        }
    }
}
