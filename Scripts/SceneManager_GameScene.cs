using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class SceneManager_GameScene : MonoBehaviour
{
    [field: SerializeField] public SceneState state { get; set; }
    [field: SerializeField] public AudioSource BGM { get; set; }
    private void Start()
    {
        BGM.Play();
        
    }
    private void Update()
    {
        switch(state)
        {
            case SceneState.Start:
                if(World.instance.SBS.state == GameState.InGame)
                {
                    state = SceneState.Idol;
                }
                break;
            case SceneState.Idol:
                break;
            case SceneState.Next:
                break;
        }
    }
}
