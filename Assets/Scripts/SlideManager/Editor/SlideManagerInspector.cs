using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SlideManager))]
public class SlideManagerInspector : Editor {
    ReorderableList reoderableList;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("pages");

        reoderableList = new ReorderableList(serializedObject, prop, true, true, true, true);

        reoderableList.drawElementCallback = (rect, index, isActive, isForcused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };

        reoderableList.onAddCallback += (list)=>
        {
            prop.arraySize++;

            list.index = prop.arraySize - 1;

            prop.GetArrayElementAtIndex(list.index);
        };
    }


    void Remove(ReorderableList list)
    {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reoderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
