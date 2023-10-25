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
    [field: SerializeField] public SceneAsset scene;
    [field: SerializeField] public TextMeshProUGUI buttonText { get; set; }

    /// <summary>
    /// Ž©“®‚ÅButtonOnClick‚ð’Ç‰Á
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
        SceneManager.LoadScene(scene.name);
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