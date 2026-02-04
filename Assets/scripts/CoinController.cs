using UnityEngine;

public class CoinController : MonoBehaviour, ICollectable
{
    public void OnCollect(Rigidbody2D rb)
    {
        Debug.Log("coin collected");
        Destroy(gameObject);
    }

}
