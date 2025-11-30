using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    
    [SerializeField] GameObject mainMenuBox;
    [SerializeField] GameObject tutorialBox;
    [SerializeField] GameObject controlsBox;
    [SerializeField] GameObject creditsBox;

    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 3f;

    public void TutorialWindowChoice()
    {
        mainMenuBox.SetActive(false);
        tutorialBox.SetActive(true);
    }

    public void BackFromTutorial()
    {
        mainMenuBox.SetActive(true);
        tutorialBox.SetActive(false);

    }
    public void StartTutorial()
    {
        StartCoroutine(TransitionRoutine());

        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        StartCoroutine(TransitionRoutine());
        SceneManager.LoadScene(2);
    }

    IEnumerator TransitionRoutine()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    public void ControlsMenu()
    {
        controlsBox.SetActive(true);
        mainMenuBox.SetActive(false);
    }

    public void BackFromControls()
    {
        controlsBox.SetActive(false);
        mainMenuBox.SetActive(true);
    }
    public void CreditsMenu()
    {
        creditsBox.SetActive(true);
        mainMenuBox.SetActive(false);
    }

    public void BackFromCredits()
    {
        creditsBox.SetActive(false);
        mainMenuBox.SetActive(true);
    }



    

    
}
