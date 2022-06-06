using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{

  private AudioSource audioClioSourceWorld;
  private bool iconClioChanged;
  private StopAnimation stop;
  [Header("Image Clio")]
  public Image clioIcon;
  [Header("Configuracao do Score")]
  public TextMeshProUGUI textScore;
  public AudioClip[] soundsScore;


  [Header("Audios da personagem Clio")]
  public AudioClip[] audiosClioDesc;
  public AudioClip[] audiosClioPerg;
  public int orderAudioPerg = 0;
  public int orderAudioDesc = 0;

  [Header("Configuracao das Perguntas")]



  public GameObject[] PanelQuestion;
  public GameObject pontosParada;
  public bool isAnswering;

  // Start is called before the first frame update
  void Start()
  {
    PlayerPrefs.SetInt("ScoreFran", 0);
    textScore.text = PlayerPrefs.GetInt("ScoreFran").ToString();

    audioClioSourceWorld = GetComponent<AudioSource>();
    stop = FindObjectOfType(typeof(StopAnimation)) as StopAnimation;

    audioClioSourceWorld.clip = audiosClioDesc[orderAudioDesc];
    audioClioSourceWorld.PlayDelayed(2.5f);

  }

  // Update is called once per frame
  void Update()
  {

    if (audioClioSourceWorld.isPlaying && !iconClioChanged)
    {
      iconClioChanged = true;
      StartCoroutine("audioClioIcon");
    }
    if (stop.isStopAnimation && pontosParada != null && !isAnswering)
    {
      isAnswering = true;
      audioClioSourceWorld.clip = audiosClioPerg[orderAudioPerg];
      audioClioSourceWorld.PlayDelayed(1.0f);
      PanelQuestion[orderAudioPerg].SetActive(true);


    }


  }

  public void Responder(int respostaButton)
  {
    if (respostaButton == pontosParada.GetComponent<QuestionInfo>().resposta)
    {
      int score = PlayerPrefs.GetInt("ScoreFran");
      score += 10;
      PlayerPrefs.SetInt("ScoreFran", score);
      audioClioSourceWorld.PlayOneShot(soundsScore[0]);
    }
    else
    {
      audioClioSourceWorld.PlayOneShot(soundsScore[1]);
    }
    textScore.text = PlayerPrefs.GetInt("ScoreFran").ToString();
    pontosParada.SetActive(false);
    isAnswering = false;
    PanelQuestion[orderAudioPerg].SetActive(false);
    orderAudioPerg++;
    stop.anim.speed = 1;
    stop.isStopAnimation = false;

    try
    {

      audioClioSourceWorld.clip = audiosClioDesc[++orderAudioDesc];
      audioClioSourceWorld.Play();
    }
    catch (IndexOutOfRangeException ex)
    {
      print("Sem descrição");
    }



  }

  IEnumerator audioClioIcon()
  {

    if (audioClioSourceWorld.isPlaying) clioIcon.color = new Color(1, 1, 1, 0.5f);
    else clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    if (audioClioSourceWorld.isPlaying) clioIcon.color = new Color(1, 1, 1, 0.75f);
    else clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    clioIcon.color = new Color(1, 1, 1, 1);
    yield return new WaitForSeconds(0.3f);
    iconClioChanged = false;

  }
}
