using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    [Header("Success and Fail Ending")]
    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject[] gameObjects;

    private void Start() 
    {
        Reset();
    }

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnSuccess,OpenSuccessMenu);
        EventManager.AddHandler(GameEvent.OnNextLevel,Reset);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.AddHandler(GameEvent.OnGameOver,OpenFailMenu);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnSuccess,OpenSuccessMenu);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,Reset);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnGameOver,OpenFailMenu);
    }

    //Next Levele Gectigimizde
    private void OnNextLevel()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }
    private void OpenSuccessMenu()
    {
        OpenClose(false);
        successPanel.SetActive(true);
        gameData.isGameEnd=true;
        successPanel.transform.DOScale(Vector2.one*1.5f,0.5f).OnComplete(()=> {
            successPanel.transform.DOScale(Vector2.one*1.2f,0.5f);
        });
    }

    private void OpenFailMenu()
    {
        OpenClose(false);
        failPanel.SetActive(true);
        gameData.isGameEnd=true;
        failPanel.transform.DOScale(Vector2.one*1.5f,0.5f).OnComplete(()=> {
            failPanel.transform.DOScale(Vector2.one*1.2f,0.5f);
        });
    }

    private void Reset()
    {
        gameData.RequiredBox=0;
        gameData.score=0;
        gameData.RemainingTime=0;
        gameData.timerIsRunning=true;
        OpenClose(true);
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(1);
        gameData.isGameEnd=false;
    } 

    //Bu method ile istedigimiz objeleri gameEndingte kapatip,resette acabiliriz.
    private void OpenClose(bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    #region Score Management
    void OnIncreaseScore()
    {
        //gameData.score += 50;
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,1f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    #endregion

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }

}
