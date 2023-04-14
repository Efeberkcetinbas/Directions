using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DirectionBox : MonoBehaviour
{
    public int index=0;
    int x=0;
    int y=0;

    private Action<int,int> swapFunc=null;

    public bool isLeft,isRight,isUp,isDown;

    public bool canPass=true;

    public void Init(int i,int j,int index,Sprite sprite,Action<int,int> swapFunc)
    {
        this.index=index;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite=sprite;
        UpdatePos(i,j);
        this.swapFunc=swapFunc;

    }

    public void UpdatePos(int i, int j)
    {
        x=i;
        y=j;
        this.gameObject.transform.localPosition=new Vector2(i,j);
    }

    public bool IsEmpty(int _index)
    {
        return index==_index;
    }

    private void OnMouseDown() 
    {
        if(Input.GetMouseButtonDown(0) && swapFunc!=null)
        {
            swapFunc(x,y);
            EventManager.Broadcast(GameEvent.OnMove);
            transform.DOScale(Vector3.one/2,0.2f).OnComplete(()=>transform.DOScale(new Vector3(0.9f,0.9f,0.9f),0.2f));
        }

        
    }

}
