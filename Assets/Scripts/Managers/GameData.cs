using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Data", menuName ="Data/Game Data",order =0)]
public class GameData : ScriptableObject
{
    public int score,highScore;

    public float RemainingTime;

    public bool timerIsRunning=false;

    //ilerde gelistirmelerle artar
    public int increaseScore=1;
    
    
}
