using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class MovePoint : MonoBehaviour
{
    [SerializeField] private GameObject increaseScorePrefab;
    [SerializeField] private Transform pointPos;

    public GameData gameData;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore,StartPointMove);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore,StartPointMove);
    }

    private void StartPointMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseScore.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }
}
