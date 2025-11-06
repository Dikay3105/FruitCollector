using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [HideInInspector]
    public float fallSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Nếu rơi khỏi màn hình dưới cùng thì hủy
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1f)
            Destroy(gameObject);
    }
}
