using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy2 : MonoBehaviour
{
    private float speed;
    public int hp;
    private Rigidbody2D rigidBody;
    //public GameObject bakuhatu;
    //private ParticleSystem particle;
    [SerializeField] private Transform self;
    [SerializeField] private Transform target;

    //public GameObject particleSystemObject; // ParticleSystemがアタッチされているGameObjectをInspectorから設定します。

    //public ParticleSystem particleSystemComponent; // ParticleSystemコンポーネントを格納する変数
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        hp = 10;
        AssignPlayerAsTarget();
        rigidBody = GetComponent<Rigidbody2D>();
        //bakuhatu = GameObject.Find("ParticleSystem");


        // GameObjectからParticleSystemコンポーネントへのアクセスを取得
        //particleSystemComponent = particleSystemObject.GetComponent<ParticleSystem>();




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
            Destroy(this.gameObject);

            //particleSystemComponent.Play();
            // ParticleSystemコンポーネントが取得できた場合、操作を行うことができます
            // たとえば、再生する場合は次のようにします。
            //particleSystemComponent.Play();
            //Destroy(this.gameObject);

            //ParticleSystem newParticle = Instantiate(bakuhatu);
            //newParticle.transform.position = self.position;
            //newParticle.Play();

            // Destroy(this.gameObject);
            //// パーティクルシステムのインスタンスを生成する。
            //ParticleSystem newParticle = Instantiate(particle);
            //// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            //newParticle.transform.position = this.transform.position;
            //// パーティクルを発生させる。
            //newParticle.Play();
        }
        //プレイヤーの弾に当たったら
        //if (other.gameObject.CompareTag("PlayerBullet"))
        //{
        //    this.hp -= 5;
        //    if (this.hp <= 0)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }
    [ContextMenu("Assign Player as Target")]
    private void AssignPlayerAsTarget()
    {
        target = GameObject.Find("Player").transform;
    }
}
