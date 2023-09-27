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

    //public GameObject particleSystemObject; // ParticleSystem���A�^�b�`����Ă���GameObject��Inspector����ݒ肵�܂��B

    //public ParticleSystem particleSystemComponent; // ParticleSystem�R���|�[�l���g���i�[����ϐ�
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        hp = 10;
        AssignPlayerAsTarget();
        rigidBody = GetComponent<Rigidbody2D>();
        //bakuhatu = GameObject.Find("ParticleSystem");


        // GameObject����ParticleSystem�R���|�[�l���g�ւ̃A�N�Z�X���擾
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
        //Debug.Log("Player�ƐڐG���܂���"); 
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            //particleSystemComponent.Play();
            // ParticleSystem�R���|�[�l���g���擾�ł����ꍇ�A������s�����Ƃ��ł��܂�
            // ���Ƃ��΁A�Đ�����ꍇ�͎��̂悤�ɂ��܂��B
            //particleSystemComponent.Play();
            //Destroy(this.gameObject);

            //ParticleSystem newParticle = Instantiate(bakuhatu);
            //newParticle.transform.position = self.position;
            //newParticle.Play();

            // Destroy(this.gameObject);
            //// �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
            //ParticleSystem newParticle = Instantiate(particle);
            //// �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
            //newParticle.transform.position = this.transform.position;
            //// �p�[�e�B�N���𔭐�������B
            //newParticle.Play();
        }
        //�v���C���[�̒e�ɓ���������
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
