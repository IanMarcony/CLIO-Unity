using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
public class GazeSelectTour : MonoBehaviour
{
  private bool gazeAt;
  private bool gazeClicked;

  public bool isNextButton;

  private GameControllerSelectTour controller;
  [Header("Canvas Select")]
  public GameObject canvasSelect;
  [Header("About Description Audio")]
  public AudioSource clickAudio;
  public AudioClip audioClip;
  // Start is called before the first frame update
  private void Start()
  {
    controller = FindObjectOfType(typeof(GameControllerSelectTour)) as GameControllerSelectTour;
    if (canvasSelect != null) canvasSelect.SetActive(false);

  }
  void Update()
  {




    if (gazeAt && (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Mouse0)) && !gazeClicked && isNextButton)
    {
      gazeClicked = true;
      clickAudio.clip = audioClip;
      clickAudio.Play();
      controller.NextTimeline();

      gazeClicked = false;


    }
    if (gazeAt && (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Mouse0)) && !gazeClicked && !isNextButton)
    {
      gazeClicked = true;
      clickAudio.clip = audioClip;
      clickAudio.Play();
      controller.BackTimeline();

      gazeClicked = false;


    }

  }






  public void PointerEnter()
  {
    gazeAt = true;
    if (canvasSelect != null) canvasSelect.SetActive(gazeAt);




  }
  public void PointerExit()
  {
    gazeAt = false;

    if (canvasSelect != null) canvasSelect.SetActive(gazeAt);

  }

}

