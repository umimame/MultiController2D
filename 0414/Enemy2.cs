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
    // �������ʉ�


    public AudioClip explosionSE;

    //�ړ����̉�
    public AudioSource moveSE;
    //public AudioClip moveSE2;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15f;
        hp = 10;
        AssignPlayerAsTarget();
        MoveSE();
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
                DestroySelf();
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
        //Debug.Log("Player�ƐڐG���܂���"); 
        if (other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
        //�v���C���[�̒e�ɓ���������
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
        //Player�^�O�����I�u�W�F�N�g���^�[�Q�b�g�ɐݒ肷��
        target = GameObject.Find("Player").transform;
    }
    void DestroySelf()
    {
        ExplosionSE();
        //���g��j��
        Destroy(this.gameObject);
    }
    void MoveSE()
    {
        // AudioSource�R���|�[�l���g���擾
        moveSE = GetComponent<AudioSource>();

        // �I�[�f�B�I���Đ��\�ȏꍇ
        if (moveSE != null)
        {
            // �I�[�f�B�I���Đ�
            moveSE.Play();
        }
    }
    void ExplosionSE()
    {
        // �I�[�f�B�I���Đ�
        AudioSource.PlayClipAtPoint(explosionSE, this.transform.position);
    }
}
