using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using My;
using UnityEditor.Build.Content;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]
public class World : SingletonDontDestroy<World>
{
    [field: SerializeField] public KeyMap keyMap { get; private set; }
    [field: SerializeField] public PresetsByPlayerType presets { get; set; }
    [field: SerializeField] public List<GameObject> players { get; set; }
    [SerializeField] private List<PlayerController> playerController;
    [field: SerializeField] public GameObject player_KeybordMouse { get; private set; }
    [field: SerializeField] public GameObject player_Pad_CardinalDirectione { get; private set; }
    [field: SerializeField] public GameObject player_Pad_DoubleStick { get; private set; }
    [field: SerializeField] public StateByScenes SBS { get; private set; }
    [field: SerializeField] public List<ControllerType> controllerType { get; set; }
    [field: SerializeField] public bool timeStop { get; private set; }
    [field: SerializeField] public AlwaysUI debugUI { get; private set; }
    private PlayerInputManager inputManager;
    [SerializeField] private GameObject stage;
    private Hitstop hitstop;
    [SerializeField] private Instancer result;
    [field: SerializeField] public AudioSource audioSource {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        keyMap = new KeyMap();
        players = new List<GameObject>();
        playerController = new List<PlayerController>();
        inputManager = GetComponent<PlayerInputManager>();
        SBS = GetComponent<StateByScenes>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
    }

    private void Update()
    {
        switch (SBS.state)
        {
            case GameState.Title:
                PlayerInitialize();
                break;
            case GameState.GameStart:
                stage = GameObject.FindGameObjectWithTag("Stage");
                PlayerInstance();
                SBS.state = GameState.InGame;
                break;
            case GameState.InGame:
                if(playerController.Count <= 1) { SBS.state = GameState.GameEnd; }
                break;
            case GameState.GameEnd:
                if (gameObject.GetComponent<Hitstop>() == null)
                {
                    hitstop = gameObject.AddComponent<Hitstop>();
                }
                if(hitstop.state == SceneState.Next)
                {
                    result.Instance();
                    result.Last.GetComponentInChildren<ResultUI>().winnerPlayer = AddFunction.TagToArray(playerController[0].tag) + 1;
                    SBS.state = GameState.Result;
                    Destroy(hitstop);
                }
                break;
            case GameState.Result:
                break;

        }

        playerController.RemoveAll(PlayerRemove);

    }

    /// <summary>
    /// PlayerControllerÇÃstateÇ™DeathÇ»ÇÁListÇ©ÇÁçÌèú
    /// </summary>
    /// <param name="pc"></param>
    /// <returns></returns>
    private bool PlayerRemove(PlayerController pc)
    {
        if (pc == null) { return true; }
        if(pc.state == Chara.State.Death)
        {
            return true;
        }
        return false;
    }

    private void PlayerInitialize()
    {
        for(int i =0;i< controllerType.Count; i++)
        {
            controllerType[i] = ControllerType.Non;
        }
        players = new List<GameObject>();
        playerController = new List<PlayerController>();

    }

    private void PlayerInstance()
    {
        for (int i = 0; i < controllerType.Count; ++i)
        {
            switch (controllerType[i])
            {
                case ControllerType.KeybordMouse:
                    players.Add(Instantiate(player_KeybordMouse));
                    break;

                case ControllerType.Pad_CardinalDirection:
                    players.Add(Instantiate(player_Pad_CardinalDirectione));
                    break;

                case ControllerType.Pad_DoubleStick:
                    players.Add(Instantiate(player_Pad_DoubleStick));
                    break;
            }

            Debug.Log(i);

            players[i].tag = AddFunction.ArrayToTag(i);
            players[i].transform.position = presets.playerPos[i];
            playerController.Add(players[i].GetComponentInChildren<PlayerController>());
            playerController[i].ColorChange(presets.playerColorPre[i]);
            ClampByStage clamp = players[i].GetComponentInChildren<ClampByStage>();
            clamp.stage = stage;
        }
    }

    public void DebugUI()
    {
        if (Input.GetKey(KeyCode.F12) && debugUI.display == false)
        {
            debugUI.Display();
        }else if(Input.GetKey(KeyCode.F12) && debugUI.display == true)
        {
            debugUI.Close();
        }
    }

    public int PlayerTypes
    {
        get { return controllerType.Count; }
    }

    public void OnPerformed(InputAction.CallbackContext context)
    {

    }

    //void OnGUI()
    //{
    //    if (Gamepad.current == null) return;
    //    GUILayout.Label($"leftStick: {Gamepad.current.leftStick.ReadValue()}");
    //    GUILayout.Label($"buttonNorth: {Gamepad.current.buttonNorth.isPressed}");
    //    GUILayout.Label($"buttonSouth: {Gamepad.current.buttonSouth.isPressed}");
    //    GUILayout.Label($"buttonEast: {Gamepad.current.buttonEast.isPressed}");
    //    GUILayout.Label($"buttonWest: {Gamepad.current.buttonWest.isPressed}");
    //    GUILayout.Label($"leftShoulder: {Gamepad.current.leftShoulder.ReadValue()}");
    //    GUILayout.Label($"leftTrigger: {Gamepad.current.leftTrigger.ReadValue()}");
    //    GUILayout.Label($"rightShoulder: {Gamepad.current.rightShoulder.ReadValue()}");
    //    GUILayout.Label($"rightTrigger: {Gamepad.current.rightTrigger.ReadValue()}");
    //}


}
public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject); // í«â¡
        }
        else
            Destroy(gameObject);
    }
}


public enum ControllerType
{
    KeybordMouse,
    Pad_CardinalDirection,
    Pad_DoubleStick,
    Non,
}
