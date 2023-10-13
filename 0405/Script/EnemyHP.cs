using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    GameObject bullet;
    private object clone;

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
        }
        if (other.gameObject.CompareTag("Player_Bullet02"))
        {  
            Destroy(other.gameObject);   
        }
    }

    public void TagJudge(GameObject bullet, int pow)
    {
        if (bullet.CompareTag("Player_Bullet01"))
        {
            transform.GetComponent<Bullet>();
            currentHP -= pow;
        }
        if (bullet.CompareTag("Player_Bullet02"))
        {
            transform.GetComponent<Bullet>();
            currentHP -= pow;
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}