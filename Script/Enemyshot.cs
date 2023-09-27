using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    enum ShotType
    {
        NONE = 0,
        AIM,
        THREE_WAY,
    }

    [System.Serializable]
    struct ShotData
    {
        public int frame;
        public ShotType type;
        public EnemyBullet bullet;
    }

    [SerializeField] ShotData shotData = new ShotData { frame = 60, type = ShotType.NONE, bullet = null };

    GameObject playerObj = null;
    int shotFrame = 0;

    void Start()
    {
        switch (shotData.type)
        {
            case ShotType.AIM:
                playerObj = GameObject.Find("Player");
                break;
        }
    }

    void Shot()
    {
        ++shotFrame;
        if (shotFrame > shotData.frame)
        {
            switch (shotData.type)
            {
                case ShotType.AIM:
                {
                      if (playerObj == null) { break; }
                      EnemyBullet bullet = (EnemyBullet)Instantiate(
                          shotData.bullet,
                          transform.position,
                          Quaternion.identity
                       );
                        bullet.SetMoveVec(playerObj.transform.position - transform.position);
                }
                break;

                case ShotType.THREE_WAY:
                {
                    EnemyBullet bullet = (EnemyBullet)Instantiate(
                        shotData.bullet,
                        transform.position,
                        Quaternion.identity
                     );
                        bullet = (EnemyBullet)Instantiate(shotData.bullet, transform.position, Quaternion.identity);
                        bullet.SetMoveVec(Quaternion.AngleAxis(15, new Vector3(0, 0, 1)) * new Vector3(-1, 0, 0));
                        bullet = (EnemyBullet)Instantiate(shotData.bullet, transform.position, Quaternion.identity);
                        bullet.SetMoveVec(Quaternion.AngleAxis(-15, new Vector3(0, 0, 1)) * new Vector3(-1, 0, 0));
                }
                break;
            }

            shotFrame = 0;
        }
    }
}
