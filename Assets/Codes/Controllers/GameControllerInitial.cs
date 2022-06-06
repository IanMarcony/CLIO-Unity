using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameControllerInitial : MonoBehaviour
{
  [Header("Panel")]
  public GameObject mainPanel;
  public GameObject intructionsPanel;
  public GameObject teamPanel;

  [Header("Audios Click")]

  public AudioSource clickButton;
  // Start is called before the first frame update
  public void Start()
  {
    StartCoroutine(DesactiveVRToClio("none"));
    PlayerPrefs.SetString("IntroClio", "not");

    mainPanel.SetActive(true);
    teamPanel.SetActive(false);
    intructionsPanel.SetActive(false);



  }



  public void PlayCLio()
  {

    mainPanel.SetActive(false);
    teamPanel.SetActive(false);
    intructionsPanel.SetActive(true);
    clickButton.Play();

    StartCoroutine(ActiveVRToClio("cardboard"));


  }

  public void goToTeam()
  {
    mainPanel.SetActive(false);
    teamPanel.SetActive(true);
    intructionsPanel.SetActive(false);
    clickButton.Play();

  }

  public void goToMain()
  {
    mainPanel.SetActive(true);
    teamPanel.SetActive(false);
    intructionsPanel.SetActive(false);
    clickButton.Play();

  }



  public IEnumerator ActiveVRToClio(String YESVR)
  {
    yield return new WaitForSeconds(10);
        
    XRSettings.LoadDeviceByName(YESVR);    

    XRSettings.enabled = true;
    intructionsPanel.SetActive(false);

    SceneManager.LoadScene("MainRoom");
  }

  public IEnumerator DesactiveVRToClio(String NOVR)
  {


    XRSettings.LoadDeviceByName(NOVR);
    yield return new WaitForSeconds(0.5f);

    XRSettings.enabled = false;



  }

  public void enterSiteCLIO()
  {
    clickButton.Play();

    Application.OpenURL("https://clio-tcc.herokuapp.com");
  }
  public void enterInstagramCLIO()
  {
    clickButton.Play();

    Application.OpenURL("https://instagram.com/deusa_clio/");

  }



}
