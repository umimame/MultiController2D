using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrontCanvas : SingletonDontDestroy<FrontCanvas>
{
    [SerializeField] private Instancer quitgameCanvas;
    [SerializeField] private KeyMap keyMap;
    [field: SerializeField] public PlayerInput input { get; set; }
    private void Start()
    {
        keyMap = new KeyMap();
        keyMap.Enable();

    }
    private void Update()
    {
        if(keyMap.Public.Pause.ReadValue<float>() >= 1.0f)
        {

            if (quitgameCanvas.Displaying == false)
            {
                quitgameCanvas.Instance();
            }
        }
        quitgameCanvas.Update();
    }
    public void OnPause(InputValue value)
    {
        Debug.Log("Pause");
        if(quitgameCanvas.Displaying == false)
        {
            quitgameCanvas.Instance();
        }
    }


    public void OnPause(InputAction.CallbackContext value)
    {
        Debug.Log("Pause");
        if (quitgameCanvas.Displaying == false)
        {
            quitgameCanvas.Instance();
        }
    }

}
