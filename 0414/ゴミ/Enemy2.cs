using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    private float speed;
    public int hp;
    [SerializeField] private Transform self;
    [SerializeField] private Transform target;
    // 爆発効果音
    public AudioClip explosionSE;

    //移動中の音
    public AudioSource moveSE;

    private EnemyParticle enemyParticle;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(10.0f, 30.0f); ;
        //speed = 1.0f;
        hp = 10;
        AssignPlayerAsTarget();
        MoveSE();
        enemyParticle = GetComponent<EnemyParticle>();
        ////変数をEnemyParticleに渡す
        //enemyParticle.GetEnemyHp(hp);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G))
        {
            DestroySelf();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Enemy2csの" + hp);
        }
    }
    void Move()
    {
        Vector3 dir = (target.position - self.position);
        self.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        self.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Playerと接触しました"); 
        if (other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
        //プレイヤーの弾に当たったら
        //if (other.gameObject.CompareTag("PlayerBullet"))
        //{
        //    this.hp -= 5;
        //    if (this.hp <= 0)
        //    {
        //        DestroySelf();
        //    }
        //}

    }
    [ContextMenu("Assign Player as Target")]
    private void AssignPlayerAsTarget()
    {
        //Playerタグを持つオブジェクトをターゲットに設定する
        target = GameObject.Find("Player").transform;
    }
    void DestroySelf()
    {
        Debug.Log("ぐえ～死んだンゴ");
        ExplosionSE();
        // EnemyParticle スクリプトの OnTriggerEnter2D メソッドを呼び出す
        enemyParticle.TriggerEnemyParticle();
        //自身を破壊
        Destroy(this.gameObject);
    }
    void MoveSE()
    {
        // AudioSourceコンポーネントを取得
        moveSE = GetComponent<AudioSource>();

        // オーディオが再生可能な場合
        if (moveSE != null)
        {
            // オーディオを再生
            moveSE.Play();
        }
    }
    void ExplosionSE()
    {
        // オーディオを再生
        AudioSource.PlayClipAtPoint(explosionSE, this.transform.position);
    }
}
