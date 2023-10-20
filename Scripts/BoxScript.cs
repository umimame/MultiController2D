using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BoxScript : MonoBehaviour
{
    private ParticleSystem particle;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        // 当たった相手が"Tama1"タグを持っていたら
        if (collision.gameObject.tag == "Tama1")
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
