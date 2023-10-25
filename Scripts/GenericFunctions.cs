using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace My
{

    public class GenericFunctions : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    public static class AddFunction
    {

        /// <summary>
        /// Vector2���p�x(360�x)�ɕύX
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Vec2ToAngle(Vector2 v)
        {
            return Mathf.Repeat(Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg, 360);
        }

        public static float GetAngleByVec2(Vector3 start, Vector3 target)
        {
            float angle;
            Vector3 dt = start - target;
            angle = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

            return angle;
        }

        public static Vector3 CameraToMouse()
        {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
        }

        /// <summary>
        /// �^�O����v�f�ii�Ȃǁj�ɂ��ĕԂ�
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static int TagToArray(string tag)
        {
            switch (tag)
            {
                case "Player01":
                    return 0;
                case "Player02":
                    return 1;
            }
            return -1;
        }

        /// <summary>
        /// �v�f�ii�Ȃǁj���^�O���ɂ��ĕԂ�
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ArrayToTag(int array)
        {
            switch (array)
            {
                case 0:
                    return "Player01";
                case 1:
                    return "Player02";
            }
            return "0";
        }
    }

    public class KeyMapCallBack
    {
        public static KeyMap keyMap;

        public KeyMapCallBack()
        {
            keyMap = new KeyMap();
            keyMap.Enable();
        }

        public static Vector2 Move { get { return keyMap.Keybord.Move.ReadValue<Vector2>(); } }
        public static bool Inputting
        {
            get
            {
                if (Move == new Vector2(0, 0))
                {
                    return false;
                }
                return true;
            }
        }

        public static float DebugMode
        {
            get
            {
                Debug.Log("Debug");
                return keyMap.Keybord.Debug.ReadValue<float>();
            }
        }

        public static void Schema()
        {
            foreach (var device in InputSystem.devices)
            {
                Debug.Log(device);
            }
        }
    }
    /// <summary>
    /// �ړ��͈͂��~�`�ɐ���
    /// </summary>
    [Serializable]
    public class CircleClamp
    {

        [SerializeField] private GameObject center;
        [field: SerializeField] public GameObject moveObject { get; set; }
        [field: SerializeField] public float radius { get; private set; }

        public void Initialize()
        {
            moveObject.transform.position = center.transform.position;
        }
        public void Limit()
        {
            if (Vector2.Distance(moveObject.transform.position, center.transform.position) > radius)
            {
                Vector3 nor = moveObject.transform.position - center.transform.position;
                moveObject.transform.position = center.transform.position + nor.normalized * radius;
            }
        }
    }


    [Serializable]
    public class PresetByPlayerType<T>
    {
        [SerializeField] private int playerType;
        [field: SerializeField] public List<T> presets { get; set; }
        [ContextMenu("PresetsResize")]
        public void Initialize()
        {
            playerType = World.instance.PlayerTypes;
            presets = new List<T>(playerType);
        }
    }

    /// <summary>
    /// �Ԋu����N���X
    /// </summary>
    [Serializable]
    public class Interval
    {
        [field: SerializeField] public bool active { get; private set; }
        [SerializeField] private float interval;
        [SerializeField] private float time;
        [field: SerializeField] public bool timeOverride;

        /// <summary>
        /// �����ɂ͍ŏ�����g�p�ł��邩�ǂ������L�q����
        /// </summary>
        /// <param name="start"></param>
        public void Initialize(bool start, float interval = 0.0f)
        {
            if(interval != 0.0f) { this.interval = interval; }
            if (start == true)
            {
                time = interval;
            }
            else
            {
                time = 0.0f;
            }
            active = (time >= interval) ? true : false;
        }

        public void Update()
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                active = true;
            }
            else
            {
                active = false;
            }
        }

        /// <summary>
        /// �����́u���������̃��\�b�h�v
        /// </summary>
        /// <param name="action"></param>
        public void Launch(Action action)
        {
            if (active == true)
            {

                action();
            }
        }

        public void Reset()
        {
            time = 0.0f;
        }
    }

    [Serializable]
    public class Shake
    {
        [field: SerializeField] public Interval interval { get; set; }
        [field: SerializeField] public GameObject targetObj { get; set; }

        public void Initialize()
        {
            interval.Initialize(true);
        }

        public void Update()
        {
            if (interval.active == true)
            {
            }
            interval.Update();

        }

    }


    [Serializable]
    public class SpriteOrImage
    {
        [field: SerializeField] public SpriteRenderer[] sprites { get; set; }
        [field: SerializeField] public Image[] images { get; set; }
        [field: SerializeField] public TextMeshProUGUI[] texts { get; set; }

        /// <summary>
        /// ������SpriteRenderer�܂���Image���A�^�b�`���ꂽGameObject
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Initialize(GameObject obj)
        {
            sprites = obj.GetComponentsInChildren<SpriteRenderer>();
            images = obj.GetComponentsInChildren<Image>();
            texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            if(sprites.Length == 0 && images.Length == 0 && texts.Length == 0)
            {
                Debug.LogError("��������A�^�b�`����Ă��܂���");
            }
        }

        /// <summary>
        /// SpriteRenderer�܂���Image��Color��Ԃ�<br/>
        /// �������݂��Ȃ��ꍇ�̓G���[���b�Z�[�W���o����Color.white��Ԃ�
        /// </summary>
        public Color color
        {
            get
            {
                if (sprites.Length != 0)
                {

                    return sprites[0].color;
                }
                else if (images.Length != 0)
                {
                    return images[0].color;
                }

                Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                return Color.white;
            }
            set
            {
                if (sprites.Length != 0)
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.color = value;

                    }
                }
                else if (images.Length != 0)
                {
                    foreach (Image image in images)
                    {

                        image.color = value;
                    }
                }
                else if (texts.Length != 0)
                {
                    foreach (TextMeshProUGUI text in texts)
                    {

                        text.color = value;
                    }
                }
                else
                {
                    Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                }
            }
        }

        public float Alpha
        {
            get
            {
                if (sprites.Length != 0)
                {

                    return sprites[0].color.a;
                }
                else if (images.Length != 0)
                {
                    return images[0].color.a;
                }

                Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                return 0.0f;
            }
            set
            {
                if (sprites.Length != 0)
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, value);

                    }
                }
                else if (images.Length != 0)
                {
                    foreach (Image image in images)
                    {

                        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
                    }
                }
                else if (texts.Length != 0)
                {
                    foreach (TextMeshProUGUI text in texts)
                    {

                        text.color = new Color(text.color.r, text.color.g, text.color.b, value);
                    }
                }
                else
                {
                    Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                }
            }
        }

    }
    /// <summary>
    /// �w�肵���^���������AList�ɂ��ĕԂ��֐�
    /// </summary>
    public class TypeFinder : MonoBehaviour
    {
        [field: SerializeField] public FieldInfo[] fields { get; private set; }
        public List<T> GetAndInList<T>(Type type)
        {
            fields = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            List<T> variables = new List<T>();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(T))
                {
                    T variable = (T)field.GetValue(GetComponent(type));
                    variables.Add(variable);

                }
            }

            return variables;
        }
    }

    [Serializable] public class BarByParam
    {

        [SerializeField] private GameObject bar;
        [SerializeField] private float entity;
        [SerializeField] private float max;
        [SerializeField] private float ratio;
        [SerializeField] private bool warp;

        public void Update(float entity, float max)
        {
            this.entity = entity;
            this.max = max;
            ratio = entity / max;
            bar.transform.localScale = new Vector3(ratio, bar.transform.localScale.y);
        }
        public void Update(Parameter param)
        {
            entity = param.entity;
            max = param.max;
            ratio = entity / max;
            if (warp == true)
            {
                bar.transform.localScale = new Vector3(bar.transform.localScale.x, ratio);

            }
            else
            {
                bar.transform.localScale = new Vector3(ratio, bar.transform.localScale.y);


            }
        }
    }
}