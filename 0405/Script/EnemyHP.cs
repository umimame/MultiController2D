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

   public void TagJudge()
   {
        if (bullet.CompareTag("Player01"))
        {
            //Bullet clone = bullet.GetComponent<Bullet>();

            //currentHP -= clone.pow.entity
        }
   }
}
