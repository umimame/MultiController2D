using My;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneManager_CharaSelect : MonoBehaviour
{

    [SerializeField] private Image[] playerImg;
    [SerializeField] private Image infoImg;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private Button[] charaButtons;
    [SerializeField] private Button startButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button OKButton;
    [SerializeField] private GameObject[] belts;
    [SerializeField] private GameObject[] infos;
    [SerializeField] private int currentIndex;
    [SerializeField] private ControllerType currentType;
    [SerializeField] private SceneState state;
    [SerializeField] private FadeInstancer fadeUI;
    [SerializeField] private AudioSource BGM;
    private void Start()
    {
        BGM.Play();
        currentIndex = 0;
        for (int i = 0; i < infos.Length; ++i)
        {
            infos[i].SetActive(false);
        }
        BeltChange();
        charaButtons = buttonsParent.GetComponentsInChildren<Button>();
        state = SceneState.Start;
        fadeUI.Initialize();
    }

    private void Update()
    {

        if(Ready == true)
        {
            state = SceneState.Next;
            startButton.interactable = true;
            OKButton.interactable = false;
        }
        else
        {
            state = SceneState.Idol;
            startButton.interactable = false;
            OKButton.interactable = true;
        }

        if (AlreadySelectMouse)
        {
            OKButton.interactable = false;
        }

        if(currentIndex == 0)
        {
            cancelButton.interactable = false;
        }else if(currentIndex >= 1)
        {
            cancelButton.interactable = true;
        }
        switch(state)
        {
            case SceneState.Start:
                state = SceneState.Idol;
                break;
            case SceneState.Idol:
                
                break;
            case SceneState.Next:
                
                break;
        }
    }


    [EnumAction(typeof(ControllerType))]    // intを指定したenumにキャスト
    public void OnCharaButtonClick(int type)
    {
        currentType = (ControllerType)type;
        InfoChange();
        ImgChange();
    }

    public void OnOKButtonClick()
    {
        if (currentIndex < World.instance.PlayerTypes)
        {
            World.instance.controllerType[currentIndex] = currentType;
            ImgChange();
            currentIndex++;
            BeltChange();
            InfoChange();
        }
    }


    public void OnStartButtonClick()
    {
        fadeUI.Instance();
    }

    public void OnCancelButton()
    {
        currentIndex--;
        playerImg[currentIndex].sprite = null;
        BeltChange();
    }

    public void ImgChange()
    {
        switch (currentType)
        {
            case ControllerType.KeybordMouse:
                infoImg.sprite = playerImg[currentIndex].sprite = World.instance.player_KeybordMouse.GetComponentInChildren<Engine>().sprite.sprite;
                break;
            case ControllerType.Pad_DoubleStick:
                infoImg.sprite = playerImg[currentIndex].sprite = World.instance.player_Pad_DoubleStick.GetComponentInChildren<Engine>().sprite.sprite;
                break;
            case ControllerType.Pad_CardinalDirection:
                infoImg.sprite = playerImg[currentIndex].sprite = World.instance.player_Pad_CardinalDirectione.GetComponentInChildren<Engine>().sprite.sprite;
                break;
        }

    }

    public void BeltChange()
    {

        for (int i = 0; i < belts.Length; ++i)
        {
            if (i == currentIndex) { belts[i].SetActive(true); }
            else { belts[i].SetActive(false); }
        }
    }

    public void InfoChange()
    {
        for(int i = 0; i < infos.Length; ++i)
        {
            if (i == (int)currentType) { infos[i].SetActive(true); }
            else { infos[i].SetActive(false); }
        }
    }

    /// <summary>
    /// 全てのプレイヤーがキャラクターを選択したら
    /// </summary>
    public bool Ready
    {
        get { return currentIndex >= World.instance.PlayerTypes; }
    }

    public bool AlreadySelectMouse
    {
        get
        {
            if (currentType == ControllerType.KeybordMouse)
            {
                foreach (ControllerType ct in World.instance.controllerType)
                {
                    if (ct == ControllerType.KeybordMouse)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

    }
}
