using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    public DirectionBox directionPrefab;

    public DirectionBox[,] directions=new DirectionBox[4,4];

    public Sprite[] sprites;

    private DirectionBox tempDirectionPrefab;

    [SerializeField] private int puzzleLength;

    private List<Color> colors=new List<Color>();

    [SerializeField] private int yValue,xValue,indexValue,minX,maxX;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnCheckFinish,OnCheckFinish);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnCheckFinish,OnCheckFinish);
    }

    void OnCheckFinish()
    {
        //Burada Check islemi olacak

    }
    private void Start() 
    {
        AddingListColor();
        Init();
    }

    void AddingListColor()
    {
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.yellow);
        colors.Add(Color.magenta);
    }

    

    void Init()
    {
        int n=0;
        for(int y=yValue; y>=0; y--)
        {
            for(int x=0; x<xValue; x++)
            {   
                DirectionBox directionBox=Instantiate(directionPrefab,new Vector2(x,y),Quaternion.identity);
                directionBox.Init(x,y,n+1,sprites[n],ClickToSwap);
                directions[x,y]=directionBox;
                tempDirectionPrefab=directionBox;
                n++;

                if(n<puzzleLength-1)
                {
                    //Daha duzgun sprite lar alinca getChildlari kaldirirsin hem buradan hem de directionBoxtan
                    if(tempDirectionPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name=="ArrowTop") tempDirectionPrefab.isUp=true;
                    if(tempDirectionPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name=="ArrowButtom") tempDirectionPrefab.isDown=true;
                    if(tempDirectionPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name=="ArrowLeft") tempDirectionPrefab.isLeft=true;
                    if(tempDirectionPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name=="ArrowRight") tempDirectionPrefab.isRight=true;
                }
                
                tempDirectionPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color=colors[Random.Range(0,colors.Count)];
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
        if(x<minX && directions[x+1,y].IsEmpty(indexValue))
            return 1;
        //is Left Empty
        if(x>0 && directions[x-1,y].IsEmpty(indexValue))
            return -1;

        return 0;
    }

    private int getDy(int x,int y)
    {   
        //is Top Empty
        if(y<maxX && directions[x,y+1].IsEmpty(indexValue))
            return 1;
        if(y>0 && directions[x,y-1].IsEmpty(indexValue))
            return -1;

        return 0;

    }
}
