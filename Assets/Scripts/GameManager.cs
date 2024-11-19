using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;

    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private GameObject bestScoreGO;

    [SerializeField] private GameObject tapIcon;
    [SerializeField] private GameObject tapToJumpText;

    private int score;

    private int bestScore;
    public int Score => score;

    public int BestScore => bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();
        }
    }

    private void Start()
    {
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("Best Score", 0).ToString();
        UpdateBestScore();
        gameOver.SetActive(false);

    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        UpdateBestScore();



        playButton.SetActive(false);
        gameOver.SetActive(false);
        bestScoreGO.SetActive(false);
        tapIcon.SetActive(false);
        tapToJumpText.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    private void UpdateBestScore()
    {

        if (score > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score);
            bestScoreText.text = "Best Score: " + score.ToString();
        }
    }





    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        bestScoreGO.SetActive(true);
        tapIcon.SetActive(true);
        tapToJumpText.SetActive(true);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        UpdateBestScore();
    }



}
