using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    void GoBackToTitle()
    {
        SceneManager.LoadScene(0);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Esc");
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                GoBackToTitle();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                QuitGame();
            }
        }
    }
}
