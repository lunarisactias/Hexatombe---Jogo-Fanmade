using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;
    public string selectedDifficulty;

    public enum DifficultyLevels
    {
        Easy,
        Medium,
        Hard,
        Expert
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(DifficultyLevels difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevels.Easy:
                selectedDifficulty = "Easy";
                break;
            case DifficultyLevels.Medium:
                selectedDifficulty = "Medium";
                break;
            case DifficultyLevels.Hard:
                selectedDifficulty = "Hard";
                break;
            case DifficultyLevels.Expert:
                selectedDifficulty = "Expert";
                break;
        }
    }
}
