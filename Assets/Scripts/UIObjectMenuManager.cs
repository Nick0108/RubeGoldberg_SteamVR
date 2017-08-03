using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectMenuManager : MonoBehaviour {

    public List<Sprite> BasicObjectImages;
    public List<Sprite> ComplexObjectImages;

    public Image SelectedBorder;

    public Image BasicMiddleImage;
    public Image BasicLeftImage;
    public Image BasicRightImage;

    public Image ComplexMiddleImage;
    public Image ComplexLeftImage;
    public Image ComplexRightImage;

    public Text BasicMiddleText;
    public Text BasicLeftText;
    public Text BasicRightText;

    public Text ComplexMiddleText;
    public Text ComplexLeftText;
    public Text ComplexRightText;

    public Text SpawnNum;

    Vector3 upSelectPos = new Vector3(0f, 0.4f, 0f);
    Vector3 downSelectPos = new Vector3(0f, -0.47f, 0f);

    public void UpSelectedBorder()
    {
        SelectedBorder.rectTransform.localPosition = upSelectPos;
    }

    public void DownSelectedBorder()
    {
        SelectedBorder.rectTransform.localPosition = downSelectPos;
    }

    public void UIAppear()
    {
        gameObject.SetActive(true);
    }
    public void UIDisappear()
    {
        gameObject.SetActive(false);
    }

    public void CheckCurrentCanvas(bool Basic,int currentNum)
    {
        Debug.Log("CheckCurrentCanvas");
        if (Basic)
        {
            BasicMiddleImage.sprite = BasicObjectImages[currentNum];
            BasicMiddleText.text = BasicMiddleImage.sprite.name;
            if (currentNum - 1 < 0)
                BasicLeftImage.sprite = BasicObjectImages[BasicObjectImages.Count - 1];
            else
                BasicLeftImage.sprite = BasicObjectImages[currentNum - 1];
            BasicLeftText.text = BasicLeftImage.sprite.name;

            if (currentNum + 1 > BasicObjectImages.Count - 1)
                BasicRightImage.sprite = BasicObjectImages[0];
            else
                BasicRightImage.sprite = BasicObjectImages[currentNum + 1];
            BasicRightText.text = BasicRightImage.sprite.name;
        }
        else
        {
            ComplexMiddleImage.sprite = ComplexObjectImages[currentNum];
            ComplexMiddleText.text = ComplexMiddleImage.sprite.name;
            if (currentNum - 1 < 0)
                ComplexLeftImage.sprite = ComplexObjectImages[ComplexObjectImages.Count - 1];
            else
                ComplexLeftImage.sprite = ComplexObjectImages[currentNum - 1];
            ComplexLeftText.text = ComplexLeftImage.sprite.name;

            if (currentNum + 1 > ComplexObjectImages.Count - 1)
                ComplexRightImage.sprite = ComplexObjectImages[0];
            else
                ComplexRightImage.sprite = ComplexObjectImages[currentNum + 1];
            ComplexRightText.text = ComplexRightImage.sprite.name;
        }
    }

    public void UpdateSpawnNumUI(int GameSpawnNum)
    {
        SpawnNum.text = "SpawnNum : " + GameSpawnNum;
    }
}
