using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] string[] tutorialTextArray;
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] int currentIndex = 0;

    public void Tutorial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           NextTextLine();
        }
    }

    public void NextTextLine()
    {
        if (currentIndex < tutorialTextArray.Length)
        {
            currentIndex++;
            tutorialText.text = tutorialTextArray[currentIndex];
        }
    }
}
