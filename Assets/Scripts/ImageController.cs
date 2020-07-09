using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    #region Variables
    [SerializeField] int ID;

    SimonGameManager gameManager;
    Color32 basicColor;
    Color32 highlightColor;
    bool disableClicks = false;
    #endregion

    void Start()
    {
        gameManager = FindObjectOfType<SimonGameManager>();
        basicColor = GetComponent<Image>().color;
        highlightColor = basicColor;
        basicColor.a = 150;
        ChangeColorToNormal();
    }

    public void PressedImage()
    {
        if (disableClicks)
            return;

        gameManager.PressedColor(ID);
    }

    public void ChangeColorToHighlight()
    {
        GetComponent<Image>().color = highlightColor;
    }
    public void ChangeColorToNormal()
    {
        GetComponent<Image>().color = basicColor;
    }
    public void ChangeImageStatus()
    {
        if (disableClicks)
            disableClicks = false;
        else
            disableClicks = true;
    }
}
