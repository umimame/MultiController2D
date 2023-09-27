using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int GenSpan;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        GenSpan = 3;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(0f, 10.0f);
        float y = Random.Range(-5.0f, 5.0f);
        time += Time.deltaTime;

        if (time > GenSpan)
        {
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.transform.position = new Vector3(x, y, 0);
            time = 0f;
        }
    }
}
