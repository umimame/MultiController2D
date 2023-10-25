using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaSelectManager : MonoBehaviour
{
    public static CharaSelectManager instance;

    public Text PlayerName;
    public Text PushKey;

    public int Character1 = 1;
    public int Character2 = 2;
    public int Character3 = 3;

    public int SelPlayer1 = 0;
    public int SelPlayer2 = 0;
    [SerializeField] private string gameScene;

    int SelectNum = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void subroutine()
    {
        Debug.Log("サブルーチンコール");
    }

    public void OnClick1()
    {
        if (SelectNum == 0)
        {
            SelPlayer1 = Character1;
        }
        else
        {
            SelPlayer2 = Character1;
        }
    }
    public void OnClick2()
    {
        if (SelectNum == 0)
        {
            SelPlayer1 = Character2;
        }
        else
        {
            SelPlayer2 = Character2;
        }
    }
    public void OnClick3()
    {
        if (SelectNum == 0)
        {
            SelPlayer1 = Character3;
        }
        else
        {
            SelPlayer2 = Character3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SelPlayer1 == Character1 || SelPlayer1 == Character2 || SelPlayer1 == Character3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SelectNum = 1;
                PlayerName.text = "Player2";
                PushKey.text = "Please S Key";
            }
        }

        if (SelPlayer2 == Character1 || SelPlayer2 == Character2 || SelPlayer2 == Character3)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene(gameScene);
            }
        }
    }
}
