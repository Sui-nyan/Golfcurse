using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GUIManager : MonoBehaviour
{

    [SerializeField] private Animator TransitionAnimation;
    
    void Start()
    {
        if (!TransitionAnimation)
        {
            TransitionAnimation =  GetComponentInChildren<Animator>();
        }
    }
    /// <summary>
    /// scene transition fade from black to new scene
    /// </summary>
    public void TransitionIn()
    {
        if (TransitionAnimation)
        {
            Debug.Log("FadeIn");
            TransitionAnimation.SetTrigger("FadeIn");
        }
    }
    /// <summary>
    /// scene transition fade to black
    /// </summary>
    public void TransitionOut()
    {
        if (TransitionAnimation)
        {
            Debug.Log("FadeOut");
            TransitionAnimation.SetTrigger("FadeOut");
        }
    }
    /// <summary>
    /// Transition and load first level
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        TransitionIn();
    }
    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    ///  handles game over UI and returns to main menu
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        FindObjectOfType<SoundEffectManager>().PlaySound("GameOver");
        TransitionOut();
        yield return new WaitForSeconds(2.5f);
        
        SceneManager.LoadScene(0);
    }
}
