using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip MoveSound,GameOverSound,CanPassSound;

    AudioSource musicSource,effectSource;


    private void Start() {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        //musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume=0.4f;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnMove,OnMove);
        EventManager.AddHandler(GameEvent.OnIncreaseScore,OnCanPass);
    }
    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnMove,OnMove);
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore,OnCanPass);
    }

    void OnCanPass()
    {
        effectSource.PlayOneShot(CanPassSound);
    }

    void OnGameOver()
    {
        effectSource.PlayOneShot(GameOverSound);
    }

    void OnMove()
    {
        effectSource.PlayOneShot(MoveSound);
    } 
}
