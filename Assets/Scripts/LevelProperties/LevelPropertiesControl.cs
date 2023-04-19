using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPropertiesControl : MonoBehaviour
{
    public GameData gameData;

    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 cameraPoint;

    [SerializeField] private Transform mainCam;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);    
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    void OnNextLevel()
    {
        gameData.StartPoint=startPoint;
        mainCam.position=cameraPoint;
    }
}
