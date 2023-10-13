using System.Collections;
using UnityEngine;

public class EnemyApproach : MonoBehaviour
{
    public float initialOrbitRadius = 50.0f; // 初期の周回半径
    public float minOrbitRadius = 1.0f; // 最小の周回半径
    public float orbitSpeed = 45.0f; // ターゲットを周る速度
    public float approachSpeed = 2.0f; // ターゲットに近づく速度
    public float timeToReachMinRadius = 15.0f; // 最小半径に到達するまでの時間
    public Transform target; // ターゲット

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
                // ターゲットの周りを回る位置を計算
                Vector3 orbitPosition = target.position + (Quaternion.Euler(0, 0, orbitSpeed * Time.time) * Vector3.right * currentOrbitRadius);

                // ターゲットの周りを回りながら徐々に近づく
                transform.position = Vector3.MoveTowards(transform.position, orbitPosition, approachSpeed * Time.deltaTime);

                // 周回半径を徐々に減少
                currentOrbitRadius = Mathf.Lerp(initialOrbitRadius, minOrbitRadius, timer / timeToReachMinRadius);

                timer += Time.deltaTime;
            }

            yield return null;
        }

        // 最小半径に到達後は一定の距離で周回し続ける
        while (true)
        {
            if (target != null)
            {
                // ターゲットの周りを回る位置を計算
                Vector3 orbitPosition = target.position + (Quaternion.Euler(0, 0, orbitSpeed * Time.time) * Vector3.right * currentOrbitRadius);

                // ターゲットの周りを回りながら徐々に近づく
                transform.position = Vector3.MoveTowards(transform.position, orbitPosition, approachSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
