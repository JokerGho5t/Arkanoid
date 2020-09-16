using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JokerGho5t.MessageSystem
{
    public class MessageListener : MonoBehaviour
    {
        #region Public Variables

        /// <summary> UnityEvent executed when this listener has been triggered </summary>
        public UnityEvent Event;

        /// <summary> Game event string value that will trigger this listener's callback </summary>
        public string MessageName;

        #endregion

        #region Unity Methods

        private void Reset()
        {
            MessageName = string.Empty;
            Event = new UnityEvent();
        }

        private void Awake() { MessageName = MessageName.Trim(); }

        private void OnEnable() { RegisterListener(); }

        private void OnDisable() { UnregisterListener(); }

        #endregion

        #region Private Methods

        private void RegisterListener()
        {
            Message.AddListener(MessageName, OnMessage);
        }

        private void UnregisterListener()
        {
            Message.RemoveListener(MessageName, OnMessage);
        }

        private void OnMessage()
        {
            if (Event == null) return;
            Event.Invoke();
        }

        #endregion
    }
}
