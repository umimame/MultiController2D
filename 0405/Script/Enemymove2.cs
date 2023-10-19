using Unity.VisualScripting;
using UnityEngine;

public class Enemymove2 : MonoBehaviour
{
    public float speed = 3.0f;  // �G�̈ړ����x
    public float detectionRange = 15.0f;  // �v���C���[�����m����͈�
    public float fireRate = 2.0f;  // �e�����p�x�i�b�j
    public GameObject bulletPrefab;  // �e�̃v���n�u
    public float bulletSpeed = 10.0f;
    public AudioClip shootSound; // �e������SE
    private Transform player1;  // �v���C���[��Transform�R���|�[�l���g
    private Transform player2;
    private Rigidbody2D rb;
    private float nextFireTime; // ���ɒe��������
    private AudioSource audioSource; // AudioSource�R���|�[�l���g
    private Collider2D movementArea;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player01").transform;  // �v���C���[��Transform���擾
        player2 = GameObject.FindGameObjectWithTag("Player02").transform;
        rb = GetComponent <Rigidbody2D>();
        nextFireTime = Time.time + fireRate; // �ŏ��ɒe�������Ԃ�ݒ�
        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        movementArea = GetComponent<Collider2D>();
    }

    void Update()
    {
        // �v���C���[�ƓG�̋������v�Z
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        // �v���C���[�����m�͈͓��ɂ��邩�`�F�b�N
        if (IsWithinDetectionRange(distanceToPlayer1) || IsWithinDetectionRange(distanceToPlayer2))
        {
            Transform targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
            // �v���C���[�̕���������
            Vector2 direction = (targetPlayer.position - transform.position).normalized;
            rb.velocity = direction * speed;

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
            rb.velocity = Vector2.zero; // �v���C���[�����m�͈͊O�ɂ���ꍇ�͒�~
        }
        if (Input.GetKey("e"))
        {
            Destroy(gameObject);
        }
    }

    bool IsWithinDetectionRange(float distanceToPlayer)
    {
        // ���m�͈͓��ɂ��邩�ǂ������m�F
        return distanceToPlayer <= detectionRange;
    }

    void Shoot(Transform targetPlayer)
    {
        // �e�𐶐����ăv���C���[�̕����ɔ���
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}