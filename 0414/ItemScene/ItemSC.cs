using UnityEngine;

public class ItemSC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �Փ˂����I�u�W�F�N�g���v���C���[1�܂��̓v���C���[2�̃^�O�������ǂ������m�F
        if (other.CompareTag("Player01") || other.CompareTag("Player02"))
        {
            Debug.Log("������");
            // �Փ˂����玩�g��j��
            Destroy(gameObject);
        }
    }
}
