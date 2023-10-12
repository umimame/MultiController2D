using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player_Bullet01"))
        {
            
            Destroy(other.gameObject);
            //currentHP -= clone.pow.entity;
        }
        if (other.gameObject.CompareTag("Player_Bullet02"))
        {
           
            Destroy(other.gameObject);
            //currentHP -= clone.pow.entity;
        }
    }
}
