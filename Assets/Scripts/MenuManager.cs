using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject instructionsButton;
    [SerializeField] private GameObject easyButton;
    [SerializeField] private GameObject mediumButton;
    [SerializeField] private GameObject hardButton;
    [SerializeField] private GameObject expertButton;
    [SerializeField] private GameObject difficultyText;
    public void PlayButton()
    {
        playButton.SetActive(false);
        instructionsButton.SetActive(false);
        easyButton.SetActive(true);
        mediumButton.SetActive(true);
        hardButton.SetActive(true);
        expertButton.SetActive(true);
        difficultyText.SetActive(true);
    }

    public void DifficultyButton(int difficultyIndex)
    {
        DifficultyManager.DifficultyLevels level = (DifficultyManager.DifficultyLevels)difficultyIndex;
        DifficultyManager.instance.SetDifficulty(level);
        SceneManager.LoadScene("GameScene");
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
