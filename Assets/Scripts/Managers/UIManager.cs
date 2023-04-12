using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score,highscore,endingScore;
    public GameData gameData;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUpdateUI, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateGameOverUI,OnUIGameOver);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateGameOverUI,OnUIGameOver);
    }

    
    void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
        //coin.SetText("x" + gameData.coins.ToString());
    }

    void OnUIGameOver()
    {
        score.SetText("");
        endingScore.SetText("Score : " + gameData.score.ToString());
        highscore.SetText("High Score : " + gameData.highScore.ToString());
    }
}
