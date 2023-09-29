using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;

    void Update()
    {

    }
    public void TriggerEnemyParticle()
    {
        // ここに EnemyParticle の OnTriggerEnter2D メソッドに関連する処理を実装
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
