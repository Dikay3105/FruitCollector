using UnityEngine;

public class BasketCollision : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PickupData data = other.GetComponent<PickupData>();
        if (data != null)
        {
            if (data.score != 0) gm.AddScore(data.score);
            if (data.healthImpact != 0) gm.ChangeHealth(data.healthImpact);

            Destroy(other.gameObject);
        }
        else
        {
            // fallback: nếu object có tag "Fruit" / "Bomb"
            if (other.CompareTag("Fruit"))
            {
                gm.AddScore(1);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Bomb"))
            {
                gm.ChangeHealth(-1);
                Destroy(other.gameObject);
            }
        }
    }
}
