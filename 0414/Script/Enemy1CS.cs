using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy1CS : MonoBehaviour
{
    private float speed;
    [SerializeField][Tooltip("���g��Transform")] private Transform self;
    [Tooltip("target��Transform")] private Transform target;
    // �������ʉ�
    [SerializeField][Tooltip("���g���j�󂳂ꂽ�Ƃ��̉�")] private AudioClip explosionSE;
    // �ړ����̉�
    [SerializeField][Tooltip("���g���ړ����̉�")] private AudioSource moveSE;
    // �����G�t�F�N�g
    private EnemyParticle enemyParticle;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(10.0f, 30.0f);
        //speed = 20.0f;
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
        if (other.transform == target)
        {
            //Debug.Log("����");
            DestroySelf();
        }
    }
    // �^�[�Q�b�g��ݒ肷��
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    //���g���E��
    void DestroySelf()
    {
        //Debug.Log("�����`���񂾃��S");
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
    // �������ꂽ�Ƃ���2�̃v���C���[�Ƃ̋������r���āA�߂������^�[�Q�b�g�ɐݒ�
    void SetInitialTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player01");
        GameObject[] players02 = GameObject.FindGameObjectsWithTag("Player02");

        List<Transform> allPlayers = new List<Transform>();

        allPlayers.AddRange(players.Select(player => player.transform));
        allPlayers.AddRange(players02.Select(player => player.transform));

        if (allPlayers.Count >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, allPlayers[0].position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, allPlayers[1].position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(allPlayers[0]);
            }
            else
            {
                SetTarget(allPlayers[1]);
            }
        }
        else if (allPlayers.Count == 1)
        {
            SetTarget(allPlayers[0]);
        }
    }
    // ��Ɂi�ړ������j2�̃v���C���[�Ƃ̋������r���āA�߂������^�[�Q�b�g�ɐݒ�
    void CheckForNewTarget()
    {
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player01");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player02");

        List<Transform> allPlayers = new List<Transform>();

        allPlayers.AddRange(players1.Select(player => player.transform));
        allPlayers.AddRange(players2.Select(player => player.transform));

        if (allPlayers.Count >= 2)
        {
            float distanceToPlayer1 = Vector3.Distance(transform.position, allPlayers[0].position);
            float distanceToPlayer2 = Vector3.Distance(transform.position, allPlayers[1].position);

            if (distanceToPlayer1 < distanceToPlayer2)
            {
                SetTarget(allPlayers[0]);
            }
            else
            {
                SetTarget(allPlayers[1]);
            }
        }
        else if (allPlayers.Count == 1)
        {
            SetTarget(allPlayers[0]);
        }
    }
}
