using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSun : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        SceneManager.LoadScene("TheSun");
    }

    public void ReturnToScene1()
    {
        SceneManager.LoadScene("SceneLVL1");
    }
}
