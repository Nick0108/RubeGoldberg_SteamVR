using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour {

    public List<GameObject> Stars;
    public int currentStarNum;
    public int StarsNum;
    public GameObject gameWinCanvas;
    public int SpawnNum;

    private void Start()
    {
        currentStarNum = 0;
        gameWinCanvas.SetActive(false);
    }

    public void AddScore(Collision star)
    {
        Stars.Add(star.gameObject);
        star.gameObject.SetActive(false);
        currentStarNum++;
    }

    public void ScoreReset()
    {
        foreach(GameObject star in Stars)
        {
            star.SetActive(true);
        }
        Stars = null;
        currentStarNum = 0;
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
        SteamVR_LoadLevel.Begin("Level_2");
    }
    IEnumerator WaitS()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
