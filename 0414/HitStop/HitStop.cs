using System.Collections;
using UnityEngine;

public class Hitstop : MonoBehaviour
{
    private bool isHitstopActive = false;
    private float hitstopDuration = 0.2f; // �q�b�g�X�g�b�v�̎��ԁi�b�j
    private float slowMotionDuration =5f; // �q�b�g�X�g�b�v��̃X���[���[�V�����̎��ԁi�b�j
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ��Ƃ���Space�L�[�Ńq�b�g�X�g�b�v�𔭐�������
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

        isHitstopActive = false;
    }
}
