using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Parameter gauge;
   public Slider slider; // Inspector��Slider���A�^�b�`����
   // public GameObject frontBar;
    private bool increase;
    [SerializeField] private  float increaseAmount;
    [SerializeField] private float decreaseAmount;
    private void Start()
    {
        gauge.Initialize();
        increase = true;
        increaseAmount = 0.0001f;
        decreaseAmount = 0.1f;
        //StartCoroutine(UISlider());
    }








    private void Update()
    {
        if (increase)
        {
            IncreaseGauge(increaseAmount);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("��������");
            DecreaseGauge(decreaseAmount);
        }
    }
    // �Q�[�W�𑝉������郁�\�b�h
    public void IncreaseGauge(float amount)
    {
        slider.value += amount;
        slider.value = Mathf.Min(1f, slider.value); // �ő�l��1�ɃN�����v
    }

    // �Q�[�W�����������郁�\�b�h
    public void DecreaseGauge(float amount)
    {
        slider.value -= amount;
        slider.value = Mathf.Max(0f, slider.value); // �ŏ��l��0�ɃN�����v
        increase = false;
        Invoke("IncreaseSwitch", 1f);

    }
    private void IncreaseSwitch()
    {
        increase = true;
    }



    //private IEnumerator UISlider()
    //{


    //    while (true)
    //    {


    //        // �X�y�[�X�L�[�������ꂽ��Q�[�W������������
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            Debug.Log("������");
    //            DecreaseGauge(0.1f);

    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(1f);

    //            IncreaseGauge(0.1f);
    //            Debug.Log("������");
    //        }
    //    }
    //}
}
