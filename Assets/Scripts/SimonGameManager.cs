using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonGameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject playObjects;
    [SerializeField] GameObject loseObjects;
    [SerializeField] Text loseScoreText;
    [SerializeField] Text scoreText;
    [SerializeField] Image[] images;

    List<int> numberChain;
    bool waiterBool = false;
    int actChainIndex = 0;
    #endregion

    void Start()
    {
        numberChain = new List<int>();
        MakeRandomColors();
    }

    private void MakeRandomColors()
    {
        int randomNum = Random.Range(0, 3);
        Debug.Log("Random number is " + randomNum);
        numberChain.Add(randomNum);
        scoreText.text = "SCORE\n" + (numberChain.Count-1);

        StartCoroutine(PresentRandomColours());
    }
    private void FreezeControls()
    {
        //Freeze player input
        foreach(Image image in images)
        {
            image.GetComponent<ImageController>().ChangeImageStatus();
        }
    }

    public void PressedColor(int ID)
    {
        if (waiterBool)
            return;

        //Debugging
        switch (ID)
        {
            case 0: Debug.Log("Green");
                break;
            case 1:Debug.Log("Red");
                break;
            case 2:Debug.Log("Yellow");
                break;
            case 3: Debug.Log("Blue");
                break;
            default:
                break;
        }
        //--------

        StartCoroutine(ClickedColor(ID));
    }

    private void CheckUserInput(int num)
    {
        if(numberChain[actChainIndex] == num)
        {
            actChainIndex++;
        }
        else
        {
            YouLost();
            return;
        }

        if(actChainIndex >= numberChain.Count)
        {
            Debug.Log("NEXT ROUND");
            actChainIndex = 0;
            MakeRandomColors();
        }
    }

    private void YouLost()
    {
        Debug.Log("YOU LOST");
        FreezeControls();
        playObjects.SetActive(false);
        loseObjects.SetActive(true);
        loseScoreText.text = "SCORE\n" + (numberChain.Count-1);

    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator PresentRandomColours()
    {
        FreezeControls();
        //Time between player guess and new sequence
        yield return new WaitForSeconds(0.4f);
        foreach(int number in numberChain)
        {
            images[number].GetComponent<ImageController>().ChangeColorToHighlight();
            yield return new WaitForSeconds(0.7f);
            images[number].GetComponent<ImageController>().ChangeColorToNormal();
            yield return new WaitForSeconds(0.2f);
        }
        FreezeControls();
    }

    IEnumerator ClickedColor(int ID)
    {
        //Preventing animation errors
        waiterBool = true;
        images[ID].GetComponent<ImageController>().ChangeColorToHighlight();
        yield return new WaitForSeconds(0.3f);
        images[ID].GetComponent<ImageController>().ChangeColorToNormal();
        waiterBool = false;

        CheckUserInput(ID);
    }

}
