using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Enemy))]
public class Path : Editor
{
    private Enemy targetComponent;

    public void OnSceneGUI()
    {
        targetComponent = (Enemy)target;
        Handles.color = Color.cyan;
        var positions = targetComponent.chosenPath;

        for (int i = 1; i < positions.Length + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = positions[i % positions.Length];

            Handles.DrawLine(previousPoint, currentPoint, 4f);
        }

        var polygonPoints = new Vector3[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            polygonPoints[i] = positions[i];
            positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
        }
        Handles.color = new Color(0, 1, 1, 0.2f);
        Handles.DrawAAConvexPolygon(polygonPoints);

        if (GUI.changed && !Application.isPlaying)
        {
            EditorUtility.SetDirty(targetComponent);
            EditorSceneManager.MarkSceneDirty(targetComponent.gameObject.scene);
        }
    }
}
