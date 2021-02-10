using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Damage : MonoBehaviour {

    
    public float whiteScreenFlashTimer = 0.1f;
    public bool whiteScreenFlashEnabled = false;
    private float whiteScreenFlashTimerStart;
    public GameObject whiteScreen;

    public float FadeTime;
    public float FadeSpeed;

    public static float infection;
    public Text InfectionText;
    Image CriticalScreen;
    public GameObject redScreen;
    public bool redScreenFlashEnabled = false;
    public float redScreenFlashTimer = 0.1f;
    private float redScreenFlashTimerStart;

    AudioSource audioSrc;
    bool playAudioCrit = true;
    public AudioClip virusCrit;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip heal;

    public Text DisplayText;

    public Vector3 LastCheckPoint;

    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        CriticalScreen = redScreen.GetComponent<Image>();
        FadeTime = 0.0f;
        FadeSpeed = 1.0f;
        LastCheckPoint = gameObject.transform.position;
        redScreenFlashTimerStart = redScreenFlashTimer;
        infection = 0;
        SetCountInfect();

	}
	
	// Update is called once per frame
	void Update () {


        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }
        if (FadeTime < 0)
        {
            FadeTime = 0;
            DisplayText.text = "";
        }
        if (FadeTime >= 3)
        {
            FadeTime = 3;
        }
        if (infection < 100)
        {
            //Dead = false;
        }
        if (infection >= 75)
        {
            if (playAudioCrit) {
                PlayAudioClip(virusCrit);
                playAudioCrit = false;
            }
            redScreen.SetActive(true);
            if (infection >= 90)
            {
                CriticalScreen.color = new Color(CriticalScreen.color.r, CriticalScreen.color.g, CriticalScreen.color.b, Mathf.PingPong(Time.time * 10, 1));
                InfectionText.color = new Color(Mathf.Sin(Time.time * 30), 0f, 0f, 1.0f);
            }
            else
            {
                CriticalScreen.color = new Color(CriticalScreen.color.r, CriticalScreen.color.g, CriticalScreen.color.b, Mathf.PingPong(Time.time, 1));
                InfectionText.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
            }


        }

        // Ammo Flashing Color if ammo is below number
        else if (infection <= 74)
        {
            InfectionText.color = new Color(1f, 1f, 1f, 1f);
        }
        if (redScreenFlashEnabled == true)
        {
            redScreen.SetActive(true);
            redScreenFlashTimer -= Time.deltaTime;
        }

        if (redScreenFlashTimer <= 0)
        {
            redScreen.SetActive(false);
            redScreenFlashEnabled = false;
            redScreenFlashTimer = redScreenFlashTimerStart;
        }
        if (whiteScreenFlashEnabled == true)
        {
            whiteScreen.SetActive(true);
            whiteScreenFlashTimer -= Time.deltaTime;
        }

        if (whiteScreenFlashTimer <= 0)
        {
            whiteScreen.SetActive(false);
            whiteScreenFlashEnabled = false;
            whiteScreenFlashTimer = whiteScreenFlashTimerStart;
        }
    }
    public void DoDamage(float amount)
    {
        infection += amount;
        redScreenFlashEnabled = true;

        if (infection >= 100)
        {
            //audioSrc.clip = death;
            //audioSrc.Play();
            PlayAudioClip(death);
            playAudioCrit = true;
            gameObject.transform.position = LastCheckPoint;
            //Dead = true;
            infection = 0;
            redScreen.SetActive(false);
            ScoreManager.SM.Player1Dead();
        }
        SetCountInfect();
    }
    public void ReduceInfection(float amount)
    {
        infection -= amount;
        whiteScreenFlashEnabled = true;

        if (infection <= 0)
        {
            infection = 0;
            whiteScreen.SetActive(false);
        }
        SetCountInfect();
    }
    public void SetCountInfect()
    {
        InfectionText.text = Mathf.CeilToInt(infection).ToString() + " %";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GreenInfection")) {

            //audioSrc.PlayOneShot(hurt);
            PlayAudioClip(hurt);
            DoDamage(Random.Range(1, 4));
            SetCountInfect();
        }
    }
    
    private void PlayAudioClip(AudioClip audio) {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        AudioSource audioSourceToUse = null;
        if (audioSources.Length != 0) {
            for (int i = 0; i < audioSources.Length; i++) {
                if (!audioSources[i].isPlaying) {
                    audioSourceToUse = audioSources[i];
                }
            }
        }

        if (audioSourceToUse == null) {
            audioSourceToUse = gameObject.AddComponent<AudioSource>();
        }

        audioSourceToUse.PlayOneShot(audio);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueVial"))
        {
            if (infection >= 1)
            {
                if (other.GetComponent<Pickup_Health>())
                {
                    if (other.GetComponent<Pickup_Health>().active == true)
                    {
                        //audioSrc.clip = heal;
                        //audioSrc.Play();
                        PlayAudioClip(heal);
                        DisplayText.text = "infection reduced significantly";
                        FadeTime += 3;
                        whiteScreenFlashEnabled = true;
                        ReduceInfection(Random.Range(10, 20));
                        SetCountInfect();
                        other.GetComponent<Pickup_Health>().pickup();
                    }
                }
            }
        }
    }
}
