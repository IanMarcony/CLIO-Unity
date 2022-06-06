using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopAnimation : MonoBehaviour
{

    public Animator anim;
    public bool isStopAnimation;
    private GameController gameController;


    void Start()
    {

        anim = GetComponent<Animator>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "stopAnim")
        {
                //Acabou tour;
            anim.speed = 0;
            print("PAROU");
             SceneManager.LoadScene("MainRoom");    
        }

        if (other.gameObject.tag == "pergunta")
        {
            //Iniciou uma pergunta
            anim.speed = 0;
            print("Parou pra responder");
            isStopAnimation = true;
            gameController.pontosParada = other.gameObject;

        }
    }

}
