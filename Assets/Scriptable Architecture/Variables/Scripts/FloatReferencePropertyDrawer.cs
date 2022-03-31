using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferencePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect labelRect = position;
        labelRect.width /= 3;
        EditorGUI.LabelField(labelRect, property.displayName);

        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");


        Rect useConstantRect = labelRect;
        useConstantRect.x += useConstantRect.width;
        useConstant.boolValue = EditorGUI.Toggle(useConstantRect, useConstant.boolValue);


        Rect valueRect = useConstantRect;
        valueRect.x += valueRect.width;

        if(useConstant.boolValue)
        {
            SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
            constantValue.floatValue = EditorGUI.FloatField(valueRect, constantValue.floatValue);
        }
        else
        {
            SerializedProperty floatVariable = property.FindPropertyRelative("variable");
            floatVariable.objectReferenceValue = EditorGUI.ObjectField(valueRect, floatVariable.objectReferenceValue, typeof(FloatVariable));
        }
    }

    
}
#endif