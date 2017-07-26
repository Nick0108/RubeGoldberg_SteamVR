using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> BasicObjects;
    public List<GameObject> ComplexObjects;
    public List<GameObject> CurrentList;

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

    int BasicIndex;
    int ComplexIndex;
    int currentIndex;

    bool isBasic;

    private void Start()
    {
        CurrentList = BasicObjects;
        isBasic = true;
        BasicIndex = 0;
        ComplexIndex = 0;
        currentIndex = BasicIndex;

        foreach(GameObject basicObject in BasicObjects)
        {
            basicObject.SetActive(false);
        }
        foreach (GameObject ComplexObject in ComplexObjects)
        {
            ComplexObject.SetActive(false);
        }
    }

    public void SwipeRight()
    {
        CurrentList[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex > CurrentList.Count - 1)
        {
            currentIndex = 0;
        }
        CurrentList[currentIndex].SetActive(true);
        CheckCurrentCanvas();
    }
    public void SwipeLeft()
    {
        CurrentList[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex > CurrentList.Count - 1)
        {
            currentIndex = CurrentList.Count - 1;
        }
        CurrentList[currentIndex].SetActive(true);
        CheckCurrentCanvas();
    }
    public void SwipeUp()
    {
        if (!isBasic)
        {
            ComplexIndex = currentIndex;
            CurrentList = BasicObjects;
            currentIndex = BasicIndex;
            isBasic = true;
            SelectedBorder.transform.localPosition.Set(0, 0.4f, 0);
        }
    }
    public void SwipeDown()
    {
        if (isBasic)
        {
            BasicIndex = currentIndex;
            CurrentList = ComplexObjects;
            currentIndex = ComplexIndex;
            isBasic = false;
            SelectedBorder.transform.localPosition.Set(0, -0.47f, 0);
        }
        
    }
    public void Appear()
    {
        gameObject.SetActive(true);
        CurrentList[currentIndex].SetActive(true);
    }
    public void Disappear()
    {
        gameObject.SetActive(false);
        CurrentList[currentIndex].SetActive(false);
    }

    public void CheckCurrentCanvas()
    {
        if (isBasic)
        {
            BasicMiddleImage.sprite = BasicObjectImages[currentIndex];
            if (currentIndex - 1 < 0)
                BasicLeftImage.sprite = BasicObjectImages[BasicObjectImages.Count - 1];
            else
                BasicLeftImage.sprite = BasicObjectImages[currentIndex - 1];
            if (currentIndex + 1 > BasicObjectImages.Count - 1)
                BasicRightImage.sprite = BasicObjectImages[0];
            else
                BasicLeftImage.sprite = BasicObjectImages[currentIndex + 1];
        }
        else
        {
            ComplexMiddleImage.sprite = ComplexObjectImages[currentIndex];
            if (currentIndex - 1 < 0)
                ComplexLeftImage.sprite = ComplexObjectImages[ComplexObjectImages.Count - 1];
            else
                ComplexLeftImage.sprite = ComplexObjectImages[currentIndex - 1];
            if (currentIndex + 1 > ComplexObjectImages.Count - 1)
                ComplexRightImage.sprite = ComplexObjectImages[0];
            else
                ComplexLeftImage.sprite = ComplexObjectImages[currentIndex + 1];
        }
    }
}
