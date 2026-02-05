using UnityEngine;

public class BoosterController : MonoBehaviour, ICollectable
{
    // takes the rb from the player and adds 20 velocity in the direction they are moving
    public void OnCollect(Rigidbody2D rb)
    {
        if (rb.linearVelocityX == 0) return;
        Debug.Log("Boosted");
        rb.linearVelocityX += 20f * (rb.linearVelocityX / Mathf.Abs(rb.linearVelocityX)); // x / |x| is equal to positive 1 if x>1 and negative 1 if x<1, it is effectively a normalization
    }
}
