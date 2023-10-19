using UnityEngine;

public class ItemSC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したオブジェクトがプレイヤー1またはプレイヤー2のタグを持つかどうかを確認
        if (other.CompareTag("Player01") || other.CompareTag("Player02"))
        {
            Debug.Log("当たり");
            // 衝突したら自身を破壊
            Destroy(gameObject);
        }
    }
}
