using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text Result;
    [SerializeField] private Text Who;
    [SerializeField] private Text PlayerName;
    [SerializeField] public int winnerPlayer { get; set; } = 0;
    [SerializeField] private Text HowManyKills;
    [SerializeField] private Text KillsCount;
    [SerializeField] private Text HowManyShots;
    [SerializeField] private Text ShotsCount;
    [SerializeField] private Text Time;
    [SerializeField] private Text TimeCount;
    [SerializeField] private Text ClickToTitle;

    [SerializeField] private AudioSource playerNameSE;
    [SerializeField] private AudioSource drumRoll;

    [SerializeField] private SceneAsset titleScene;
    
    private bool spaceKeyEntered = false; // Space キーが押されたかどうかのフラグ
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayText());
        Debug.Log("playerNameSEのデカさ" + playerNameSE.volume);
        Debug.Log("drumRollのデカさ" + drumRoll.volume);
    }

    // Update is called once per frame
    void Update()
    {
        KillsCount.color = Color.red;
        ShotsCount.color = Color.red;
        TimeCount.color = Color.red;
        // PlayerName.textの内容によって色を変更
        if (PlayerName.text.Contains("プレイヤー" + winnerPlayer))
        {
            PlayerName.color = UnityEngine.Color.green;
        }
        else if (PlayerName.text.Contains("プレイヤー" + winnerPlayer))
        {
            PlayerName.color = UnityEngine.Color.blue;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spaceKeyEntered = true;
        }
    }
    private IEnumerator DisplayText()//時間でテキスト表示
    {
        yield return new WaitForSeconds(1f);
        Result.text = "試合結果";
        DrumRoll();

        yield return new WaitForSeconds(1f);
        Who.text = "勝者は...";

        yield return new WaitForSeconds(2f);
        drumRoll.Stop();

        PlayerName.text = "プレイヤー" + winnerPlayer; // 最後に残ったプレイヤーのタグを参照
        PlayerNameSE();

        yield return new WaitForSeconds(3f);
        HowManyKills.text = "倒された雑魚敵の数:";
        KillsCount.text = "殺した数";//変数参照,変数未実装

        yield return new WaitForSeconds(1f);
        HowManyShots.text = "発射された弾の数:";
        ShotsCount.text = "発射した数";//変数未実装

        yield return new WaitForSeconds(1f);
        Time.text = "試合時間:";
        TimeCount.text = "時間数える変数";//変数未実装

        yield return new WaitForSeconds(0.5f);
        ClickToTitle.text = "SPACEキーでタイトルに戻る";
        StartCoroutine(BlinkText());

        yield return new WaitUntil(() => spaceKeyEntered);
        //Spaceキーが押された後の処理
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(titleScene.name);
        Debug.Log("タイトル行きまっせ～");
    }

    IEnumerator BlinkText()//テキスト点滅用
    {
        while (true)
        {
            // テキストを点滅させる
            ClickToTitle.color = new Color(1f, 0f, 0f, 0f); // アルファ値を0に透明になる
            yield return new WaitForSeconds(0.5f); // 0.5秒待つ

            ClickToTitle.color = new Color(1f, 0f, 0f, 1f); // アルファ値を1に透明解除
            yield return new WaitForSeconds(0.5f); // 0.5秒待つ
        }
    }
    void PlayerNameSE()
    {
        playerNameSE.PlayOneShot(playerNameSE.clip);
    }
    void DrumRoll()
    {
        drumRoll.PlayOneShot(drumRoll.clip);
    }
}
