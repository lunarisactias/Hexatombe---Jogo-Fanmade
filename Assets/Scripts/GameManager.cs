using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Game Settings")]
    public int sacrificios = 0;
    [SerializeField] private GameObject finalDoor;
    [SerializeField] private float gameDuration;

    [Header("UI")]
    [SerializeField] private GameObject[] characterButtons;
    [SerializeField] private Volume vignetteEffect;
    [SerializeField] private TextMeshProUGUI sacrificiosText;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Player Sprite")]
    [SerializeField] private SpriteRenderer playerSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Time.timeScale = 0f;

        switch (DifficultyManager.instance.selectedDifficulty)
        {
            case "Easy":
                gameDuration = 1801f;
                break;
            case "Medium":
                gameDuration = 1501f;
                break;
            case "Hard":
                gameDuration = 1201f;
                break;
            case "Expert":
                gameDuration = 901f;
                break;
        }

    }

    private void Update()
    {
        sacrificiosText.text = $"Sacrifícios: {sacrificios}/6";

        if (gameDuration > 0f)
        {
            gameDuration -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameDuration / 60f);
            int seconds = Mathf.FloorToInt(gameDuration % 60f);
            timerText.text = $"Tempo restante: {minutes:00}:{seconds:00}";
        }
        else
        {
            YouLose();
            timerText.text = "Tempo restante: 00:00";
        }
    }

    public void OpenFinalDoor()
    {
        if (sacrificios >= 6)
        {
            finalDoor.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        StartCoroutine(FadeOutVignette(1f));
        panelWin.SetActive(true);
    }

    public void YouLose()
    {
        Time.timeScale = 0f;
        StartCoroutine(FadeOutVignette(1f));
        panelLose.SetActive(true);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void ChooseSprite(Sprite sprite)
    {
        playerSprite.sprite = sprite;

        StartCoroutine(FadeInVignette(1.5f, 0.335f));

        foreach (GameObject button in characterButtons)
        {
            button.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private IEnumerator FadeInVignette(float duration, float targetIntensity)
    {
        Vignette vignette;

        if (vignetteEffect.profile.TryGet(out vignette))
        {
            vignette.active = true;

            float startValue = 0f;
            vignette.intensity.value = startValue;

            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / duration;

                vignette.intensity.value = Mathf.Lerp(startValue, targetIntensity, t);

                yield return null;
            }

            vignette.intensity.value = targetIntensity;
        }
    }

    private IEnumerator FadeOutVignette(float duration)
    {
        Vignette vignette;

        if (vignetteEffect.profile.TryGet(out vignette))
        {
            float startValue = vignette.intensity.value;
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;

                float t = timeElapsed / duration;
                vignette.intensity.value = Mathf.Lerp(startValue, 0f, t);

                yield return null;
            }

            vignette.intensity.value = 0f;
            vignette.active = false;
        }
    }
}
