using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytest : MonoBehaviour
{
    private float speed;
    public int hp;
    [SerializeField] private Transform self;
    private Transform target;
    // 爆発効果音
    public AudioClip explosionSE;
    // 移動中の音
    public AudioSource moveSE;
    // 爆発エフェクト
    private EnemyParticle enemyParticle;

    // Start is called before the first frame update
    void Start()
    {
        //speed = Random.Range(10.0f, 30.0f);
        speed = 10.0f;
        hp = 10;
        MoveSE();
        enemyParticle = GetComponent<EnemyParticle>();
        // 初期ターゲットを設定
        SetInitialTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G))
        {
            DestroySelf();
        }

        // 新しいターゲットが近くにあるか確認
        CheckForNewTarget();
    }

    void Move()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - self.position);
            self.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            self.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player タグを持つオブジェクトに当たったら
        if (other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
    }

    // ターゲットを設定するメソッド
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void DestroySelf()
    {
        Debug.Log("ぐえ～死んだンゴ");
        ExplosionSE();
        enemyParticle.TriggerEnemyParticle();
        Destroy(this.gameObject);
    }

    void MoveSE()
    {
        moveSE = GetComponent<AudioSource>();

        if (moveSE != null)
        {
            moveSE.Play();
        }
    }

    void ExplosionSE()
    {
        AudioSource.PlayClipAtPoint(explosionSE, this.transform.position);
    }

    // 2つのプレイヤーとの距離を比較して、近い方をターゲットに設定
    void SetInitialTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, players[0].transform.position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, players[1].transform.position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(players[0].transform);
            }
            else
            {
                SetTarget(players[1].transform);
            }
        }
        else if (players.Length == 1)
        {
            SetTarget(players[0].transform);
        }
    }

    // 2つのプレイヤーとの距離を比較して、近い方をターゲットに設定
    void CheckForNewTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, players[0].transform.position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, players[1].transform.position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(players[0].transform);
            }
            else
            {
                SetTarget(players[1].transform);
            }
        }
        else if (players.Length == 1)
        {
            SetTarget(players[0].transform);
        }
    }
}
