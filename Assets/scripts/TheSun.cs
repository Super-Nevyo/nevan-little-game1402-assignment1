using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSun : MonoBehaviour
{
    // touching the sun brings you to the "you win" screen
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        SceneManager.LoadScene("TheSun");
    }
    // this is attached to the button from the sun screen to play again
    public void ReturnToScene1()
    {
        SceneManager.LoadScene("SceneLVL1");
    }
}
