using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GUIManager : MonoBehaviour
{

    [SerializeField] private Animator TransitionAnimation;
    // Start is called before the first frame update
    void Start()
    {
        if (!TransitionAnimation)
        {
            TransitionAnimation =  GetComponentInChildren<Animator>();
        }
    }

    public void TransitionIn()
    {
        if (TransitionAnimation)
        {
            Debug.Log("FadeIn");
            TransitionAnimation.SetTrigger("FadeIn");
        }
    }

    public void TransitionOut()
    {
        if (TransitionAnimation)
        {
            Debug.Log("FadeOut");
            TransitionAnimation.SetTrigger("FadeOut");
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        TransitionIn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
