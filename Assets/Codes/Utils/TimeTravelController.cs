using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTravelController : MonoBehaviour
{
  private GameControllerSelectTour controller;
  public String scene;
  // Start is called before the first frame update
  void Start()
  {
    controller = FindObjectOfType(typeof(GameControllerSelectTour)) as GameControllerSelectTour;
  }

  // Update is called once per frame
  public void changeScenePlayerTimeTravel()
  {
    if (scene == "") return;

    StartCoroutine(timeTravel(scene));



  }



  IEnumerator timeTravel(String scene)
  {
    controller.gazePointerObject.SetActive(false);
    controller.selectIcon.SetActive(false);
    controller.timeline.SetActive(false);
    controller.buttonBack.SetActive(false);
    controller.audioSource.Stop();
    controller.audioSource.loop = false;
    controller.audioSource.clip = controller.audioTravel;    
    controller.audioSource.Play();
    controller.luz.intensity = 0;
    controller.particulasLuz[0].Play();
    yield return new WaitForSeconds(1f);
    controller.particulasLuz[1].Play();

    for (float i = 0; i <= 1.5f; i += 0.1f)
    {
      controller.luz.intensity = i;
      yield return new WaitForEndOfFrame();
    }

    AsyncOperation loading = SceneManager.LoadSceneAsync(scene);

    while (!loading.isDone)
    {


      yield return null;
    }
  }

}
