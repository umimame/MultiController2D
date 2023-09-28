using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy2 : MonoBehaviour
{
    private float speed;
    public int hp;
    private Rigidbody2D rigidBody;
    [SerializeField] private Transform self;
    [SerializeField] private Transform target;
    // 爆発効果音
    public AudioClip explosionSE;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        hp = 10;
        AssignPlayerAsTarget();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.hp -= 5;
            if (this.hp <= 0)
            {
                Destroy(this.gameObject);
            }
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
        target = GameObject.Find("Player").transform;
    }
    void DestroySelf()
    {
        // オーディオを再生
        AudioSource.PlayClipAtPoint(explosionSE, this.transform.position);
        Destroy(this.gameObject);
    }
}
