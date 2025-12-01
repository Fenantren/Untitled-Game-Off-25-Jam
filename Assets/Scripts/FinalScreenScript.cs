using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalScreenScript : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 2f;
    public void BackToMainMenu()
    {
        

        StartCoroutine(TransitionRoutine());

        SceneManager.LoadScene(0);
    }
    IEnumerator TransitionRoutine()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }
}
