using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/Events/GameEvent", order = 0)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void RaiseEvent(EventArgs args = null)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(args);
    }

    public void Register(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void Unregister(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
