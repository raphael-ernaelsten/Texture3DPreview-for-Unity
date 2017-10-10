using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extensions for Texture3D object so we can just invoke functions on them
/// </summary>
public static class Texture3DExtensions
{
    #region Members
    /// <summary>
    /// The material used to render the Texture3D
    /// </summary>
    private static Material _previewTexture3dMaterial;
    #endregion

    #region Functions
    /// <summary>
    /// Accessor to the material used to render the Texture3D
    /// </summary>
    private static Material PreviewTexture3dMaterial
    {
        get
        {
            if (_previewTexture3dMaterial == null)
            {
                _previewTexture3dMaterial = new Material(Shader.Find("Hidden/Texture3DPreview"));
            }

            return _previewTexture3dMaterial;
        }
    }

    /// <summary>
    /// Sets the parameters to the PreviewRenderUtility and calls the rendering
    /// </summary>
    /// <param name="texture3D">The Texture3D to preview</param>
    /// <param name="angle">The camera angle</param>
    /// <param name="distance">The distance of the camera to the preview cube</param>
    /// <param name="samplingIterations">The amount of slices used to raymarch in the Texture3D</param>
    /// <param name="density">A linear factor to multiply the Texture3D with</param>
    private static void RenderInPreviewRenderUtility(Texture3D texture3D, Vector2 angle, float distance, int samplingIterations, float density)
    {
        PreviewTexture3dMaterial.SetInt("_SamplingQuality", samplingIterations);
        PreviewTexture3dMaterial.SetTexture("_MainTex", texture3D);
        PreviewTexture3dMaterial.SetFloat("_Density", density);

        PreviewRenderUtilityHelpers.Instance.DrawMesh(MeshHelpers.Cube, Matrix4x4.identity, PreviewTexture3dMaterial, 0);

        PreviewRenderUtilityHelpers.Instance.camera.transform.position = Vector2.zero;
        PreviewRenderUtilityHelpers.Instance.camera.transform.rotation = Quaternion.Euler(new Vector3(-angle.y, -angle.x, 0));
        PreviewRenderUtilityHelpers.Instance.camera.transform.position = PreviewRenderUtilityHelpers.Instance.camera.transform.forward * -distance;
        PreviewRenderUtilityHelpers.Instance.camera.Render();
    }

    /// <summary>
    /// Renders a preview of the Texture3D
    /// </summary>
    /// <param name="texture3D">The Texture3D to preview</param>
    /// <param name="rect">The area where the preview is located</param>
    /// <param name="backgroundStyle">The GUIStyle used for preview windows</param>
    /// <param name="angle">The camera angle</param>
    /// <param name="distance">The distance of the camera to the preview cube</param>
    /// <param name="samplingIterations">The amount of slices used to raymarch in the Texture3D</param>
    /// <param name="density">A linear factor to multiply the Texture3D with</param>
    /// <returns>A Texture with the preview</returns>
    public static Texture RenderTexture3DPreview(this Texture3D texture3D, Rect rect, GUIStyle backgroundStyle, Vector2 angle, float distance, int samplingIterations, float density)
    {
        PreviewRenderUtilityHelpers.Instance.BeginPreview(rect, backgroundStyle);

        RenderInPreviewRenderUtility(texture3D, angle, distance, samplingIterations, density);

        return PreviewRenderUtilityHelpers.Instance.EndPreview();
    }

    /// <summary>
    /// Renders a thumbnail of the Texture3D
    /// </summary>
    /// <param name="texture3D">The Texture3D to preview</param>
    /// <param name="rect">The area where the preview is located</param>
    /// <param name="angle">The camera angle</param>
    /// <param name="distance">The distance of the camera to the preview cube</param>
    /// <param name="samplingIterations">The amount of slices used to raymarch in the Texture3D</param>
    /// <param name="density">A linear factor to multiply the Texture3D with</param>
    /// <returns>A Texture2D with the thumbnail</returns>
    public static Texture2D RenderTexture3DStaticPreview(this Texture3D texture3D, Rect rect, Vector2 angle, float distance, int samplingIterations, float density)
    {
        PreviewRenderUtilityHelpers.Instance.BeginStaticPreview(rect);
        
        RenderInPreviewRenderUtility(texture3D, angle, distance, samplingIterations, density);

        return PreviewRenderUtilityHelpers.Instance.EndStaticPreview();
    }
    #endregion
}
