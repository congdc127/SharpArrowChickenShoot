using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    GameEvent gameEvent;

    private void OnEnable()
    {
        gameEvent = (GameEvent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();

        GUILayout.Label("Raise Event");
        if (GUILayout.Button("Raise"))
            gameEvent.RaiseEvent();

        GUILayout.EndHorizontal();

    }
}
#endif