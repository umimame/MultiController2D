using System.Collections;
using UnityEngine;
using My;
public class Hitstop : MonoBehaviour
{
    
    public SceneState state { get; private set; }
    private float hitstopDuration = 0.2f; // �q�b�g�X�g�b�v�̎��ԁi�b�j
    private float slowMotionDuration =3.0f; // �q�b�g�X�g�b�v��̃X���[���[�V�����̎��ԁi�b�j
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (state == SceneState.Start)
        {
            StartCoroutine(HitstopEffect());
        }
        
    }

    IEnumerator HitstopEffect()
    {
        state  = SceneState.Idol;

        // �q�b�g�X�g�b�v���̏����i�����Ƀq�b�g�X�g�b�v���̉��o�Ȃǂ�ǉ��j

        // �q�b�g�X�g�b�v�̎���
        Time.timeScale = 0f;

        // �q�b�g�X�g�b�v�̎��Ԃ����ҋ@
        yield return new WaitForSecondsRealtime(hitstopDuration);

        // �X���[���[�V�����̎���
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + slowMotionDuration)
        {
            float t = (Time.realtimeSinceStartup - startTime) / slowMotionDuration;
            Time.timeScale = Mathf.Lerp(0f, originalTimeScale, t);
            yield return null;
        }

        // �q�b�g�X�g�b�v����
        Time.timeScale = originalTimeScale;
        state = SceneState.Next;
    }
}
