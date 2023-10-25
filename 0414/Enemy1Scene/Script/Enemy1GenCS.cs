using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1GenCS : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField][Tooltip("�G�̐����Ԋu����")] private int GenSpan;
    [SerializeField][Tooltip("�G����������Ă���̎���(�������ꂽ�烊�Z�b�g)")] private float time;
    [SerializeField][Tooltip("��������G�̐�")] private int GenNum;

    [SerializeField] private List<Transform> targets; // �����̃^�[�Q�b�g

    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 10;

        // �^�[�Q�b�g�̏�����
        targets = new List<Transform>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= GenSpan)
        {
            for (int i = 0; i < GenNum; i++)
            {
                // �����_���Ƀ^�[�Q�b�g��I��
                Transform randomTarget = GetRandomTarget();

                if (randomTarget != null)
                {
                    float xPlus = Random.Range(30.0f, 50.0f);
                    float xMinus = Random.Range(-30.0f, -50.0f);
                    float y = Random.Range(-50.0f, 50.0f);
                    int RL = Random.Range(0, 2);

                    // �^�[�Q�b�g�̈ʒu�ɓG�𐶐�
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(randomTarget.position.x + (RL == 0 ? xMinus : xPlus),
                        randomTarget.position.y + y, 0);
                    //Debug.Log("���A�Q��I");
                    // ���������G�̃^�[�Q�b�g��ݒ�
                    enemy.GetComponent<Enemy1CS>().SetTarget(randomTarget);
                }
            }

            // �G�𐶐�������� time �� 0 �Ƀ��Z�b�g
            time = 0f;
        }
        //Debug.Log("Time: " + time);
    }

    [ContextMenu("Assign Players as Targets")]
    public void AssignPlayersAsTargets()
    {
        Debug.Log("Assign");
        // "Player01" �܂��� "Player02" �^�O�����S�ẴI�u�W�F�N�g���擾
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player01");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player02");

        // �^�[�Q�b�g���X�g���N���A���āA�V���Ƀ^�[�Q�b�g��ǉ�
        targets.Clear();

        // �v���C���[1�̔z������X�g�ɒǉ�
        foreach (GameObject player in players1)
        {
            if(player.TryGetComponent(out Rigidbody2D rb))
            {
                targets.Add(player.transform);
            }
        }

        // �v���C���[2�̔z������X�g�ɒǉ�
        foreach (GameObject player in players2)
        {

            if (player.TryGetComponent(out Rigidbody2D rb))
            {

                targets.Add(player.transform);
            }
        }

        Enemy1CS clone = EnemyPrefab.GetComponent<Enemy1CS>();
        clone.players1 = players1;
        clone.players2 = players2;
    }
    // �����_���Ƀ^�[�Q�b�g��I������
    private Transform GetRandomTarget()
    {
        if (targets.Count == 0)
            return null;

        // �����_���ɃC���f�b�N�X��I��
        int randomIndex = Random.Range(0, targets.Count);

        return targets[randomIndex];
    }
}
