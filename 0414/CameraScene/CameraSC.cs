using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeDuration;
    private float time;
    public Transform target;

    private void Start()
    {
        shakeAmount = 0.1f;//óhÇÍïù
        shakeDuration = 1f;//óhÇÍÇÃí∑Ç≥
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowPosition = target.position;
        nowPosition.z = -10f;
        if (Input.GetKeyDown(KeyCode.Space) && time <= 0f)
        {
            // Space ÉLÅ[Ç™âüÇ≥ÇÍÅAÇ©Ç¬óhÇÍÇÃèàóùÇ™äJénÇ≥ÇÍÇƒÇ¢Ç»Ç¢èÍçá

            // åoâﬂéûä‘Çâ¡éZ
            time += Time.deltaTime;
        }

        // åoâﬂéûä‘Ç™óhÇÍÇÃä˙ä‘à»â∫ÇÃä‘ÅAÉJÉÅÉâÇóhÇÁÇµë±ÇØÇÈ
        if (time > 0f && time < shakeDuration)
        {
            // ÉâÉìÉ_ÉÄÇ»óhÇÍó ÇéÊìæ
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);

            nowPosition.x += shakeX;
            nowPosition.y += shakeY;
            transform.position = nowPosition;

            //// ÉJÉÅÉâÇÃà íuÇ…óhÇÍÇâ¡éZ
            //Vector3 newPosition = transform.position;
            //newPosition.x += shakeX;
            //newPosition.y += shakeY;
            //// ÉJÉÅÉâÇÃà íuÇçXêV
            //transform.position = newPosition;

            // åoâﬂéûä‘Çâ¡éZ
            time += Time.deltaTime;
        }
        else
        {
            // óhÇÍÇÃä˙ä‘Çí¥Ç¶ÇΩÇÁ time Ç 0 Ç…ñﬂÇ∑
            time = 0f;
            Vector3 resetPotision = transform.position;
            resetPotision.x = target.position.x;
            resetPotision.y = target.position.y;
            transform.position = resetPotision;
        }
    }
}
