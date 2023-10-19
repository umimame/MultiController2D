using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;
    private float time;
    public Transform target;
    [field: SerializeField] public bool active { get;set; }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowPosition = target.position;
        if(GetComponent<Camera>() != null ) // アタッチ先のオブジェクトがカメラなら
        {
            nowPosition.z = -10f;
        }
        if ( time <= 0f)
        {
            // Space キーが押され、かつ揺れの処理が開始されていない場合

            // 経過時間を加算
            time += Time.deltaTime;
        }

        // 経過時間が揺れの期間以下の間、カメラを揺らし続ける
        if (time > 0f && time < shakeDuration && active == true)
        {
            // ランダムな揺れ量を取得
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);

            nowPosition.x += shakeX;
            nowPosition.y += shakeY;
            transform.position = nowPosition;

            //// カメラの位置に揺れを加算
            //Vector3 newPosition = transform.position;
            //newPosition.x += shakeX;
            //newPosition.y += shakeY;
            //// カメラの位置を更新
            //transform.position = newPosition;

            // 経過時間を加算
            time += Time.deltaTime;
        }
        else
        {
            // 揺れの期間を超えたら time を 0 に戻す
            time = 0f;
            Vector3 resetPotision = transform.position;
            resetPotision.x = target.position.x;
            resetPotision.y = target.position.y;
            transform.position = resetPotision;
            active = false;
        }
    }

}
