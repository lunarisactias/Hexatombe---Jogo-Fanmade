using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void InstructionsButton()
    {
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }

    public void CloseInstructionsButton()
    {
        instructionsPanel.SetActive(false);
    }


}
