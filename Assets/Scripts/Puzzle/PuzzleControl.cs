using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    public DirectionBox directionPrefab;

    public DirectionBox[,] directions=new DirectionBox[4,4];

    public Sprite[] sprites;

    private void Start() 
    {
        Init();
    }

    void Init()
    {
        int n=0;
        for(int y=3; y>=0; y--)
        {
            for(int x=0; x<4; x++)
            {   
                DirectionBox directionBox=Instantiate(directionPrefab,new Vector2(x,y),Quaternion.identity);
                directionBox.Init(x,y,n+1,sprites[n],ClickToSwap);
                directions[x,y]=directionBox;
                n++;
            }
        }
    }

    private void ClickToSwap(int x,int y)
    {
        int dx=getDx(x,y);
        int dy=getDy(x,y);

        var from=directions[x,y];
        var target=directions[x+dx,y+dy];

        //swap This 2 Directions
        directions[x,y]=target;
        directions[x+dx,y+dy]=from;

        //update position of 2 Directions
        from.UpdatePos(x+dx,y+dy);
        target.UpdatePos(x,y);
        
    }

    private int getDx(int x,int y)
    {
        //is Right Empty
        if(x<3 && directions[x+1,y].IsEmpty())
            return 1;
        //is Left Empty
        if(x>0 && directions[x-1,y].IsEmpty())
            return -1;

        return 0;
    }

    private int getDy(int x,int y)
    {   
        //is Top Empty
        if(y<3 && directions[x,y+1].IsEmpty())
            return 1;
        if(y>0 && directions[x,y-1].IsEmpty())
            return -1;

        return 0;

    }
}
