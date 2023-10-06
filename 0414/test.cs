using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int GenSpan;
    public float time;
    public int GenNum;

    public List<Transform> targets; // �����̃^�[�Q�b�g

    // Start is called before the first frame update
    void Start()
    {
        GenSpan = 3;
        time = 0;
        GenNum = 5;

        // �^�[�Q�b�g�̏�����
        targets = new List<Transform>();
        AssignPlayersAsTargets();
    }

    // Update is called once per frame
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
                    float xPlus = Random.Range(15.0f, 30.0f);
                    float xMinus = Random.Range(-15.0f, -30.0f);
                    float y = Random.Range(-30.0f, 30.0f);
                    int RL = Random.Range(0, 2);

                    // �^�[�Q�b�g�̈ʒu�ɓG�𐶐�
                    GameObject enemy = Instantiate(EnemyPrefab);
                    enemy.transform.position = new Vector3(randomTarget.position.x + (RL == 0 ? xMinus : xPlus),
                        randomTarget.position.y + y, 0);

                    // ���������G�̃^�[�Q�b�g��ݒ�
                    enemy.GetComponent<enemytest>().SetTarget(randomTarget);
                }
            }

            // �G�𐶐�������� time �� 0 �Ƀ��Z�b�g
            time = 0f;
        }
    }

    [ContextMenu("Assign Players as Targets")]
    private void AssignPlayersAsTargets()
    {
        // "Player" �^�O�����S�ẴI�u�W�F�N�g���擾
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // �^�[�Q�b�g���X�g���N���A���āA�V���Ƀ^�[�Q�b�g��ǉ�
        targets.Clear();
        foreach (GameObject player in players)
        {
            targets.Add(player.transform);
        }
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
