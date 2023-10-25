using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManager_TitleScene : MonoBehaviour
{

    [SerializeField] private SceneState state;
    [SerializeField] private ShockWaveInstancer shockWave;
    [SerializeField] private FadeInstancer fadeUI;
    [SerializeField] private InputAction input;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private KeyMap keyMap;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        state = SceneState.Idol;
        shockWave.Initialize();
        fadeUI.Initialize();
        keyMap = new KeyMap();
        keyMap.Enable();
        //FrontCanvas.instance.input.onActionTriggered += OnPositive;
    }

    private void Update()
    {
        shockWave.Update();
        fadeUI.Update();
        if(keyMap.Public.Positive.ReadValue<float>() >= 1.0f && state == SceneState.Idol)
        {

            if (state == SceneState.Idol)
            {
                shockWave.Instance();
                audioSource.Stop();
                state = SceneState.Start;
            }
        }

        switch (state)
        {
            case SceneState.Idol:
                break;
            case SceneState.Start:
                if (shockWave.Displaying == false)
                {
                    fadeUI.Instance();
                    fadeUI.Last.transform.SetAsLastSibling();   // Hierarchy‚Ìˆê”Ô‰º‚É”z’u
                    //SceneManager.LoadScene(scene.name);

                    state = SceneState.Next;
                }
                break;
            case SceneState.Next:
                break;
        }
    }

    public void OnDisable()
    {
    }

    public void OnPositive(InputValue value)
    {
        if (state == SceneState.Idol)
        {
            shockWave.Instance();
            audioSource.Stop();
            state = SceneState.Start;
        }
    }

    public void OnPositive(InputAction.CallbackContext context)
    {
        if (state == SceneState.Idol)
        {
            shockWave.Instance();
            audioSource.Stop();
            state = SceneState.Start;
        }

    }
}
