using System;
using UnityEngine;
using UnityEngine.Events;

namespace JokerGho5t.CustomEvents
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    [Serializable]
    public class HitEvent : UnityEvent<Vector3, Quaternion, float> { }

    [Serializable]
    public class isTriggerEvent : UnityEvent<Collider, GameObject> { }

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }
}
