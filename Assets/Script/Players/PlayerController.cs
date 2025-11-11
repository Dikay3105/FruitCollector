using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float maxSpeed = 30f;

    private float minX, maxX, halfWidth;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // gán animator

        float zDist = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, zDist));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, zDist));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        halfWidth = (sr != null) ? sr.bounds.size.x / 2f : 0.5f;

        minX = leftEdge.x + halfWidth;
        maxX = rightEdge.x - halfWidth;
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        // ✅ Không trớn – velocity thay đổi tức thì
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

        // ✅ Giữ trong màn hình
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;

        // ✅ Animation
        if (animator != null)
        {
            if (inputX < 0)       // di chuyển trái
            {
                animator.SetBool("moveL", true);
                animator.SetBool("moveR", false);
            }
            else if (inputX > 0)  // di chuyển phải
            {
                animator.SetBool("moveL", false);
                animator.SetBool("moveR", true);
            }
            else                  // đứng yên
            {
                animator.SetBool("moveL", false);
                animator.SetBool("moveR", false);
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + amount, 0, maxSpeed);
    }
}
