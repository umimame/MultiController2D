using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelect : MonoBehaviour
{
    private GameObject CharaSelectManagerObject;
    CharaSelectManager CharaManager;

    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;

    int Player1 = 0;
    int Player2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        CharaSelectManager.instance.subroutine();

        CharaSelectManagerObject = GameObject.Find("CharaSelectManager");
        CharaManager = CharaSelectManagerObject.GetComponent<CharaSelectManager>();

        Player1 = CharaManager.SelPlayer1;
        Player2 = CharaManager.SelPlayer2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1 == 1)
        {
            // 生成位置
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character1, pos, Quaternion.identity);
        }
        if (Player1 == 2)
        {
            // 生成位置
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character2, pos, Quaternion.identity);
        }
        if (Player1 == 3)
        {
            // 生成位置
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character3, pos, Quaternion.identity);
        }

        if (Player2 == 1)
        {
            // 生成位置
            Vector3 pos = new Vector3(1.0f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character1, pos, Quaternion.identity);
        }
        if (Player2 == 2)
        {
            // 生成位置
            Vector3 pos = new Vector3(1.5f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character2, pos, Quaternion.identity);
        }
        if (Player2 == 3)
        {
            // 生成位置
            Vector3 pos = new Vector3(1.5f, 0.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(Character3, pos, Quaternion.identity);
        }
    }
}

