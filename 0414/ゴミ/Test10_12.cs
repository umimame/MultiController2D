using System.Collections;
using UnityEngine;

public class EnemyApproach : MonoBehaviour
{
    public float initialOrbitRadius = 50.0f; // �����̎��񔼌a
    public float minOrbitRadius = 1.0f; // �ŏ��̎��񔼌a
    public float orbitSpeed = 45.0f; // �^�[�Q�b�g�����鑬�x
    public float approachSpeed = 2.0f; // �^�[�Q�b�g�ɋ߂Â����x
    public float timeToReachMinRadius = 15.0f; // �ŏ����a�ɓ��B����܂ł̎���
    public Transform target; // �^�[�Q�b�g

    private float currentOrbitRadius;

    private void Start()
    {
        currentOrbitRadius = initialOrbitRadius;
        StartCoroutine(ApproachTarget());
    }
    private void Update()
    {
        Debug.Log(currentOrbitRadius);
    }
    IEnumerator ApproachTarget()
    {
        float timer = 0.0f;

        while (timer < timeToReachMinRadius)
        {
            if (target != null)
            {
                // �^�[�Q�b�g�̎�������ʒu���v�Z
                Vector3 orbitPosition = target.position + (Quaternion.Euler(0, 0, orbitSpeed * Time.time) * Vector3.right * currentOrbitRadius);

                // �^�[�Q�b�g�̎�������Ȃ��珙�X�ɋ߂Â�
                transform.position = Vector3.MoveTowards(transform.position, orbitPosition, approachSpeed * Time.deltaTime);

                // ���񔼌a�����X�Ɍ���
                currentOrbitRadius = Mathf.Lerp(initialOrbitRadius, minOrbitRadius, timer / timeToReachMinRadius);

                timer += Time.deltaTime;
            }

            yield return null;
        }

        // �ŏ����a�ɓ��B��͈��̋����Ŏ��񂵑�����
        while (true)
        {
            if (target != null)
            {
                // �^�[�Q�b�g�̎�������ʒu���v�Z
                Vector3 orbitPosition = target.position + (Quaternion.Euler(0, 0, orbitSpeed * Time.time) * Vector3.right * currentOrbitRadius);

                // �^�[�Q�b�g�̎�������Ȃ��珙�X�ɋ߂Â�
                transform.position = Vector3.MoveTowards(transform.position, orbitPosition, approachSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
