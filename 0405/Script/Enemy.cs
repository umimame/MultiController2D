using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : Chara
{
    public float detectionRange = 15.0f;  // プレイヤーを検知する範囲
    public float fireRate = 2.0f;  // 弾を撃つ頻度（秒）
    public GameObject bulletPrefab;  // 弾のプレハブ
    public GameObject itemPrefab;
    public float bulletSpeed = 10.0f;
    public AudioClip shootSound; // 弾を撃つSE
    [SerializeField] private GameObject player1;  // プレイヤーのTransformコンポーネント
    [SerializeField] private GameObject player2;
    private object clone;
    private float nextFireTime; // 次に弾を撃つ時間
    private AudioSource audioSource; // AudioSourceコンポーネント
    private Collider2D movementArea;
    public bool enemymove = true;

    protected override void Start()
    {
        base.Start();
        nextFireTime = Time.time + fireRate; // 最初に弾を撃つ時間を設定
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
        movementArea = GetComponent<Collider2D>();
         player1 = GameObject.FindWithTag("Player01");
         player2 = GameObject.FindWithTag("Player02");
    }

    protected override void Update()
    {
        base.Update();
        if (player1 == null || player2 == null)
        {
            enemymove = false;
        }
        if (enemymove == true)
        {
            // プレイヤーと敵の距離を計算
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.transform.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.transform.position);

            // プレイヤーが検知範囲内にいるかチェック
            if (IsWithinDetectionRange(distanceToPlayer1) || IsWithinDetectionRange(distanceToPlayer2))
            {
                GameObject targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
                // プレイヤーの方向を向く
                Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
                engine.rb.velocity = direction * speed.entity;

                // 弾を撃つ処理
                if (Time.time > nextFireTime)
                {
                    Shoot(targetPlayer);
                    audioSource.PlayOneShot(shootSound);
                    nextFireTime = Time.time + fireRate; // 次に弾を撃つ時間を更新
                }
            }
            else
            {
                engine.rb.velocity = Vector2.zero; // プレイヤーが検知範囲外にいる場合は停止
            }

            if (Input.GetKey("e"))
            {
                if (Random.value <= 0.3f)
                {
                    DropItem();
                }
                Destroy(gameObject);
            }
        }
        else if (enemymove == false)
        {
            engine.rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player02"))
        {
            Destroy(other.gameObject);
        }
    }

    protected virtual void UnderAttack(Collider2D co)
    {
        if (co.tag != transform.tag) // 衝突先のタグが自軍と異なる場合
        {
            Debug.Log("UnderAttack");
            if (co.GetComponent<Bullet>())
            {
                Bullet coScript = co.GetComponent<Bullet>();
                hp.entity -= coScript.pow.entity;
                Debug.Log(hp.entity);
            }
        }
    }

    bool IsWithinDetectionRange(float distanceToPlayer)
    {
        // 検知範囲内にいるかどうかを確認
        return distanceToPlayer <= detectionRange;
    }

    void Shoot(GameObject targetPlayer)
    {
        // 弾を生成してプレイヤーの方向に発射
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void DropItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }

}

