using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> BasicObjects;
    public List<GameObject> ComplexObjects;
    private List<GameObject> CurrentList;
    public UIObjectMenuManager ObjectsUI;

    public int BasicIndex;
    public int ComplexIndex;
    public int currentIndex;

    public bool isBasic;
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

    public void SwipeLeft()
    {
        //Debug.Log("SwipeRight");
        CurrentList[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex > CurrentList.Count - 1)
        {
            currentIndex = 0;
        }
        CurrentList[currentIndex].SetActive(true);
        ObjectsUI.CheckCurrentCanvas(isBasic,currentIndex);
    }
    public void SwipeRight()
    {
        CurrentList[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = CurrentList.Count - 1;
        }
        CurrentList[currentIndex].SetActive(true);
        ObjectsUI.CheckCurrentCanvas(isBasic,currentIndex);
    }
    public void SwipeUp()
    {
        //Debug.Log("SwipeUp");
        if (!isBasic)
        {
            //Debug.Log("SwipeUp--2");
            CurrentList[currentIndex].SetActive(false);
            ComplexIndex = currentIndex;
            CurrentList = BasicObjects;
            currentIndex = BasicIndex;
            CurrentList[currentIndex].SetActive(true);
            isBasic = true;
            ObjectsUI.UpSelectedBorder();
        }
    }
    public void SwipeDown()
    {
        //Debug.Log("SwipeDown");
        if (isBasic)
        {
            //Debug.Log("SwipeDown--2");
            CurrentList[currentIndex].SetActive(false);
            BasicIndex = currentIndex;
            CurrentList = ComplexObjects;
            currentIndex = ComplexIndex;
            CurrentList[currentIndex].SetActive(true);
            isBasic = false;
            ObjectsUI.DownSelectedBorder();
        }
        
    }
    public void Appear()
    {
        CurrentList[currentIndex].SetActive(true);
    }
    public void Disappear()
    {
        CurrentList[currentIndex].SetActive(false);
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
                rigid.isKinematic = true;
            }
            newObject.transform.SetParent(null);
            myGameManager.SpawnNum--;
            ObjectsUI.UpdateSpawnNumUI(myGameManager.SpawnNum);
        }
    }
}
