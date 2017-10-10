using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Collection of function/variables related to the PreviewRenderUtility class
/// </summary>
public static class PreviewRenderUtilityHelpers
{
    #region Members
    /// <summary>
    /// A static copy of the PreviewRenderUtility class
    /// </summary>
    private static PreviewRenderUtility _instance;
    #endregion

    #region Functions
    /// <summary>
    /// Accessor to the PreviewRenderUtility instance
    /// </summary>
    public static PreviewRenderUtility Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PreviewRenderUtility();
            }

            return _instance;
        }
    }

    /// <summary>
    /// Transforms the drag delta of the mouse on the UI into Euler angles
    /// </summary>
    /// <param name="angles">Input angles</param>
    /// <param name="position">The area where the mouse will be watched</param>
    /// <returns>The modified angles</returns>
    public static Vector2 DragToAngles(Vector2 angles, Rect position)
    {
        int controlID = GUIUtility.GetControlID("DragToAngles".GetHashCode(), FocusType.Passive);
        Event current = Event.current;
        switch (current.GetTypeForControl(controlID))
        {
            case EventType.MouseDown:
                if (position.Contains(current.mousePosition))
                {
                    GUIUtility.hotControl = controlID;
                    current.Use();
                    EditorGUIUtility.SetWantsMouseJumping(1);
                }
                break;
            case EventType.MouseUp:
                if (GUIUtility.hotControl == controlID)
                {
                    GUIUtility.hotControl = 0;
                }
                EditorGUIUtility.SetWantsMouseJumping(0);
                break;
            case EventType.MouseDrag:
                if (GUIUtility.hotControl == controlID)
                {
                    angles -= current.delta / Mathf.Min(position.width, position.height) * 180;
                    current.Use();
                    GUI.changed = true;
                }
                break;
        }
        return angles;
    }
    #endregion

}
