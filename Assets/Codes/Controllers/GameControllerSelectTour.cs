using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum TypeTimeline { Amazonas, Brasil, Geral }
public class GameControllerSelectTour : MonoBehaviour
{
  private bool gazeAt;
  [Header("Gaze Pointer Object")]

  public GameObject gazePointerObject;


  [Header("Botão de Voltar")]
  public GameObject buttonBack;
  [Header("Timelines")]
  public GameObject timeline;
  public GameObject selectIcon;

  [Header("Particulas de Luz")]

  public ParticleSystem[] particulasLuz;
  public Light luz;

  [Header("Sounds to Travel")]

  public AudioSource audioSource;
  public AudioClip audioTravel;
  [Header("UI Timeline")]

  public TextMeshProUGUI textHeaderUITimeline;
  public Image imageUITimeline;

  [Header("All Point Time")]

  private TimeTravelController timeTravelController;

  public TypeTimeline typeTimeline;
  public int indexTimeline;
  public Sprite[] imagesAmazonas;
  public String[] textHeaderAmazonas;
  public Sprite[] imagesBrasil;
  public String[] textHeaderBrasil;
  public Sprite[] imagesGeral;
  public String[] textHeaderGeral;

  [Header("Scene's Name")]
  public String[] nameSceneAmazonas;
  public String[] nameSceneBrasil;
  public String[] nameSceneGeral;


  private bool clicked;

  // Start is called before the first frame update
  void Start()
  {
    timeTravelController = FindObjectOfType(typeof(TimeTravelController)) as TimeTravelController;
    indexTimeline = 0;
    switch (PlayerPrefs.GetString("Timeline"))
    {
      case "amazonas":
        typeTimeline = TypeTimeline.Amazonas;
        textHeaderUITimeline.text = textHeaderAmazonas[indexTimeline];
        imageUITimeline.sprite = imagesAmazonas[indexTimeline];
        timeTravelController.scene = nameSceneAmazonas[indexTimeline];
        break;
      case "brazil":
        typeTimeline = TypeTimeline.Brasil;
        textHeaderUITimeline.text = textHeaderBrasil[indexTimeline];
        imageUITimeline.sprite = imagesBrasil[indexTimeline];
        timeTravelController.scene = nameSceneBrasil[indexTimeline];
        break;
      case "general":
        typeTimeline = TypeTimeline.Geral;
        textHeaderUITimeline.text = textHeaderGeral[indexTimeline];
        imageUITimeline.sprite = imagesGeral[indexTimeline];
        timeTravelController.scene = nameSceneGeral[indexTimeline];
        break;
    }
  }



  public void NextTimeline()
  {
    if (clicked) return;
    print("Próximo");
    clicked = true;

    switch (typeTimeline)
    {
      case TypeTimeline.Amazonas:
        if (indexTimeline < textHeaderAmazonas.Length - 1) indexTimeline++;
        textHeaderUITimeline.text = textHeaderAmazonas[indexTimeline];
        imageUITimeline.sprite = imagesAmazonas[indexTimeline];
        timeTravelController.scene = nameSceneAmazonas[indexTimeline];
        break;
      case TypeTimeline.Brasil:
        if (indexTimeline < textHeaderBrasil.Length - 1) indexTimeline++;

        textHeaderUITimeline.text = textHeaderBrasil[indexTimeline];
        imageUITimeline.sprite = imagesBrasil[indexTimeline];
        timeTravelController.scene = nameSceneBrasil[indexTimeline];
        break;
      case TypeTimeline.Geral:
        if (indexTimeline < textHeaderGeral.Length - 1) indexTimeline++;
        textHeaderUITimeline.text = textHeaderGeral[indexTimeline];
        imageUITimeline.sprite = imagesGeral[indexTimeline];
        timeTravelController.scene = nameSceneGeral[indexTimeline];
        break;
    }
    clicked = false;


  }


  public void BackTimeline()
  {
    if (clicked) return;
    print("Voltar");
    clicked = true;
    if (indexTimeline > 0)
    {

      indexTimeline--;
      switch (typeTimeline)
      {
        case TypeTimeline.Amazonas:

          textHeaderUITimeline.text = textHeaderAmazonas[indexTimeline];
          imageUITimeline.sprite = imagesAmazonas[indexTimeline];
          timeTravelController.scene = nameSceneAmazonas[indexTimeline];
          break;
        case TypeTimeline.Brasil:


          textHeaderUITimeline.text = textHeaderBrasil[indexTimeline];
          imageUITimeline.sprite = imagesBrasil[indexTimeline];
          timeTravelController.scene = nameSceneBrasil[indexTimeline];
          break;
        case TypeTimeline.Geral:

          textHeaderUITimeline.text = textHeaderGeral[indexTimeline];
          imageUITimeline.sprite = imagesGeral[indexTimeline];
          timeTravelController.scene = nameSceneGeral[indexTimeline];
          break;
      }
    }
    clicked = false;


  }




}


