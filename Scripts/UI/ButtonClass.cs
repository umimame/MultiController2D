using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClass : MonoBehaviour
{
    private Button button;
    [field: SerializeField] public string scene;
    [field: SerializeField] public TextMeshProUGUI buttonText { get; set; }

    /// <summary>
    /// 自動でButtonOnClickを追加
    /// </summary>
    public virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonOnClick);
        buttonText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public virtual void ButtonOnClick()
    {
        ButtonSelectNull();
    }

    public void ButtonSelectNull()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void CloseCanvas(GameObject closeCanvas)
    {
        Destroy(closeCanvas);
    }
    
    public void SceneLoad()
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}