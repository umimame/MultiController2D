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
        if(GetComponent<Camera>() != null ) // �A�^�b�`��̃I�u�W�F�N�g���J�����Ȃ�
        {
            nowPosition.z = -10f;
        }
        if ( time <= 0f)
        {
            // Space �L�[��������A���h��̏������J�n����Ă��Ȃ��ꍇ

            // �o�ߎ��Ԃ����Z
            time += Time.deltaTime;
        }

        // �o�ߎ��Ԃ��h��̊��Ԉȉ��̊ԁA�J������h�炵������
        if (time > 0f && time < shakeDuration && active == true)
        {
            // �����_���ȗh��ʂ��擾
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);

            nowPosition.x += shakeX;
            nowPosition.y += shakeY;
            transform.position = nowPosition;

            //// �J�����̈ʒu�ɗh������Z
            //Vector3 newPosition = transform.position;
            //newPosition.x += shakeX;
            //newPosition.y += shakeY;
            //// �J�����̈ʒu���X�V
            //transform.position = newPosition;

            // �o�ߎ��Ԃ����Z
            time += Time.deltaTime;
        }
        else
        {
            // �h��̊��Ԃ𒴂����� time �� 0 �ɖ߂�
            time = 0f;
            Vector3 resetPotision = transform.position;
            resetPotision.x = target.position.x;
            resetPotision.y = target.position.y;
            transform.position = resetPotision;
            active = false;
        }
    }

}
