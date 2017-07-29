using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> BasicObjects;
    public List<GameObject> ComplexObjects;
    private List<GameObject> CurrentList;

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

    int BasicIndex;
    int ComplexIndex;
    int currentIndex;

    bool isBasic;
    public MyGameManager myGameManager;

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
            BasicMiddleText.text = BasicMiddleImage.sprite.name;
            if (currentIndex - 1 < 0)
                BasicLeftImage.sprite = BasicObjectImages[BasicObjectImages.Count - 1];
            else
                BasicLeftImage.sprite = BasicObjectImages[currentIndex - 1];
            BasicLeftText.text = BasicLeftImage.sprite.name;

            if (currentIndex + 1 > BasicObjectImages.Count - 1)
                BasicRightImage.sprite = BasicObjectImages[0];
            else
                BasicLeftImage.sprite = BasicObjectImages[currentIndex + 1];
            BasicRightText.text = BasicRightImage.sprite.name;
        }
        else
        {
            ComplexMiddleImage.sprite = ComplexObjectImages[currentIndex];
            ComplexMiddleText.text = ComplexMiddleImage.sprite.name;
            if (currentIndex - 1 < 0)
                ComplexLeftImage.sprite = ComplexObjectImages[ComplexObjectImages.Count - 1];
            else
                ComplexLeftImage.sprite = ComplexObjectImages[currentIndex - 1];
            ComplexLeftText.text = ComplexLeftImage.sprite.name;

            if (currentIndex + 1 > ComplexObjectImages.Count - 1)
                ComplexRightImage.sprite = ComplexObjectImages[0];
            else
                ComplexLeftImage.sprite = ComplexObjectImages[currentIndex + 1];
            ComplexRightText.text = ComplexRightImage.sprite.name;
        }
    }

    public void SpawnObject()
    {
        if (myGameManager.SpawnNum > 0)
        {
            GameObject newObject;
            newObject = Instantiate(CurrentList[currentIndex], CurrentList[currentIndex].transform.position, CurrentList[currentIndex].transform.rotation);
            newObject.tag = "Structure";

            Collider[] newObjectColliders = newObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in newObjectColliders)
            {
                collider.enabled = true;
            }
            Rigidbody[] newObjectRigidbodys = newObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigid in newObjectRigidbodys)
            {
                rigid.isKinematic = false;
            }
            myGameManager.SpawnNum--;
            SpawnNum.text = "SpawnNum : " + myGameManager.SpawnNum;
        }
    }
}
