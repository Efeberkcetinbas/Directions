using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleMovement : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    private bool isBeginToMove=false;

    public GameData gameData;

    
    private void OnMouseDown() 
    {
        if(!isBeginToMove)
        {
            transform.DOLocalMove(startPoint.position,1f);
            isBeginToMove=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Block"))
        {
            Debug.Log("TOUCH");
            StartCoroutine(Move(other.GetComponent<DirectionBox>()));

        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Block"))
        {
            DoExitAction(other.GetComponent<DirectionBox>());
        }
        
    }

    private IEnumerator Move(DirectionBox directionBox)
    {
        yield return new WaitForSeconds(1f);
        if(directionBox.isUp && directionBox.canPass) transform.DOLocalMoveY(transform.position.y+1,1f);
        if(directionBox.isDown && directionBox.canPass) transform.DOLocalMoveY(transform.position.y-1,1f);
        if(directionBox.isLeft && directionBox.canPass) transform.DOLocalMoveX(transform.position.x-1,1f);
        if(directionBox.isRight && directionBox.canPass) transform.DOLocalMoveX(transform.position.x+1,1f);

        if(!directionBox.canPass)
        {
            directionBox.GetComponent<SpriteRenderer>().color=Color.red;
            EventManager.Broadcast(GameEvent.OnGameOver);
        }
        else
        {
            gameData.RequiredBox++;
            EventManager.Broadcast(GameEvent.OnCheckFinish);
            //EventManager.Broadcast(GameEvent.OnCanPass);
        }
    } 

    void DoExitAction(DirectionBox directionBox)
    {
        directionBox.GetComponent<SpriteRenderer>().color=Color.green;
        EventManager.Broadcast(GameEvent.OnIncreaseScore);
        directionBox.canPass=false;
        //Bir daha buradan gecemezsin
    }
}
