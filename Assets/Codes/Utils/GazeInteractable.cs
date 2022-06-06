using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public enum TourSelected { Amazonas, Brazil, General }
public class GazeInteractable : MonoBehaviour
{


  private bool gazeAt;
  [Header("Canvas Select")]
  public GameObject canvasSelect;
  [Header("About Description Audio")]
  public AudioSource clickAudio;
  public AudioClip audioClip;
  [Header("About Select Tour")]
  public bool isSelectTour;
  public TourSelected selectTour;

 private void Start(){
    if(canvasSelect!=null)canvasSelect.SetActive(false);

 }

  // Update is called once per frame
  void Update()
  {




    if (gazeAt && (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Mouse0)))
    {
      clickAudio.clip = audioClip;
      clickAudio.Play();
      if (isSelectTour)
      {
        switch (selectTour)
        {
          case TourSelected.Amazonas:
            PlayerPrefs.SetString("Timeline", "amazonas");
            break;
          case TourSelected.Brazil:
            PlayerPrefs.SetString("Timeline", "brazil");
            break;
          case TourSelected.General:
            PlayerPrefs.SetString("Timeline", "general");
            break;
        }
      }
      ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);



    }

  }






  public void PointerEnter()
  {
    gazeAt = true;
    if(canvasSelect!=null)canvasSelect.SetActive(gazeAt);



  }
  public void PointerExit()
  {
    gazeAt = false;

    if(canvasSelect!=null)canvasSelect.SetActive(gazeAt);

  }


}
