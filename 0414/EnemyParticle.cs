using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;

    /// <summary>
    /// �Փ˂�����
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D other)
    {      // �����������肪"Player"�^�O�������Ă�����
        if (other.gameObject.CompareTag("Player"))
        {
            // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
            ParticleSystem newParticle = Instantiate(particle);
            // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
            newParticle.transform.position = this.transform.position;
            // �p�[�e�B�N���𔭐�������B
            newParticle.Play();
            // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject���폜����B(�C��)
            // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
            //Destroy(newParticle.gameObject, 1.0f);
        }
    }
}
