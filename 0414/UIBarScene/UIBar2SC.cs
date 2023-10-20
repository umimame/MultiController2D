using UnityEngine;

public class UIBar2SC : MonoBehaviour
{
    [SerializeField] private Parameter gauge;
    public GameObject frontBar;
    public bool increase;
    public float increaseAmount;
    public float decreaseAmount;

    private Vector3 initialScale; // �����̃X�P�[����ێ����邽�߂̕ϐ�

    private void Start()
    {
        gauge.Initialize();
        increase = true;
        increaseAmount = 0.0001f;
        decreaseAmount = 0.1f;

        // �����̃X�P�[�����擾
        initialScale = frontBar.transform.localScale;

        Debug.Log(initialScale.x);
    }

    private void Update()
    {
        if (increase)//�Q�[�W����
        {
            Debug.Log("������");
            // increaseAmount�Ɋ�Â��ăX�P�[���𒲐�����
            frontBar.transform.localScale += new Vector3(increaseAmount * initialScale.x, 0, 0);
            frontBar.transform.localScale = new Vector3(Mathf.Clamp(frontBar.transform.localScale.x, 0f, initialScale.x), initialScale.y, initialScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))//space��������Q�[�W����,�����Ă���P�b��Q�[�W�����Ăяo��
        {
            frontBar.transform.localScale -= new Vector3(decreaseAmount * initialScale.x, 0, 0);
            frontBar.transform.localScale = new Vector3(Mathf.Max(frontBar.transform.localScale.x, 0f), initialScale.y, initialScale.z);
            increase = false;
            Invoke("increaseSwitch", 1f);
        }
        if (Input.GetKeyDown(KeyCode.X))//�񕜐؂�ւ�
        {
            if (increase)
            {
                increase = false;
                Debug.Log("������~");
            }
            else
            {
                increase = true;
                Debug.Log("�����J�n");
            }
        }
    }
    private void increaseSwitch()
    {
        increase = true;
    }
}
