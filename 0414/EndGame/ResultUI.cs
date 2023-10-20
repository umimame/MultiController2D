using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text Result;
    [SerializeField] private Text Who;
    [SerializeField] private Text PlayerName;
    [SerializeField] public int winnerPlayer { get; set; } = 0;
    [SerializeField] private Text HowManyKills;
    [SerializeField] private Text KillsCount;
    [SerializeField] private Text HowManyShots;
    [SerializeField] private Text ShotsCount;
    [SerializeField] private Text Time;
    [SerializeField] private Text TimeCount;
    [SerializeField] private Text ClickToTitle;

    [SerializeField] private AudioSource playerNameSE;
    [SerializeField] private AudioSource drumRoll;

    [SerializeField] private SceneAsset titleScene;
    
    private bool spaceKeyEntered = false; // Space �L�[�������ꂽ���ǂ����̃t���O
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayText());
        Debug.Log("playerNameSE�̃f�J��" + playerNameSE.volume);
        Debug.Log("drumRoll�̃f�J��" + drumRoll.volume);
    }

    // Update is called once per frame
    void Update()
    {
        KillsCount.color = Color.red;
        ShotsCount.color = Color.red;
        TimeCount.color = Color.red;
        // PlayerName.text�̓��e�ɂ���ĐF��ύX
        if (PlayerName.text.Contains("�v���C���[" + winnerPlayer))
        {
            PlayerName.color = UnityEngine.Color.green;
        }
        else if (PlayerName.text.Contains("�v���C���[" + winnerPlayer))
        {
            PlayerName.color = UnityEngine.Color.blue;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spaceKeyEntered = true;
        }
    }
    private IEnumerator DisplayText()//���ԂŃe�L�X�g�\��
    {
        yield return new WaitForSeconds(1f);
        Result.text = "��������";
        DrumRoll();

        yield return new WaitForSeconds(1f);
        Who.text = "���҂�...";

        yield return new WaitForSeconds(2f);
        drumRoll.Stop();

        PlayerName.text = "�v���C���[" + winnerPlayer; // �Ō�Ɏc�����v���C���[�̃^�O���Q��
        PlayerNameSE();

        yield return new WaitForSeconds(3f);
        HowManyKills.text = "�|���ꂽ�G���G�̐�:";
        KillsCount.text = "�E������";//�ϐ��Q��,�ϐ�������

        yield return new WaitForSeconds(1f);
        HowManyShots.text = "���˂��ꂽ�e�̐�:";
        ShotsCount.text = "���˂�����";//�ϐ�������

        yield return new WaitForSeconds(1f);
        Time.text = "��������:";
        TimeCount.text = "���Ԑ�����ϐ�";//�ϐ�������

        yield return new WaitForSeconds(0.5f);
        ClickToTitle.text = "SPACE�L�[�Ń^�C�g���ɖ߂�";
        StartCoroutine(BlinkText());

        yield return new WaitUntil(() => spaceKeyEntered);
        //Space�L�[�������ꂽ��̏���
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(titleScene.name);
        Debug.Log("�^�C�g���s���܂����`");
    }

    IEnumerator BlinkText()//�e�L�X�g�_�ŗp
    {
        while (true)
        {
            // �e�L�X�g��_�ł�����
            ClickToTitle.color = new Color(1f, 0f, 0f, 0f); // �A���t�@�l��0�ɓ����ɂȂ�
            yield return new WaitForSeconds(0.5f); // 0.5�b�҂�

            ClickToTitle.color = new Color(1f, 0f, 0f, 1f); // �A���t�@�l��1�ɓ�������
            yield return new WaitForSeconds(0.5f); // 0.5�b�҂�
        }
    }
    void PlayerNameSE()
    {
        playerNameSE.PlayOneShot(playerNameSE.clip);
    }
    void DrumRoll()
    {
        drumRoll.PlayOneShot(drumRoll.clip);
    }
}
