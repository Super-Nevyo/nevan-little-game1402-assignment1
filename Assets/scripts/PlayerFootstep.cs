using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    [SerializeField] private ParticleSystem footStepEffect;
    [SerializeField] private string targetTag = "Ground";
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            footStepEffect.Play();
        }
    }
}
