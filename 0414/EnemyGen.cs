using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int GenSpan;
    private float time;
    public int GenNum;

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 10;
        AssignPlayerAsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > GenSpan)
        {
            for (int i = 0; i < GenNum; i++)
            {
                float xPlus = Random.Range(15.0f, 30.0f);
                float xMinus = Random.Range(-15.0f, -30.0f);
                float y = Random.Range(-30.0f, 30.0f);
                int RL = Random.Range(0, 2);
                if (RL == 0)//プレイヤーを中心に左側に生成
                {
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(target.transform.position.x + xMinus,
                       target.transform.position.y + y, 0);
                    time = 0f;
                }
                else //プレイヤーを中心に右側に生成
                {
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(target.transform.position.x + xPlus,
                       target.transform.position.y + y, 0);
                    time = 0f;
                }

            }
        }
    }
    [ContextMenu("Assign Player as Target")]
    private void AssignPlayerAsTarget()
    {
        target = GameObject.Find("Player").transform;
    }

}
