using UnityEngine;

public class BoosterController : MonoBehaviour, ICollectable
{
    public void OnCollect(Rigidbody2D rb)
    {
        Debug.Log("Boosted");
        rb.linearVelocityX += 20f * (rb.linearVelocityX / Mathf.Abs(rb.linearVelocityX));
    }
}
