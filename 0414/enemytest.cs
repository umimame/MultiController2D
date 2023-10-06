using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytest : MonoBehaviour
{
    private float speed;
    public int hp;
    [SerializeField] private Transform self;
    private Transform target;
    // �������ʉ�
    public AudioClip explosionSE;
    // �ړ����̉�
    public AudioSource moveSE;
    // �����G�t�F�N�g
    private EnemyParticle enemyParticle;

    // Start is called before the first frame update
    void Start()
    {
        //speed = Random.Range(10.0f, 30.0f);
        speed = 10.0f;
        hp = 10;
        MoveSE();
        enemyParticle = GetComponent<EnemyParticle>();
        // �����^�[�Q�b�g��ݒ�
        SetInitialTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.G))
        {
            DestroySelf();
        }

        // �V�����^�[�Q�b�g���߂��ɂ��邩�m�F
        CheckForNewTarget();
    }

    void Move()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - self.position);
            self.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            self.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player �^�O�����I�u�W�F�N�g�ɓ���������
        if (other.gameObject.CompareTag("Player"))
        {
            DestroySelf();
        }
    }

    // �^�[�Q�b�g��ݒ肷�郁�\�b�h
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void DestroySelf()
    {
        Debug.Log("�����`���񂾃��S");
        ExplosionSE();
        enemyParticle.TriggerEnemyParticle();
        Destroy(this.gameObject);
    }

    void MoveSE()
    {
        moveSE = GetComponent<AudioSource>();

        if (moveSE != null)
        {
            moveSE.Play();
        }
    }

    void ExplosionSE()
    {
        AudioSource.PlayClipAtPoint(explosionSE, this.transform.position);
    }

    // 2�̃v���C���[�Ƃ̋������r���āA�߂������^�[�Q�b�g�ɐݒ�
    void SetInitialTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, players[0].transform.position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, players[1].transform.position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(players[0].transform);
            }
            else
            {
                SetTarget(players[1].transform);
            }
        }
        else if (players.Length == 1)
        {
            SetTarget(players[0].transform);
        }
    }

    // 2�̃v���C���[�Ƃ̋������r���āA�߂������^�[�Q�b�g�ɐݒ�
    void CheckForNewTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, players[0].transform.position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, players[1].transform.position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(players[0].transform);
            }
            else
            {
                SetTarget(players[1].transform);
            }
        }
        else if (players.Length == 1)
        {
            SetTarget(players[0].transform);
        }
    }
}
