using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace My
{
    /// <summary>
    /// �I�u�W�F�N�g�𓮂�����{�N���X<br\>
    /// ���ꂼ��C���X�y�N�^����K�v�ȃR���|�[�l���g���A�^�b�`����
    /// </summary>
    [Serializable]
    public class Engine : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D rb { get; set; }
        [field: SerializeField] public Vector3 velocityPlan { get; set; }
        [field: SerializeField] public Collider2D coll { get; set; }
        [field: SerializeField] public SpriteRenderer sprite { get; set; }
        [field: SerializeField] public SpriteRenderer aimCircle { get; set; }
        [field: SerializeField] public float time { get; set; }
        private float angle;
        private float difference;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            velocityPlan = new Vector2(0.0f, 0.0f);
            coll = GetComponent<Collider2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            time = 0.0f;

            float angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
            float difference = transform.eulerAngles.z - angle;
        }
        private void Update()
        {

        }

        /// <summary>
        /// RigidBody�𓮂���
        /// �A�^�b�`�����I�u�W�F�N�g��Update�̍Ō�ɌĂ�
        /// </summary>
        public void VelocityResult()
        {
            angle = AddFunction.Vec2ToAngle(velocityPlan.normalized);
            difference = transform.eulerAngles.z - angle;
            rb.velocity = velocityPlan;

        }
        public void LookAtVec(Vector3 obj)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, obj - transform.position);
        }

        /// <summary>
        /// �����蔻��̍폜
        /// </summary>
        public void CollDisabled()
        {
            coll.enabled = false;
            rb.isKinematic = true;
        }

    }
}