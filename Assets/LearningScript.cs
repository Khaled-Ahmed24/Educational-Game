
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LearningScript : MonoBehaviour {

	public GameObject C;
    public GameObject E1;
    public GameObject E2;
    public GameObject N;
    public GameObject Character;
    public GameObject replayButton;
    public AudioSource audioSource;
    public AudioSource clickAudio;
    public Sprite[] Characters;
    public Sprite[] charExamples;
    public AudioClip[] MCharacterSources;
    public AudioClip[] FCharacterSources;
    public Sprite[] Numbers;
    public AudioClip[] numbersM;
    public AudioClip[] numbersF;
    public Sprite[] G;

    private string Gender;
    private string SelectedCharacter;
    private string SelectedNumber;
	private int idx, t;
    private MainScript main;
    private AudioListener mainListener;
    void Start () {
        SelectedCharacter = PlayerPrefs.GetString("SelectedChar");
        Gender = PlayerPrefs.GetString("Gender");
        t = PlayerPrefs.GetInt("Type");
        Debug.Log(t);
        mainListener = FindObjectOfType<AudioListener>();
        mainListener.enabled = false;
        if (t == 1)
            Number();
        else
            Letter();
    }

	IEnumerator Seconds()
	{
        while (audioSource.isPlaying)
        {
            //Debug.Log(audioSource.time);
            if(t == 0)
            {
                if (audioSource.time >= 1.0f)
                {
                    E1.SetActive(true);
                }
                if (audioSource.time >= 3.5f)
                {
                    E2.SetActive(true);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        replayButton.SetActive(true);
    }
    public void Letter()
    {
        //Debug.Log(SelectedCharacter);
        //Debug.Log(Gender);
        char c = SelectedCharacter[0];
        int idx = c - 'A';
        //Debug.Log(idx);
        C.GetComponent<Image>().sprite = Characters[idx];
        E1.GetComponent<Image>().sprite = charExamples[idx * 2];
        E2.GetComponent<Image>().sprite = charExamples[idx * 2 + 1];
        if (Gender == "Male")
        {
            audioSource.clip = MCharacterSources[idx];
            Character.GetComponent<Image>().sprite = G[0];
        }  
        else
        {
            audioSource.clip = FCharacterSources[idx];
            Character.GetComponent<Image>().sprite = G[1];
        }
        audioSource.Play();
        C.SetActive(true);
        Character.SetActive(true);
        StartCoroutine(Seconds());
    }
    public void Number()
    {
        //Debug.Log(SelectedCharacter);
        //Debug.Log(Gender);
        char c = SelectedCharacter[0];
        int idx = c - '0';
        //Debug.Log(idx);
        N.GetComponent<Image>().sprite = Numbers[idx];
        if (Gender == "Male")
        {
            audioSource.clip = numbersM[idx];
            Character.GetComponent<Image>().sprite = G[0];
        }  
        else
        {
            audioSource.clip = numbersF[idx];
            Character.GetComponent<Image>().sprite = G[1];
        }
        audioSource.Play();
        N.SetActive(true);
        Character.SetActive(true);
        StartCoroutine(Seconds());
    }
    public void Reapet()
    {
        clickAudio.Play();
        replayButton.SetActive(false);
        if (t == 0)
        {
            E1.SetActive(false);
            E2.SetActive(false);
        }
        audioSource.Play();
        StartCoroutine(Seconds());
    }

    public void BackButton()
    {
        clickAudio.Play();
        main = FindObjectOfType<MainScript>();
        main.backgroundAudio.Play();
        mainListener.enabled = true;
        SceneManager.UnloadSceneAsync("LearningScene");
    }
}
