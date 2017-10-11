# **[Texture3D](https://docs.unity3d.com/ScriptReference/Texture3D.html) preview for [Unity](https://unity3d.com)**

## This package enables interactive previews of [Texture3D](https://docs.unity3d.com/ScriptReference/Texture3D.html) assets in Unity's Inspector window.

**[You can directly download the Unity package by clicking here or by going in the Release section](https://github.com/raphael-ernaelsten/Texture3DPreview-for-Unity/releases)**

----------

### Previews and thumbnails of [Texture3D](https://docs.unity3d.com/ScriptReference/Texture3D.html) asset

When importing the Texture3D asset, Unity will automatically render a preview of the Texture3D to display in the Project window.

![Texture3D asset thumbnail](https://i.imgur.com/K9IhLF3.jpg)

When selecting a Texture3D asset in the Project windows, Unity will display an preview of the Texture3D in the Inspector.

![Texture3D asset preview](https://i.imgur.com/Lm5Kykw.gif)

----------

### Previews of a [Texture3D](https://docs.unity3d.com/ScriptReference/Texture3D.html) field on a GameObject's component

To enable Texture3D field preview on a GameObject's component, add 

    [Texture3DPreview]
in front of the declared field.

![Texture3D field preview in Inspector](https://i.imgur.com/ru8u1qK.gif)

----------

### Requirements 

 - Shader model 3 capable graphic card
 - Unity 2017.1+

----------

### TODO 

 - Preview non uniform Texture3D in their respective ratio (currently all previews will be cube)
 - Add alpha blend mode for rendering previews

----------

### Know issues / limitations

 - Previews are currently in additive mode
 - It makes the interactive previews hard to see on the light grey free license skin of Unity
 - Previews of multiples Texture3D assets don't work
 - Sometimes, the preview of a Texture3D field becomes empty

----------

Feel free to contact me for any comment or suggestion. Twitter : @raphernaelsten
