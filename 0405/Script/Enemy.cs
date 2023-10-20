using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : Chara
{
    public float detectionRange = 15.0f;  // �v���C���[�����m����͈�
    public float fireRate = 2.0f;  // �e�����p�x�i�b�j
    public GameObject bulletPrefab;  // �e�̃v���n�u
    public GameObject itemPrefab;
    public float bulletSpeed = 10.0f;
    public AudioClip shootSound; // �e������SE
    [SerializeField] private GameObject player1;  // �v���C���[��Transform�R���|�[�l���g
    [SerializeField] private GameObject player2;
    private object clone;
    private float nextFireTime; // ���ɒe��������
    private AudioSource audioSource; // AudioSource�R���|�[�l���g
    private Collider2D movementArea;
    public bool enemymove = true;

    protected override void Start()
    {
        base.Start();
        nextFireTime = Time.time + fireRate; // �ŏ��ɒe�������Ԃ�ݒ�
        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        movementArea = GetComponent<Collider2D>();
         player1 = GameObject.FindWithTag("Player01");
         player2 = GameObject.FindWithTag("Player02");
    }

    protected override void Update()
    {
        base.Update();
        if (player1 == null || player2 == null)
        {
            enemymove = false;
        }
        if (enemymove == true)
        {
            // �v���C���[�ƓG�̋������v�Z
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.transform.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.transform.position);

            // �v���C���[�����m�͈͓��ɂ��邩�`�F�b�N
            if (IsWithinDetectionRange(distanceToPlayer1) || IsWithinDetectionRange(distanceToPlayer2))
            {
                GameObject targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
                // �v���C���[�̕���������
                Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
                engine.rb.velocity = direction * speed.entity;

                // �e��������
                if (Time.time > nextFireTime)
                {
                    Shoot(targetPlayer);
                    audioSource.PlayOneShot(shootSound);
                    nextFireTime = Time.time + fireRate; // ���ɒe�������Ԃ��X�V
                }
            }
            else
            {
                engine.rb.velocity = Vector2.zero; // �v���C���[�����m�͈͊O�ɂ���ꍇ�͒�~
            }

            if (Input.GetKey("e"))
            {
                if (Random.value <= 0.3f)
                {
                    DropItem();
                }
                Destroy(gameObject);
            }
        }
        else if (enemymove == false)
        {
            engine.rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player02"))
        {
            Destroy(other.gameObject);
        }
    }

    protected virtual void UnderAttack(Collider2D co)
    {
        if (co.tag != transform.tag) // �Փː�̃^�O�����R�ƈقȂ�ꍇ
        {
            Debug.Log("UnderAttack");
            if (co.GetComponent<Bullet>())
            {
                Bullet coScript = co.GetComponent<Bullet>();
                hp.entity -= coScript.pow.entity;
                Debug.Log(hp.entity);
            }
        }
    }

    bool IsWithinDetectionRange(float distanceToPlayer)
    {
        // ���m�͈͓��ɂ��邩�ǂ������m�F
        return distanceToPlayer <= detectionRange;
    }

    void Shoot(GameObject targetPlayer)
    {
        // �e�𐶐����ăv���C���[�̕����ɔ���
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void DropItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }

}

