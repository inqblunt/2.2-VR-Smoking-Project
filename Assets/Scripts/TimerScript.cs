using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public static bool firstIsStarted = false;
    public static bool secondIsStarted = false;
    public static bool buttonEnabled = true;
    public static float timer = 0;
    public Text timeText;
    public Slider scoreSlider;
    static bool updateLabel = true;

    public static int isSmoking = 0;
    
    public static int score = 8000;

    int updateCounter = 0;

    public static bool flush = false;
    static bool endMusicPlay = false;

    public AudioClip TimeClip;
    public AudioSource SoundSource;
    public AudioClip ScoreClip;
    public AudioSource ScoreSource;
    public AudioSource EndSource;
    public AudioClip EndClip;

    // Use this for initialization
    void Start () {
        // StartTimer();
        SoundSource.clip = TimeClip;
        ScoreSource.clip = ScoreClip;
        EndSource.clip = EndClip;
    }

    public static void StartTimer ()
    {
        // player starts building phase
        firstIsStarted = true;
        updateLabel = true;
        buttonEnabled = false; // disables starting button 
        GameObject.Find("NavMesh").SetActive(true);
        endMusicPlay = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (firstIsStarted == true || secondIsStarted == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 1 && secondIsStarted != true)
        {
            firstIsStarted = false;
            timer = 0;
            // start second round, enable spawning and AI patterns
            secondIsStarted = true;
            GameObject.Find("CharacterManager").GetComponent<SpawnScript>().ButtonSwitch();
        }
        if ((timer >= 60 && secondIsStarted == true) || score <= 0)
        {
            if (endMusicPlay == false)
            {
                EndSource.Play();
                endMusicPlay = true;
            }
            isSmoking = 0;
            secondIsStarted = false;
            updateLabel = false;
            timer = 0;
            if (score < 0)
            {
                score = 0;
            }
            timeText.text = "Final Score: " + score.ToString();
            buttonEnabled = true;
            GameObject.Find("NavMesh").SetActive(false);
            flush = true;
            //end game
            //display score and cleanup
            //reset game
        }
        var minutes = timer / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = timer % 60;//Use the euclidean division for the seconds.
        scoreSlider.value = score;
        //update the label value
        if (updateLabel == true) {
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            //SoundSource.Play();
        }
            //Debug.Log(isSmoking);
    }

    private void FixedUpdate()
    {
        updateCounter++;

        if (updateCounter % 50 == 0 && secondIsStarted == true)
        {
            int toLose = 100 * isSmoking;
            if (toLose > 0)
            {
                ScoreSource.Play();
            }
            if (secondIsStarted == true)
            {
                SoundSource.Play();
            }
            score -= toLose;
            
        }
    }
        
}
