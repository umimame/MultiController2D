using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class World : SingletonDontDestroy<World>
{
    public enum ControllerType
    {
        KeybordMouse,
        Pad_CardinalDirection,
        Pad_DoubleStick,
    }
    public enum GameState
    {
        Title,
        StartGame,
        InGame,
        GameEnd,
        Result,
        Non,
    }
    [field: SerializeField] public KeyMapCallBack keyMap { get; private set; }
    [field: SerializeField] public PresetsByPlayerType presets { get; set; }
    [field: SerializeField] public List<GameObject> players { get; set; }
    [SerializeField] private List<PlayerController> playerController;
    [field: SerializeField] public GameObject player_KeybordMouse { get; private set; }
    [field: SerializeField] public GameObject player_Pad_CardinalDirectione { get; private set; }
    [field: SerializeField] public GameObject player_Pad_DoubleStick { get; private set; }
    [field: SerializeField] public GameState gameState { get; private set; }
    [field: SerializeField] public List<ControllerType> controllerType { get; set; }
    [field: SerializeField] public int playerCount { get; set; }
    [field: SerializeField] public bool timeStop { get; private set; }
    [field: SerializeField] public AlwaysUI debugUI { get; private set; }
    private PlayerInputManager inputManager;
    [SerializeField] private GameObject stage;
    [SerializeField] private SceneAsset endGameScene;
    [SerializeField] private InstanceUI resultController;

    protected override void Awake()
    {
        base.Awake();
        keyMap = new KeyMapCallBack();
        players = new List<GameObject>();
        playerController = new List<PlayerController>();
        inputManager = GetComponent<PlayerInputManager>();
        PlayerInstance();
    }
    private void Start()
    {
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Title:
                gameState = GameState.InGame;
                break;
            case GameState.InGame:

                break;
            case GameState.GameEnd:
                resultController.Instance();
                gameState = GameState.Result;
                break;
            case GameState.Result:
                break;

        }
        playerCount = 0;
        for(int i =0;i<players.Count; ++i)
        {
            if(playerController[i].state != Chara.State.Death)
            {
                playerCount++;
            }
        }
        if(gameState == GameState.InGame && playerCount <= 1)
        {
            gameState = GameState.GameEnd;
        }
    }

    private void PlayerInstance()
    {
        string schema = "";
        for (int i = 0; i < controllerType.Count; ++i)
        {
            switch (controllerType[i])
            {
                case ControllerType.KeybordMouse:
                    players.Add(Instantiate(player_KeybordMouse));
                    schema = "Keybord";
                    break;

                case ControllerType.Pad_CardinalDirection:
                    players.Add(Instantiate(player_Pad_CardinalDirectione));
                    schema = "Pad";
                    break;

                case ControllerType.Pad_DoubleStick:
                    players.Add(Instantiate(player_Pad_DoubleStick));
                    schema = "Pad";
                    break;
            }

            Debug.Log(i);

            players[i].tag = AddFunction.ArrayToTag(i);
            players[i].transform.position = presets.playerPos[i];
            playerController.Add(players[i].GetComponentInChildren<PlayerController>());
            playerController[i].ColorChange(presets.playerColorPre[i]);
            ClampByStage clamp = players[i].GetComponentInChildren<ClampByStage>();
            clamp.stage = stage;
            Debug.Log(playerController[i].input.currentControlScheme);
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
    void OnGUI()
    {
        if (Gamepad.current == null) return;

        GUILayout.Label($"leftStick: {Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"buttonNorth: {Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"buttonSouth: {Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"buttonEast: {Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"buttonWest: {Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"leftShoulder: {Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"leftTrigger: {Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"rightShoulder: {Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"rightTrigger: {Gamepad.current.rightTrigger.ReadValue()}");

    }

}
public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject); // 追加
        }
        else
            Destroy(gameObject);
    }
}

public static class AddFunction
{

    /// <summary>
    /// Vector2を角度(360度)に変更
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float Vec2ToAngle(Vector2 v)
    {
        return Mathf.Repeat(Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg, 360);
    }

    public static float GetAngleByVec2(Vector3 start, Vector3 target)
    {
        float angle;
        Vector3 dt = start - target;
        angle = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

        return angle;
    }

    public static Vector3 CameraToMouse()
    {
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
    }

    /// <summary>
    /// タグ名を要素（iなど）にして返す
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static int TagToArray(string tag)
    {
        switch (tag)
        {
            case "Player01":
                return 0;
            case "Player02":
                return 1;
        }
        return -1;
    }

    /// <summary>
    /// 要素（iなど）をタグ名にして返す
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static string ArrayToTag(int array)
    {
        switch (array)
        {
            case 0:
                return "Player01";
            case 1:
                return "Player02";
        }
        return "0";
    }
}

public class KeyMapCallBack
{
    public static KeyMap keyMap;

    public KeyMapCallBack()
    {
        keyMap = new KeyMap();
        keyMap.Enable();
    }

    public static Vector2 Move{ get { return keyMap.Keybord.Move.ReadValue<Vector2>(); } }
    public static bool Inputting
    {
        get
        {
            if (Move == new Vector2(0, 0))
            {
                return false;
            }
            return true;
        }
    }

    public static float DebugMode
    {
        get {
            Debug.Log("Debug");
            return keyMap.Keybord.Debug.ReadValue<float>(); }
    }

    public static void Schema()
    {
        foreach(var device in InputSystem.devices)
        {
            Debug.Log(device);
        }
    }
}
/// <summary>
/// 移動範囲を円形に制限
/// </summary>
[Serializable] public class CircleClamp
{

    [SerializeField] private GameObject center;
    [field: SerializeField] public GameObject moveObject { get; set; }
    [SerializeField] private float radius;

    public void Initialize()
    {
        moveObject.transform.position = center.transform.position;
    }
    public void Limit()
    {
        if(Vector2.Distance(moveObject.transform.position, center.transform.position) > radius)
        {
            Vector3 nor = moveObject.transform.position - center.transform.position;
            moveObject.transform.position = center.transform.position + nor.normalized * radius;
        }
    }
}


[Serializable] public class PresetByPlayerType<T>
{
    [SerializeField] private int playerType;
    [field: SerializeField] public List<T> presets { get; set; }
    [ContextMenu("PresetsResize")]
    public void Initialize()
    {
        playerType = World.instance.PlayerTypes;
        presets = new List<T>(playerType);
    }
}

/// <summary>
/// 間隔制御クラス
/// </summary>
[Serializable] public class Interval
{
    [field: SerializeField] public bool active { get; private set; }
    [SerializeField] private float interval;
    [SerializeField] private float time;
    [field: SerializeField] public bool timeOverride;

    /// <summary>
    /// 引数には最初から使用できるかどうかを記述する
    /// </summary>
    /// <param name="start"></param>
    public void Initialize(bool start)
    {
        if(start == true)
        {
            time = interval;
        }
        else
        {
            time = 0.0f;
        }
        active = (time >= interval) ? true : false;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if(time >= interval)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    /// <summary>
    /// 引数は「引数無しのメソッド」
    /// </summary>
    /// <param name="action"></param>
    public void Launch(Action action)
    {
        if(active == true)
        {

            action();
        }
    }

    public void Reset()
    {
        time = 0.0f;
    }
}

[Serializable] public class Shake
{
    [field: SerializeField] public Interval interval { get; set; }
    [field: SerializeField] public GameObject targetObj { get; set; }

    public void Initialize()
    {
        interval.Initialize(true);
    }

    public void Update()
    {
        if(interval.active == true)
        {
        }
        interval.Update();

    }

}

[Serializable] public class InstanceUI
{
    [field: SerializeField] public GameObject UI { get; set; }
    [SerializeField] private List<GameObject> clone = new List<GameObject>();

    public void Instance()
    {
        clone.Add(GameObject.Instantiate(UI));
    }

    public void Death()
    {
        GameObject.Destroy(clone[0]);   // 最後から消す
        clone.RemoveAt(0);
    }
}