using UnityEngine;

public class UIBar2SC : MonoBehaviour
{
    [SerializeField] private Parameter gauge;
    public GameObject frontBar;
    public bool increase;
    public float increaseAmount;
    public float decreaseAmount;

    private Vector3 initialScale; // 初期のスケールを保持するための変数

    private void Start()
    {
        gauge.Initialize();
        increase = true;
        increaseAmount = 0.0001f;
        decreaseAmount = 0.1f;

        // 初期のスケールを取得
        initialScale = frontBar.transform.localScale;

        Debug.Log(initialScale.x);
    }

    private void Update()
    {
        if (increase)//ゲージ増加
        {
            Debug.Log("増加中");
            // increaseAmountに基づいてスケールを調整する
            frontBar.transform.localScale += new Vector3(increaseAmount * initialScale.x, 0, 0);
            frontBar.transform.localScale = new Vector3(Mathf.Clamp(frontBar.transform.localScale.x, 0f, initialScale.x), initialScale.y, initialScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))//space押したらゲージ減少,押してから１秒後ゲージ増加呼び出し
        {
            frontBar.transform.localScale -= new Vector3(decreaseAmount * initialScale.x, 0, 0);
            frontBar.transform.localScale = new Vector3(Mathf.Max(frontBar.transform.localScale.x, 0f), initialScale.y, initialScale.z);
            increase = false;
            Invoke("increaseSwitch", 1f);
        }
        if (Input.GetKeyDown(KeyCode.X))//回復切り替え
        {
            if (increase)
            {
                increase = false;
                Debug.Log("増加停止");
            }
            else
            {
                increase = true;
                Debug.Log("増加開始");
            }
        }
    }
    private void increaseSwitch()
    {
        increase = true;
    }
}
