using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject inputManager;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI deliveryCompleteText;
    public Button restartButton;
    private InputSystemUIInputModule inputModule;

    private bool isGameActive;
    public int score;

    public float timeRemaining;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        inputModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
        isGameActive = true;
        timeRemaining = 120;
        //Instantiate(player, Vector3.zero, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if (timeRemaining > 0 && isGameActive == true)
        {
            timeRemaining -= Time.deltaTime;
            timer = Mathf.RoundToInt(timeRemaining);
            timerText.text = "" + timer;
        }

        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }

    }
    public void AddTime(int timeToAdd)
    {
        timeRemaining += timeToAdd;
    }
    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        if (score == 4)
        {
            DeliveryComplete();
        }
    }
    private void DeliveryComplete()
    {
        Cursor.lockState = CursorLockMode.None;

        deliveryCompleteText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        inputModule.enabled = true;
        isGameActive = false;
        inputManager.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        inputModule.enabled = true;
        isGameActive = false;
        inputManager.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        
    }
}
