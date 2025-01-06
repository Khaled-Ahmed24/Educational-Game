using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainScript : MonoBehaviour {
	public GameObject[] menu;
	public AudioSource backgroundAudio;
    public AudioSource clickAudio;
    public Slider volSlider;
    public GameObject muteButton;
    public GameObject backButton;
    public Sprite[] sp;
    private GameObject currentMenu;
    private string Gender;
    void Start () {
		BackgroundAudio();
        selectGender("Male");
    }

	public void BackgroundAudio ()
	{
        backgroundAudio.loop = true;
        backgroundAudio.Play();
        volSlider.value = backgroundAudio.volume;
	}
    public void OnButtonClickAudio()
    {
        clickAudio.Play();
    }
    public void Mute()
	{
		if(backgroundAudio.mute) {
            
            backgroundAudio.mute = false;
			muteButton.GetComponent<Image>().sprite = sp[1];
            if(backgroundAudio.volume == 0)
            {
                backgroundAudio.volume = 0.3f;
                volSlider.value = backgroundAudio.volume;
            }
                
		}
		else
		{
            backgroundAudio.mute = true;
            muteButton.GetComponent<Image>().sprite = sp[0];
        }
	}
    public void OnChangeVolume()
    {
        backgroundAudio.volume = volSlider.value;
        CheckMute();
    }
    public void LetsStart()
    {
        OnButtonClickAudio();
        menu[0].SetActive(false);
        menu[1].SetActive(true);
        backButton.SetActive(true);
        currentMenu = menu[1];
    }
    public void AlphabetsLevel()
    {
        OnButtonClickAudio();
        currentMenu.SetActive(false);
        menu[4].SetActive(true);
        currentMenu = menu[4];
    }
    public void NumbersLevel()
    {
        OnButtonClickAudio();
        currentMenu.SetActive(false);
        menu[5].SetActive(true);
        currentMenu = menu[5];
    }
    public void Quizzes()
    {
        OnButtonClickAudio();
        menu[0].SetActive(false);
        menu[2].SetActive(true);
        backButton.SetActive(true);
        currentMenu = menu[2];
    }
    public void Options()
	{
        OnButtonClickAudio();
        menu[0].SetActive(false);
        menu[3].SetActive(true);
        backButton.SetActive(true);
        currentMenu = menu[3];
    }
    public void selectGender(string gender)
    {
        Gender = gender;
        PlayerPrefs.SetString("Gender", Gender);
    }
    public void BackButton()
    {
        OnButtonClickAudio();
        if(currentMenu == menu[1])
        {
            currentMenu.SetActive(false); 
            menu[0].SetActive(true);
            backButton.SetActive(false);
            currentMenu = menu[0];
        }
        else if(currentMenu == menu[2])
        {
            currentMenu.SetActive(false);
            menu[0].SetActive(true);
            backButton.SetActive(false);
            currentMenu = menu[0];
        }
        else if(currentMenu == menu[3])
        {
            currentMenu.SetActive(false);
            menu[0].SetActive(true);
            backButton.SetActive(false);
            currentMenu = menu[0];
        }
        else if(currentMenu == menu[4])
        {
            currentMenu.SetActive(false);
            menu[1].SetActive(true);
            currentMenu = menu[1];
        }
        else if(currentMenu == menu[5])
        {
            currentMenu.SetActive(false);
            menu[1].SetActive(true);
            currentMenu = menu[1];
        }
    }
    public void QuitGame()
    {
        OnButtonClickAudio();
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void Learn(string c)
    {
        if (c[0] >= 'A' && c[0] <= 'Z')
            PlayerPrefs.SetInt("Type", 0);
        else if (c[0] >= '0' && c[0] <= '9')
            PlayerPrefs.SetInt("Type", 1);
        PlayerPrefs.SetString("SelectedChar", c);
        backgroundAudio.Pause();
        SceneManager.LoadScene("LearningScene", LoadSceneMode.Additive);
    }
    public void Quizz(bool c)
    {
        PlayerPrefs.SetInt("QuizzType", Convert.ToInt32(c));
        backgroundAudio.Pause();
        SceneManager.LoadScene("QuizzScene", LoadSceneMode.Additive);
    }

    private void CheckMute()
    {
        if (backgroundAudio.volume == 0)
        {
            muteButton.GetComponent<Image>().sprite = sp[0];
            backgroundAudio.mute = true;
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = sp[1];
            backgroundAudio.mute = false;
        }
    }
}
