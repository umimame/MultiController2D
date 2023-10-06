using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int GenSpan;
    public float time;
    public int GenNum;

    public List<Transform> targets; // 複数のターゲット

    // Start is called before the first frame update
    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 5;

        // ターゲットの初期化
        targets = new List<Transform>();
        AssignPlayersAsTargets();
    }

    // Update is called once per frame
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
                    float xPlus = Random.Range(15.0f, 30.0f);
                    float xMinus = Random.Range(-15.0f, -30.0f);
                    float y = Random.Range(-30.0f, 30.0f);
                    int RL = Random.Range(0, 2);

                    // ターゲットの位置に敵を生成
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(randomTarget.position.x + (RL == 0 ? xMinus : xPlus),
                        randomTarget.position.y + y, 0);

                    // 生成した敵のターゲットを設定
                    enemy.GetComponent<enemytest>().SetTarget(randomTarget);
                }
            }

            // 敵を生成した後に time を 0 にリセット
            time = 0f;
        }
    }

    [ContextMenu("Assign Players as Targets")]
    private void AssignPlayersAsTargets()
    {
        // "Player" タグを持つ全てのオブジェクトを取得
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // ターゲットリストをクリアして、新たにターゲットを追加
        targets.Clear();
        foreach (GameObject player in players)
        {
            targets.Add(player.transform);
        }
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
