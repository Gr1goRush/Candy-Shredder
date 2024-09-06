using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInputManager<T> : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<T> InputEventHandler;
}
