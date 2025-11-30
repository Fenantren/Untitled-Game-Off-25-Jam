using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] string[] tutorialTextArray;
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] int currentIndex = 0;
    [SerializeField] GameObject enterText;
    [SerializeField] GameObject waveTower;


    private void Update()
    {
        if(currentIndex == 7 && !waveTower)
        {
            
            currentIndex = 8;
            tutorialText.text = tutorialTextArray[currentIndex];    
        }
    }
    public void Tutorial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           NextTextLine();
        }
    }

    public void NextTextLine()
    {
        if (currentIndex < 7)
        {
            currentIndex++;
            tutorialText.text = tutorialTextArray[currentIndex];
        }
        
        
        if (currentIndex == 7)
        {
            waveTower.SetActive(true);
            enterText.SetActive(false);
        }
    }
}
