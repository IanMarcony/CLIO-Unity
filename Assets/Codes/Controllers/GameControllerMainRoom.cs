using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class GameControllerMainRoom : MonoBehaviour
{

  private KeyCode[] cheatCode = { KeyCode.Joystick1Button2, KeyCode.Joystick1Button2, KeyCode.Joystick1Button2 };
  private KeyCode[] cheatCode2 = { KeyCode.Z, KeyCode.Z, KeyCode.Z };
  private int indexLock = 0;
  private bool doingAnything;
  private RigidbodyFirstPersonController controller;
  private AudioDescController controllerAudio;
  private bool iconClioChanged;

  [Header("Image Clio")]
  public Image clioIcon;

  [Header("Audio Clio in World")]
  public AudioSource audioClioSource;

  [Header("Tutorial")]
  public GameObject painelControle;
  public GameObject paredePlayer;
  public AudioClip audioCorrect;
  public AudioClip[] audioIntrucoesPlayerTutorial;
  public TextMeshProUGUI textInstrucion;
  public int stepInstrucionController;
  public int turnInstrucion;
  public bool isTutorialPlaying, canContinueTutorial = true;
  public bool lockAction;

  [Header("Check to audio intro")]
  public AudioClip audioToPlay;
  public bool isIntroPlaying;

  [Header("Tour")]
  public bool isTourPlaying;
  public bool isInTour;
  public AudioClip audioTour;
  public float timing;
  public int stepTourPosition;
  public GameObject[] positionTour;
  public GameObject player;


  [Header("Database Descriptions Audios")]

  public AudioClip[] audioDesc;
  public GameObject[] positionPlayer;
  public bool isPlayingAudioDesc;



  // Start is called before the first frame update
  void Start()
  {
    player.transform.position = positionTour[2].transform.position;
    audioClioSource = GetComponent<AudioSource>();
    audioClioSource.clip = audioToPlay;
    painelControle.SetActive(false);
    controller = FindObjectOfType(typeof(RigidbodyFirstPersonController)) as RigidbodyFirstPersonController;
    controllerAudio = FindObjectOfType(typeof(AudioDescController)) as AudioDescController;

    controllerAudio.gvrPointer.SetActive(true);
    controller.enabled = true;


    if (PlayerPrefs.GetString("IntroClio") != "ok")
    {
      doingAnything = true;
      controller.enabled = false;
      isIntroPlaying = true;

      controllerAudio.gvrPointer.SetActive(false);
      audioClioSource.PlayDelayed(8.00f);
    }
    else
    {
      paredePlayer.SetActive(false);

    }
  }

  // Update is called once per frame
  void Update()
  {

    clioIcon.transform.LookAt(player.transform);
    if (!audioClioSource.isPlaying)
    {
      clioIcon.enabled = false;
    }
    if (audioClioSource.isPlaying && !iconClioChanged)
    {
      iconClioChanged = true;
      StartCoroutine("audioClioIcon");
    }

    if (doingAnything)
    {
      if (Input.anyKeyDown)
      {
        if (Input.GetKeyDown(cheatCode[indexLock]) || Input.GetKeyDown(cheatCode2[indexLock]))
        {
          indexLock++;
        }
        else
        {
          indexLock = 0;
        }
      }
      if ((indexLock == cheatCode.Length) || (indexLock == cheatCode2.Length))//PULAR TUDO
      {
        PlayerPrefs.SetString("IntroClio", "ok");
        isIntroPlaying = false;
        isTourPlaying = false;
        isTutorialPlaying = false;
        painelControle.SetActive(false);
        paredePlayer.SetActive(false);
        //LIBERAR CONTROLE
        doingAnything = false;
        audioClioSource.clip = audioCorrect;
        audioClioSource.Play();
        controller.enabled = true;
        indexLock = 0;
        controllerAudio.gvrPointer.SetActive(true);

      }
    }

    if (isIntroPlaying)
    {
      if (!audioClioSource.isPlaying)
      {

        isIntroPlaying = false;
        isTutorialPlaying = true;

      }
    }

    if (isTutorialPlaying)
    {
      doingAnything = true;
      switch (stepInstrucionController)
      {
        case 0:
          if (canContinueTutorial)
          {
            canContinueTutorial = false;
            if (audioIntrucoesPlayerTutorial[turnInstrucion] != null)
            {
              painelControle.SetActive(true);
              audioClioSource.clip = audioIntrucoesPlayerTutorial[turnInstrucion];
              audioClioSource.Play();
            }
          }
          if (!canContinueTutorial && !audioClioSource.isPlaying)
          {
            stepInstrucionController++;
            turnInstrucion++;
            canContinueTutorial = true;
          }

          break;
        case 1:
          if (canContinueTutorial)
          {
            canContinueTutorial = false;
            lockAction = false;
            print("Primeiro Passo do Tutorial");
            controller.enabled = true;
            textInstrucion.text = "Mova o Joystick para Andar";
            if (audioIntrucoesPlayerTutorial[turnInstrucion] != null)
            {
              audioClioSource.clip = audioIntrucoesPlayerTutorial[turnInstrucion];
              audioClioSource.Play();

            }
          }
          if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 && !lockAction)
          {
            lockAction = true;
            StartCoroutine("correctTutorial");
          }

          break;
        case 2:
          if (canContinueTutorial)
          {
            canContinueTutorial = false;
            lockAction = false;
            print("Segundo Passo do Tutorial");
            textInstrucion.text = "Aperte no botao de Selecionar";
            controllerAudio.gvrPointer.SetActive(true);
            if (audioIntrucoesPlayerTutorial[turnInstrucion] != null)
            {
              audioClioSource.clip = audioIntrucoesPlayerTutorial[turnInstrucion];
              audioClioSource.Play();

            }
          }
          if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Mouse0) && !canContinueTutorial && !lockAction)
          {
            lockAction = true;
            StartCoroutine("correctTutorial");
          }


          break;
        case 3:
          if (canContinueTutorial)
          {
            canContinueTutorial = false;
            lockAction = false;
            print("Terceiro Passo do Tutorial");
            textInstrucion.text = "Aperte no botao de Pular";
            if (audioIntrucoesPlayerTutorial[turnInstrucion] != null)
            {
              audioClioSource.clip = audioIntrucoesPlayerTutorial[turnInstrucion];
              audioClioSource.Play();

            }
          }
          if (Input.GetButtonDown("Jump") && !lockAction)
          {
            lockAction = true;
            StartCoroutine("correctTutorial");
          }

          break;
        case 4:
          if (canContinueTutorial)
          {
            canContinueTutorial = false;
            textInstrucion.text = "Tutorial Finalizado!";
            StartCoroutine("endedTutorial");
          }
          break;
      }
    }

    if (isTourPlaying)
    {
      timing = audioClioSource.time;
      print("Timing: " + timing);
      if (!audioClioSource.isPlaying && !isInTour)
      {
        doingAnything = true;
        isInTour = true;
        stepTourPosition = 0;
        audioClioSource.clip = audioTour;
        audioClioSource.Play();
      }
      if (timing >= 6.50f && stepTourPosition == 0)
      {
        player.transform.position = positionTour[stepTourPosition].transform.position;
        stepTourPosition++;

      }
      else if (timing >= 14.00f && stepTourPosition == 1)
      {

        player.transform.position = positionTour[stepTourPosition].transform.position;
        stepTourPosition++;
      }
      else if (timing >= 25.50f && stepTourPosition == 2)
      {
        player.transform.position = positionTour[stepTourPosition].transform.position;
        stepTourPosition++;

      }
      if (!audioClioSource.isPlaying && isInTour && stepTourPosition == 3)
      {
        doingAnything = false;
        //Ativar TUDO
        PlayerPrefs.SetString("IntroClio", "ok");
        isTourPlaying = false;
        controller.enabled = true;
        controllerAudio.gvrPointer.SetActive(true);
      }

    }

    if (isPlayingAudioDesc)
    {
      if (!audioClioSource.isPlaying)
      {
        controller.enabled = true;//Habilitar andar
        isPlayingAudioDesc = false;//Dizer que parou
        controllerAudio.gvrPointer.SetActive(true);//Habilita o Gaze Pointer
        audioClioSource.clip = null;//Tirar clip
      }
    }
  }


  IEnumerator correctTutorial()
  {
    stepInstrucionController++;
    turnInstrucion++;
    audioClioSource.clip = audioCorrect;
    audioClioSource.Play();
    textInstrucion.text = "Parabens";
    yield return new WaitForSeconds(3.5f);

    canContinueTutorial = true;
  }

  IEnumerator endedTutorial()
  {
    audioClioSource.clip = audioIntrucoesPlayerTutorial[turnInstrucion];
    audioClioSource.Play();
    isTutorialPlaying = false;
    yield return new WaitForSeconds(5.00f);
    audioClioSource.Stop();
    paredePlayer.SetActive(false);
    painelControle.SetActive(false);
    //INICIAR O TOUR
    isTourPlaying = true;
    controller.enabled = false;
    controllerAudio.gvrPointer.SetActive(false);
  }

  IEnumerator audioClioIcon()
  {
    clioIcon.enabled = true;
    if (audioClioSource.isPlaying) clioIcon.color = new Color(1, 1, 1, 0.5f);
    else clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    if (audioClioSource.isPlaying) clioIcon.color = new Color(1, 1, 1, 0.75f);
    else clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    iconClioChanged = false;
  }

}
