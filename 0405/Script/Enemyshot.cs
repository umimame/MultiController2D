using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    public GameObject CirclPrefab;
    private int count;

    void Update()
    {
        count += 1;

        
        if (count % 3000 == 0)
        {
            GameObject Circl = Instantiate(CirclPrefab, transform.position, Quaternion.identity);
            Rigidbody2D CirclRb = Circl.GetComponent<Rigidbody2D>();

            
        }
    }
}
