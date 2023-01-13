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
        TransitionAnimation = GetComponentInChildren<Animator>();
    }

    public void TransistionOut()
    {
        if(TransitionAnimation) TransitionAnimation.SetTrigger("changeScene");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        TransistionOut();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
