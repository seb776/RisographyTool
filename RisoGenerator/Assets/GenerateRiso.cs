using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Color codes
// Red FF665E
// Blue 0078BF
// Purple 765ba7
// Orange ff6c2f
// Green #00A95C

public class GenerateRiso : MonoBehaviour
{
    public Texture InputImage;
    public Color ColorA;
    public Color ColorB;

    public bool SaveColorA;
    public bool SaveColorB;
    public bool SaveColorBlack;


    public Material MaterialColorA;
    public Material MaterialColorB;
    public Material MaterialColorBlack;

    public Material ShowA;
    public Material ShowB;
    public Material ShowBlack;


    public Material PreviewMaterial;

    private RenderTexture RTColorA;
    private RenderTexture RTColorB;
    private RenderTexture RTColorBlack;
    void Start()
    {
        Debug.Log(InputImage.width);
        RTColorA = new RenderTexture(InputImage.width, InputImage.height, 24);
        RTColorB = new RenderTexture(InputImage.width, InputImage.height, 24);
        RTColorBlack = new RenderTexture(InputImage.width, InputImage.height, 24);

        PreviewMaterial.SetTexture("_ColorATex", RTColorA);
        PreviewMaterial.SetTexture("_ColorBTex", RTColorB);
        PreviewMaterial.SetTexture("_ColorBlackTex", RTColorBlack);

        ShowA.SetTexture("_BaseMap", RTColorA);
        ShowB.SetTexture("_BaseMap", RTColorB);
        ShowBlack.SetTexture("_BaseMap", RTColorBlack);
    }

    void Update()
    {
        PreviewMaterial.SetColor("_ColorA", ColorA);
        PreviewMaterial.SetColor("_ColorB", ColorB);
        Graphics.Blit(InputImage, RTColorA, MaterialColorA);
        Graphics.Blit(InputImage, RTColorB, MaterialColorB);
        Graphics.Blit(InputImage, RTColorBlack, MaterialColorBlack);

        if (SaveColorA)
        {
            RTColorA.SaveToPNG("ColorA.png");
            SaveColorA = false;
        }
        if (SaveColorB)
        {
            RTColorB.SaveToPNG("ColorB.png");
            SaveColorB = false;
        }
        if (SaveColorBlack)
        {
            RTColorBlack.SaveToPNG("ColorBlack.png");
            SaveColorBlack = false;
        }
    }
}
