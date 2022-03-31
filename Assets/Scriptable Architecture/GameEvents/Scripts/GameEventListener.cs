using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private MyUnityEvent response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void OnEventRaised(EventArgs args = null)
    {
        response.Invoke(args);
    }
}

[System.Serializable]
public class MyUnityEvent : UnityEvent<EventArgs>
{

}
