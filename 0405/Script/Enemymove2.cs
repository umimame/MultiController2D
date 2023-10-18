using UnityEngine;

public class Enemymove2 : MonoBehaviour
{
    public float speed = 3.0f;  // 敵の移動速度
    public float detectionRange = 15.0f;  // プレイヤーを検知する範囲
    public float fireRate = 2.0f;  // 弾を撃つ頻度（秒）
    public GameObject bulletPrefab;  // 弾のプレハブ
    public float bulletSpeed = 10.0f;
    private Transform player1;  // プレイヤーのTransformコンポーネント
    private Transform player2;
    private Rigidbody2D rb;
    private float nextFireTime;  // 次に弾を撃つ時間

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player01").transform;  // プレイヤーのTransformを取得
        player2 = GameObject.FindGameObjectWithTag("Player02").transform;
        rb = GetComponent<Rigidbody2D>();
        nextFireTime = Time.time + fireRate;  // 最初に弾を撃つ時間を設定
    }

    void Update()
    {
        // プレイヤーと敵の距離を計算
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);

        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        Transform targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;

        // プレイヤーが検知範囲内にいるかチェック
        if (distanceToPlayer1 < detectionRange || distanceToPlayer2 < detectionRange)
        {
            // プレイヤーの方向を向く
            Vector2 direction = (targetPlayer.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // 弾を撃つ処理
            if (Time.time > nextFireTime)
            {
                Shoot(targetPlayer);
                nextFireTime = Time.time + fireRate;  // 次に弾を撃つ時間を更新
            }
        }
        else
        {
            rb.velocity = Vector2.zero;  // プレイヤーが検知範囲外にいる場合は停止
        }
        if (Input.GetKey("e"))
        {
            Destroy(gameObject);
        }
    }

    void Shoot(Transform targetPlayer)
    {
        // 弾を生成してプレイヤーの方向に発射
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}