using UnityEngine;

/// <summary>
/// Attribute enabling Texture3D preview in a Monobehaviour component in the Inspector
/// </summary>
public class Texture3DPreviewAttribute : PropertyAttribute
{
    #region Members
    /// <summary>
    /// Allows to show the field (when user drags/drops another Texture3D for example) or not (when Texture3D is changed in code only for example)
    /// </summary>
    public readonly bool showField;
    #endregion

    #region Constructor
    /// <summary>
    /// Declare this attribute in front of the Texture3D field to enable previewing in a Monobehaviour component in the Inspector
    /// </summary>
    /// <param name="showField">Shows or not the Texture3D field on top of the preview (default = true)</param>
    public Texture3DPreviewAttribute(bool showField = true)
    {
        this.showField = showField;
    }
    #endregion
}