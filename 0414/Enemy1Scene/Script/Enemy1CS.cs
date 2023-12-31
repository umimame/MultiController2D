using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy1CS : MonoBehaviour
{
    private float speed;
    [SerializeField][Tooltip("自身のTransform")] private Transform self;
    [SerializeField][Tooltip("targetのTransform")] private Transform target;
    // 爆発効果音
    [SerializeField][Tooltip("自身が破壊されたときの音")] private AudioClip explosionSE;
    // 移動中の音
    [SerializeField][Tooltip("自身が移動中の音")] private AudioSource moveSE;
    // 爆発エフェクト
    private EnemyParticle enemyParticle;

    [field: SerializeField] public GameObject[] players1 { get; set; }
    [field: SerializeField] public GameObject[] players2 { get; set; }


    public Vector3 scale;
    public Vector3 scale2;
    // Start is called before the first frame update
    void Start()
    {
        //speed = Random.Range(10.0f, 30.0f);
        speed = 10.0f;
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
        if (other.transform == target)
        {
            DestroySelf();
        }
    }
    // ターゲットを設定する
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    //自身を殺す
    void DestroySelf()
    {
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
    // 生成されたときに2つのプレイヤーとの距離を比較して、近い方をターゲットに設定
    void SetInitialTarget()
    {

        List<Transform> allPlayers = new List<Transform>();

        allPlayers.AddRange(players1.Select(player => player.transform));
        allPlayers.AddRange(players2.Select(player => player.transform));

        if (allPlayers.Count >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, allPlayers[0].position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, allPlayers[1].position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(allPlayers[0]);

                //テスト用の処理

                //gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                SetTarget(allPlayers[1]);

                //テスト用の処理
                //gameObject.transform.localScale = new Vector3(scale2.x, scale2.y, scale2.z);
                GetComponent<Renderer>().material.color = Color.blue;

            }
        }
        else if (allPlayers.Count == 1)
        {
            SetTarget(allPlayers[0]);
        }
    }
    // 常に（移動中も）2つのプレイヤーとの距離を比較して、近い方をターゲットに設定
    void CheckForNewTarget()
    {
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player01");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player02");

        List<Transform> allPlayers = new List<Transform>();

        allPlayers.AddRange(players1.Select(player => player.transform));
        allPlayers.AddRange(players2.Select(player => player.transform));

        if (allPlayers.Count >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, allPlayers[0].position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, allPlayers[1].position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(allPlayers[0]);

                //テスト用の処理
                //gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                GetComponent<Renderer>().material.color = Color.red;

            }
            else
            {
                SetTarget(allPlayers[1]);
                //テスト用の処理
                //gameObject.transform.localScale = new Vector3(scale2.x, scale2.y, scale2.z);
                GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        else if (allPlayers.Count == 1)
        {
            SetTarget(allPlayers[0]);
        }
    }
}
