using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MyExtensionComponent 
{
    
    public static void SaveToPNG(this RenderTexture rTex, string path)
    {
        _getWritableTex(rTex, (tex) =>
            {
                byte[] bytes = tex.EncodeToPNG();
                System.IO.File.WriteAllBytes(path, bytes);
            }
        );
    }

    public static void SaveToJPG(this RenderTexture rTex, string path)
    {
        _getWritableTex(rTex, (tex) =>
        {
            byte[] bytes = tex.EncodeToJPG();
            System.IO.File.WriteAllBytes(path, bytes);
        }
        );
    }

    public static void SaveToEXR(this RenderTexture rTex, string path)
    {
        _getWritableTex(rTex, (tex) =>
        {
            byte[] bytes = tex.EncodeToEXR();
            System.IO.File.WriteAllBytes(path, bytes);
        }
        );
    }

    public static void SaveToTGA(this RenderTexture rTex, string path)
    {
        _getWritableTex(rTex, (tex) =>
        {
            byte[] bytes = tex.EncodeToTGA();
            System.IO.File.WriteAllBytes(path, bytes);
        }
        );
    }

    private static void _getWritableTex(RenderTexture rTex, Action<Texture2D> call)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        call(tex);
        Texture2D.DestroyImmediate(tex);
    }
}
