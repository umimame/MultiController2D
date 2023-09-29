using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    public GameObject player;
    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;

    //��b���Ƃɒe�𔭎˂��邽�߂̂���
    private float targetTime = 1.0f;
    private float currentTime = 0;



    // Update is called once per frame
    void Update()
    {
        //��b�o���Ƃɒe�𔭎˂���
        currentTime += Time.deltaTime;
        if (targetTime < currentTime)
        {
            currentTime = 0;
            //�G�̍��W��ϐ�pos�ɕۑ�
            var pos = this.gameObject.transform.position;
            //�e�̃v���n�u���쐬
            var t = Instantiate(tama) as GameObject;
            //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            t.transform.position = pos;
            //�G����v���C���[�Ɍ������x�N�g��������
            //�v���C���[�̈ʒu����G�̈ʒu�i�e�̈ʒu�j������
            Vector2 vec = player.transform.position - pos;
            //�e��RigidBody2D�R���|�l���g��velocity�ɐ�����߂��x�N�g�������ė͂�������
            t.GetComponent<Rigidbody2D>().velocity = vec;
        }
    }
}