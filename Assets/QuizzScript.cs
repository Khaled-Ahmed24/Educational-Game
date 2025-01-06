using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class QuizzScript : MonoBehaviour {
	public GameObject[] Question;
	public GameObject[] Nums;
    public AudioSource clickAudio;
	public TextMeshProUGUI time, num, total, timeTxt, scoreTxt, gameOverScore, finalTime;
	public GameObject GameOver, RetryButton;
	private int currentIdx, type, zone;
	private char correctAns;
    private AudioListener mainListener;
    private MainScript main;
	void Start () {
		type = PlayerPrefs.GetInt("QuizzType");
        mainListener = FindObjectOfType<AudioListener>();
        mainListener.enabled = false;
        Shuffle();
		StartCoroutine(timer());
        zone = Convert.ToInt32(time.text);
        Debug.Log(type);
	}
	
	public void Comp(string c)
	{
        clickAudio.Play();
		if(correctAns == c[0]) {
			int n = Convert.ToInt32(num.text);
			n++;
			num.text = Convert.ToString(n);
		}
		if(type == 0)
		{
            Question[currentIdx].SetActive(false);
            currentIdx++;
            if (currentIdx < Convert.ToInt32(total.text))
            {
                Question[currentIdx].SetActive(true);
                correctAns = Question[currentIdx].name[0];
            }
            else
            {
                StopAllCoroutines();
                timeTxt.gameObject.SetActive(false);
                scoreTxt.gameObject.SetActive(false);
                gameOverScore.text = num.text;
                finalTime.text = Convert.ToString(zone - Convert.ToInt32(time.text));
                GameOver.SetActive(true);
                if (Convert.ToInt32(gameOverScore.text) <= Convert.ToInt32(total.text) / 2)
                    RetryButton.SetActive(true);
            }
        }
        else
        {
            Nums[currentIdx].SetActive(false);
            currentIdx++;
            if (currentIdx < Convert.ToInt32(total.text))
            {
                Nums[currentIdx].SetActive(true);
                correctAns = Nums[currentIdx].name[0];
            }
            else
            {
                StopAllCoroutines();
                timeTxt.gameObject.SetActive(false);
                scoreTxt.gameObject.SetActive(false);
                gameOverScore.text = num.text;
                finalTime.text = Convert.ToString(zone - Convert.ToInt32(time.text));
                GameOver.SetActive(true);
                if (Convert.ToInt32(gameOverScore.text) <= Convert.ToInt32(total.text) / 2)
                    RetryButton.SetActive(true);
            }
        }
    }

	IEnumerator timer()
	{
		for(int i = Convert.ToInt32(time.text); i > 0; i--)
		{
			time.text = Convert.ToString(i);
			yield return new WaitForSeconds(1.0f);
		}
		if(type == 0)
		{
            Question[currentIdx].SetActive(false);
        }
		else
		{
			Nums[currentIdx].SetActive(false);
		}
        timeTxt.gameObject.SetActive(false);
        scoreTxt.gameObject.SetActive(false);
        gameOverScore.text = num.text;
        finalTime.text = Convert.ToString(zone);
        GameOver.SetActive(true);
        if (Convert.ToInt32(gameOverScore.text) <= Convert.ToInt32(total.text) / 2)
            RetryButton.SetActive(true);
    }
	private void Shuffle()
	{
		if(type == 0)
		{
            for (int i = 0; i < Question.Length; i++)
            {
                int randomIdx = UnityEngine.Random.Range(0, Question.Length);
                GameObject temp = Question[i];
                Question[i] = Question[randomIdx];
                Question[randomIdx] = temp;
            }
            Question[0].SetActive(true);
            correctAns = Question[0].name[0];
        }
		else
		{
            for (int i = 0; i < Nums.Length; i++)
            {
                int randomIdx = UnityEngine.Random.Range(0, Nums.Length);
                GameObject temp = Nums[i];
                Nums[i] = Nums[randomIdx];
                Nums[randomIdx] = temp;
            }
            Nums[0].SetActive(true);
            correctAns = Nums[0].name[0];
        }
    }

	public void Retry()
	{
        clickAudio.Play();
        time.text = Convert.ToString(zone);
		num.text = "0";
        timeTxt.gameObject.SetActive(true);
        scoreTxt.gameObject.SetActive(true);
        GameOver.SetActive(false);
        RetryButton.SetActive(false);
		currentIdx = 0;
        Shuffle();
        StartCoroutine(timer());
    }

    public void BackButton()
    {
        clickAudio.Play();
        main = FindObjectOfType<MainScript>();
        main.backgroundAudio.Play();
        mainListener.enabled = true;
        SceneManager.UnloadSceneAsync("QuizzScene");
    }
}
