using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private Parameter gauge;
   public Slider slider; // InspectorでSliderをアタッチする
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
            Debug.Log("反応あり");
            DecreaseGauge(decreaseAmount);
        }
    }
    // ゲージを増加させるメソッド
    public void IncreaseGauge(float amount)
    {
        slider.value += amount;
        slider.value = Mathf.Min(1f, slider.value); // 最大値を1にクランプ
    }

    // ゲージを減少させるメソッド
    public void DecreaseGauge(float amount)
    {
        slider.value -= amount;
        slider.value = Mathf.Max(0f, slider.value); // 最小値を0にクランプ
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


    //        // スペースキーが押されたらゲージを減少させる
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            Debug.Log("減少中");
    //            DecreaseGauge(0.1f);

    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(1f);

    //            IncreaseGauge(0.1f);
    //            Debug.Log("増加中");
    //        }
    //    }
    //}
}
