using System.Collections;
using UnityEngine;

public class Hitstop : MonoBehaviour
{
    private bool isHitstopActive = false;
    private float hitstopDuration = 0.2f; // ヒットストップの時間（秒）
    private float slowMotionDuration =5f; // ヒットストップ後のスローモーションの時間（秒）
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 例としてSpaceキーでヒットストップを発生させる
        {
            if (!isHitstopActive)
            {
                StartCoroutine(HitstopEffect());
            }
        }
    }

    IEnumerator HitstopEffect()
    {
        isHitstopActive = true;

        // ヒットストップ時の処理（ここにヒットストップ時の演出などを追加）

        // ヒットストップの実装
        Time.timeScale = 0f;

        // ヒットストップの時間だけ待機
        yield return new WaitForSecondsRealtime(hitstopDuration);

        // スローモーションの実装
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + slowMotionDuration)
        {
            float t = (Time.realtimeSinceStartup - startTime) / slowMotionDuration;
            Time.timeScale = Mathf.Lerp(0f, originalTimeScale, t);
            yield return null;
        }

        // ヒットストップ解除
        Time.timeScale = originalTimeScale;

        isHitstopActive = false;
    }
}
