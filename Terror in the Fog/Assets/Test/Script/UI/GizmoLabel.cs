using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoLabel : MonoBehaviour
{ 
    public string label;

    [SerializeField]
    private Color gizmoColor = Color.white;

    private void OnDrawGizmos()
    {
        if (!string.IsNullOrEmpty(label))
        {
            Gizmos.color = gizmoColor;
            DrawLabel();
        }
    }

    private void DrawLabel()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = gizmoColor;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;

        Vector3 position = transform.position + Vector3.up * 1.5f;
        //UnityEditor.Handles.Label(position, label, style);
    }
    
}
