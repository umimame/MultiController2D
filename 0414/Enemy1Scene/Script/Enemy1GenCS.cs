using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1GenCS : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField][Tooltip("敵の生成間隔時間")] private int GenSpan;
    [SerializeField][Tooltip("敵が生成されてからの時間(生成されたらリセット)")] private float time;
    [SerializeField][Tooltip("生成する敵の数")] private int GenNum;

    [SerializeField] private List<Transform> targets; // 複数のターゲット

    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 10;

        // ターゲットの初期化
        targets = new List<Transform>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= GenSpan)
        {
            for (int i = 0; i < GenNum; i++)
            {
                // ランダムにターゲットを選択
                Transform randomTarget = GetRandomTarget();

                if (randomTarget != null)
                {
                    float xPlus = Random.Range(30.0f, 50.0f);
                    float xMinus = Random.Range(-30.0f, -50.0f);
                    float y = Random.Range(-50.0f, 50.0f);
                    int RL = Random.Range(0, 2);

                    // ターゲットの位置に敵を生成
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(randomTarget.position.x + (RL == 0 ? xMinus : xPlus),
                        randomTarget.position.y + y, 0);
                    //Debug.Log("俺、参上！");
                    // 生成した敵のターゲットを設定
                    enemy.GetComponent<Enemy1CS>().SetTarget(randomTarget);
                }
            }

            // 敵を生成した後に time を 0 にリセット
            time = 0f;
        }
        //Debug.Log("Time: " + time);
    }

    [ContextMenu("Assign Players as Targets")]
    public void AssignPlayersAsTargets()
    {
        Debug.Log("Assign");
        // "Player01" または "Player02" タグを持つ全てのオブジェクトを取得
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player01");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player02");

        // ターゲットリストをクリアして、新たにターゲットを追加
        targets.Clear();

        // プレイヤー1の配列をリストに追加
        foreach (GameObject player in players1)
        {
            if(player.TryGetComponent(out Rigidbody2D rb))
            {
                targets.Add(player.transform);
            }
        }

        // プレイヤー2の配列をリストに追加
        foreach (GameObject player in players2)
        {

            if (player.TryGetComponent(out Rigidbody2D rb))
            {

                targets.Add(player.transform);
            }
        }

        Enemy1CS clone = EnemyPrefab.GetComponent<Enemy1CS>();
        clone.players1 = players1;
        clone.players2 = players2;
    }
    // ランダムにターゲットを選択する
    private Transform GetRandomTarget()
    {
        if (targets.Count == 0)
            return null;

        // ランダムにインデックスを選択
        int randomIndex = Random.Range(0, targets.Count);

        return targets[randomIndex];
    }
}
