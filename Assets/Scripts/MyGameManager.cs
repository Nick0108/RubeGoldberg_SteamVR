using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour {

    public List<GameObject> Stars;
    public int currentStarNum;
    public int StarsNum;
    public GameObject gameWinCanvas;
    public int SpawnNum;
    public SteamVR_LoadLevel loadLevel;

    private GameObject currentStar;
    private int currentStarIndex;

    private void Start()
    {
        StarsNum = Stars.Count;
        currentStarNum = 0;
        currentStarIndex = 0;
        gameWinCanvas.SetActive(false);
        if (loadLevel == null)
            loadLevel = GetComponent<SteamVR_LoadLevel>();
    }

    public void AddScore(Transform starTrans)
    {
        for(int i =0;i< StarsNum; i++)
        {
            currentStar = Stars[i];
            if (currentStar.transform == starTrans)
            {
                currentStarIndex = i;
            }
        }
        Stars[currentStarIndex].SetActive(false);
        currentStarNum++;
        //Debug.Log("currentStarNum"+currentStarNum+"==="+ "currentStarIndex" + currentStarIndex);
    }

    public void ScoreReset()
    {
        foreach(GameObject star in Stars)
        {
            star.SetActive(true);
        }
        currentStarNum = 0;
        //Debug.Log("score has reset"+currentStarNum);
    }
    public bool CheckWin()
    {
        if (currentStarNum == StarsNum)
            return true;
        else
            return false;
    }
    public void GameWin()
    {
        gameWinCanvas.SetActive(true);
        StartCoroutine(WaitS());
        loadLevel.Trigger();
    }
    IEnumerator WaitS()
    {
        yield return new WaitForSeconds(2.0f);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            loadLevel.Trigger();
        }
    }*/
}
