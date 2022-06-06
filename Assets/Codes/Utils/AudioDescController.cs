using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class AudioDescController : MonoBehaviour
{
   
    private GameControllerMainRoom controllerMainRoom;
    [Header("Configurações de Ambiente")]
   
    public GameObject player;
     public RigidbodyFirstPersonController controller;
    public GameObject gvrPointer;
  

    [Header("Configurações de Audio Descrição")]

    public int audioDesc;
    

    

    // Start is called before the first frame update
    void Start()
    {
       
        
        controllerMainRoom = FindObjectOfType(typeof(GameControllerMainRoom)) as GameControllerMainRoom;
        controller= FindObjectOfType(typeof(RigidbodyFirstPersonController)) as RigidbodyFirstPersonController; 
      

    }

    void Update()
    {

    }


    public void PlayAudioDescHistory()
    {
        //Posicionar, colocar audio, dizer que vai tocar
        
        controller.enabled = false;
        controllerMainRoom.audioClioSource.clip = controllerMainRoom.audioDesc[audioDesc];
        player.transform.position = controllerMainRoom.positionPlayer[audioDesc].transform.position;
        controllerMainRoom.isPlayingAudioDesc = true;
        print("Configurado pra tocar a descrição");

        //Tocar descricao
        controllerMainRoom.audioClioSource.PlayDelayed(1.5f);
        gvrPointer.SetActive(false);
    }



}
