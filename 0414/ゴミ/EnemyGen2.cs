using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen2 : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int GenSpan;
    public float time;
    public int GenNum;

    private List<Transform> targets; // 複数のターゲット

    // Start is called before the first frame update
    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 3;

        // ターゲットの初期化
        targets = new List<Transform>();
        AssignPlayersAsTargets();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > GenSpan)
        {
            for (int i = 0; i < GenNum; i++)
            {
                // 最も近いターゲットを取得
                Transform closestTarget = GetClosestTarget();

                if (closestTarget != null)
                {
                    float xPlus = Random.Range(15.0f, 30.0f);
                    float xMinus = Random.Range(-15.0f, -30.0f);
                    float y = Random.Range(-30.0f, 30.0f);
                    int RL = Random.Range(0, 2);

                    // ターゲットの位置に敵を生成
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(closestTarget.position.x + (RL == 0 ? xMinus : xPlus),
                        closestTarget.position.y + y, 0);

                    // 生成した敵のターゲットを設定
                    enemy.GetComponent<enemytest>().SetTarget(closestTarget);
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

    // 最も近いターゲットを取得する
    private Transform GetClosestTarget()
    {
        if (targets.Count == 0)
            return null;

        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform target in targets)
        {
            float distanceToTarget = Vector3.Distance(currentPosition, target.position);
            if (distanceToTarget < closestDistance)
            {
                closestTarget = target;
                closestDistance = distanceToTarget;
            }
        }

        return closestTarget;
    }
}
