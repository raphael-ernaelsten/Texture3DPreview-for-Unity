using UnityEditor;
using UnityEngine;

/// <summary>
/// Draws a custom Inspector for Texture3D assets
/// Actually, we will draw the default inspector but use the ability to draw a custom preview and render a custom asset's thumbnail
/// </summary>
[CustomEditor(typeof(Texture3D))]
public class Texture3DEditor : Editor
{
    #region Members
    /// <summary>
    /// The angle of the camera preview
    /// </summary>
    private Vector2 _cameraAngle = new Vector2(127.5f, -22.5f); // This default value will be used when rendering the asset thumbnail (see RenderStaticPreview)
    /// <summary>
    /// The raymarch interations
    /// </summary>
    private int _samplingIterations = 64;
    /// <summary>
    /// The factor of the Texture3D
    /// </summary>
    private float _density = 1;

    //// TODO : Investigate to access those variables as the default inspector is ugly
    //private SerializedProperty wrapModeProperty;
    //private SerializedProperty filterModeProperty;
    //private SerializedProperty anisotropyLevelProperty;
    #endregion

    #region Functions
    /// <summary>
    /// Sets back the camera angle
    /// </summary>
    public void ResetPreviewCameraAngle()
    {
        _cameraAngle = new Vector2(127.5f, -22.5f);
    }
    #endregion

    #region Overriden base class functions (https://docs.unity3d.com/ScriptReference/Editor.html)
    /// <summary>
    /// Draws the content of the Inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //// Had to disable the default Inspector as it makes preview lag
        //DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    #region Preview
    /// <summary>
    /// Tells if the Object has a custom preview
    /// </summary>
    public override bool HasPreviewGUI()
    {
        return true;
    }

    /// <summary>
    /// Draws the toolbar area on top of the preview window
    /// </summary>
    public override void OnPreviewSettings()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset Camera", EditorStyles.miniButton))
        {
            ResetPreviewCameraAngle();
        }
        EditorGUILayout.LabelField("Quality", GUILayout.MaxWidth(50));
        _samplingIterations = EditorGUILayout.IntPopup(_samplingIterations, new string[] { "16", "32", "64", "128", "256", "512" }, new int[] { 16, 32, 64, 128, 256, 512 }, GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField("Density", GUILayout.MaxWidth(50));
        _density = EditorGUILayout.Slider(_density, 0, 5, GUILayout.MaxWidth(200));
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Draws the preview area
    /// </summary>
    /// <param name="rect">The area of the preview window</param>
    /// <param name="backgroundStyle">The default GUIStyle used for preview windows</param>
    public override void OnPreviewGUI(Rect rect, GUIStyle backgroundStyle)
    {
        _cameraAngle = PreviewRenderUtilityHelpers.DragToAngles(_cameraAngle, rect);

        if (Event.current.type == EventType.Repaint)
        {
            GUI.DrawTexture(rect, ((Texture3D)serializedObject.targetObject).RenderTexture3DPreview(rect, EditorStyles.helpBox, _cameraAngle, 6.5f /*TODO : Find distance with fov and boundingsphere, when non uniform size will be supported*/, _samplingIterations, _density), ScaleMode.StretchToFill, true);
        }
    }

    /// <summary>
    /// Draws the custom preview thumbnail for the asset in the Project window
    /// </summary>
    /// <param name="assetPath">Path of the asset</param>
    /// <param name="subAssets">Array of children assets</param>
    /// <param name="width">Width of the rendered thumbnail</param>
    /// <param name="height">Height of the rendered thumbnail</param>
    /// <returns></returns>
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        return ((Texture3D)serializedObject.targetObject).RenderTexture3DStaticPreview(new Rect(0, 0, width, height), _cameraAngle, 6.5f /*TODO : Find distance with fov and boundingsphere, when non uniform size will be supported*/, _samplingIterations, _density);
    }

    /// <summary>
    /// Allows to give a custom title to the preview window
    /// </summary>
    /// <returns></returns>
    public override GUIContent GetPreviewTitle()
    {
        return new GUIContent(serializedObject.targetObject.name + " preview");
    }
    #endregion
    #endregion
}
