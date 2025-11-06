using UnityEngine;

public class PickupData : MonoBehaviour
{
    public int score = 0;        // + điểm (ví dụ +1, +3)
    public int healthImpact = 0; // +1 để hồi máu, -1 để trừ máu

    public void Setup(int scoreValue, int healthDelta)
    {
        score = scoreValue;
        healthImpact = healthDelta;
    }
}
