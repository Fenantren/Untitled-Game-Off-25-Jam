using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header ("UI elements")]
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Animator transition;

    

    [SerializeField] GameObject youWinText;
    [SerializeField] GameObject nextLevelPortal;

    [SerializeField] float transitionTime = 1f;
    
    
    
    PlayerHealth player;
    Animator playerAnimator;
    int enemiesLeft = 0;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        playerAnimator = player.GetComponent<Animator>();
       
        
    }
    const string ENEMIES_LEFT_STRING = "Enemies Left : ";
    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
            nextLevelPortal.SetActive(true);
        }
    }

    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelRoutine());

    }

    IEnumerator LoadNextLevelRoutine()
    {
        playerAnimator.enabled = true;
        
        yield return new WaitForSeconds(1f);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);




    }


    
    public void PauseScreen()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        //transition screen
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if ( context.performed)
        {
            if (pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;

            }
            else if (pauseMenu.activeInHierarchy == true)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }

        }
        
    }


}
