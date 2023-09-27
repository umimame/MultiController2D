using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;

    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D other)
    {      // 当たった相手が"Player"タグを持っていたら
        if (other.gameObject.CompareTag("Player"))
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            //Destroy(newParticle.gameObject, 1.0f);
        }
    }
}
