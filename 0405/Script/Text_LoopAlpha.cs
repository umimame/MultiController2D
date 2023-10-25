using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Text_LoopAlpha : MonoBehaviour
{
    [SerializeField] private float alphaChangeSpeed;
    [SerializeField] private TextMeshProUGUI text;
    private float time;
    [SerializeField] private Button parent;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        parent = transform.parent.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            if (parent.interactable == false)
            {
                text.color = new Color(text.color.r,text.color.g,text.color.b, 0.2f);
                return;
            }
        }
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime;
        color.a = Mathf.Abs(Mathf.Sin(alphaChangeSpeed * time));    // alpha�l��0�����ɂȂ�Ȃ��悤�ɂ���
        return color;
    }
}
