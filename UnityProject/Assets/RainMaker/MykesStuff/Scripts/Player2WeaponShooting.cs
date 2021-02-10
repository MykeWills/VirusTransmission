using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2WeaponShooting : MonoBehaviour
{
   
    public float whiteScreenFlashTimer = 0.1f;
    public bool whiteScreenFlashEnabled = false;
    private float whiteScreenFlashTimerStart;
    public GameObject whiteScreen;
    public float FadeTime;
    public float FadeSpeed;
    bool dead;
    private Vector3 GreenGuninitPos = new Vector3(0f, 0f, 0f);
    private Vector3 GreenGunfinalPos = new Vector3(0f, 0.1f, -0.3f);
    public float currentLerpTime;
    private Vector3 backPos;
    Transform GreenGun;
    bool Recoiled;

    public int Ammo;
    public AudioSource audioSrc;
   
    public AudioClip BlastSound;
    public AudioClip reload;
    public Camera CameraPosition;
    public GameObject Bullet_Emitter_Green;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
  
    public GameObject MuzzleGreenFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;

    public Text PistolAmmo;
    public Text DisplayText;
    public bool AmmoShot;

    public float fireRate;
    private float nextFire;


    private Recoil recoilComponent;


    void Start()
    {
        
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        FadeTime = 0.0f;
        FadeSpeed = 1.0f;
        DisplayText.text = "";

        var cam = GameObject.Find("/MykesPlayer2Controller/Camera/Recoil/").transform;
        recoilComponent = cam.GetComponent<Recoil>();
        muzzleFlashTimerStart = muzzleFlashTimer;
        Recoiled = false;
        AmmoShot = true;
        Ammo = 100;
        SetCountAmmo();
    }
    public void FixedUpdate()
    {
        //dead = Player2Damage.P2Dead;
        if (dead)
        {
            Ammo = 100;
            SetCountAmmo();
        }
    }

    public void Update()
    {
      
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

        if (Ammo > 200)
        {
            Ammo = 200;

            SetCountAmmo();
        }
        if (Ammo < 0)
        {
            Ammo = 0;
            SetCountAmmo();
        }
        if (Ammo <= 25)
        {
            PistolAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        else if (Ammo >= 26 && Ammo < 200)
        {
            PistolAmmo.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (Ammo == 200)
        {
            PistolAmmo.color = new Color(0f, Mathf.Sin(Time.time * 10), 0f, 1.0f);
        }


        StartCoroutine(FindGun());
        if (Input.GetAxisRaw("Player2Fire1") == -1 && Time.time > nextFire && AmmoShot == true)
        {
            if (Ammo <= 0)
            {
                AmmoShot = false;
                Ammo = 0;
                SetCountAmmo();
            }
            else
            {
                recoilComponent.StartRecoil(0.04f, -10f, 20f);

                //audioSrc.PlayOneShot(BlastSound, 1);
                PlayAudioClip(BlastSound);
     
                Ammo -= 1;
                Recoiled = true;
                muzzleFlashEnabled = true;

                GameObject Bullet_Handler;
                Rigidbody Temporary_RigidBody;

                Bullet_Handler = Instantiate(Bullet, Bullet_Emitter_Green.transform.position, Bullet_Emitter_Green.transform.rotation) as GameObject;
                Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);
                Destroy(Bullet_Handler, 5.0f);

                nextFire = Time.time + fireRate;
                SetCountAmmo();
            }
        }
        if (muzzleFlashEnabled && muzzleFlashTimer > 0)
        {
            MuzzleGreenFlashObject.SetActive(true);
            MuzzleGreenFlashObject.transform.Rotate(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer < 0)
        {

            MuzzleGreenFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
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

    public void SetCountAmmo()
    {
        PistolAmmo.text = "" + Ammo;
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
    public void addAmmo(int amount)
    {
        Ammo += amount;
        //audioSrc.PlayOneShot(reload, 1f);
        PlayAudioClip(reload);
        whiteScreenFlashEnabled = true;
        SetCountAmmo();
    }
    IEnumerator FindGun()
    {
        GreenGun = GameObject.Find("MykesPlayer2Controller/Camera/Recoil/VirusGunGreen").transform;
        if (GreenGun == null)
        {
            yield return null;
        }
        else
        {
            GreenGun = GameObject.Find("MykesPlayer2Controller/Camera/Recoil/VirusGunGreen").transform;
            Recoiling();
        }
    }
    public void Recoiling()
    {
        if (Recoiled)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime / fireRate;
            //==========================ShootCannonBack================================//
            if (currentLerpTime >= fireRate)
            {
                currentLerpTime = fireRate;
            }

            GreenGun.transform.localPosition = Vector3.Lerp(GreenGuninitPos, GreenGunfinalPos, perc);

            //==========================ReturnCannonForward================================//
            if (GreenGun.transform.localPosition == GreenGunfinalPos)
            {

                if (currentLerpTime >= fireRate)
                {
                    currentLerpTime = fireRate;
                }

                GreenGun.transform.localPosition = Vector3.Lerp(GreenGunfinalPos, GreenGuninitPos, perc);
                //-------------------TurnoffLeftCannon-------------------//
                if (GreenGun.transform.localPosition == GreenGuninitPos)
                {
                    Recoiled = false;
                    currentLerpTime = 0;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VirusAmmo"))
        {
            if (Ammo <= 199)
            {
                if (other.GetComponent<Pickup_Health>())
                {
                    if (other.GetComponent<Pickup_Health>().active == true)
                    {
                        //audioSrc.PlayOneShot(reload, 1f);
                        PlayAudioClip(reload);
                        DisplayText.text = "picked up virus containers!";
                        whiteScreenFlashEnabled = true;
                        Ammo += Random.Range(20, 30);
                        FadeTime += 3;
                        AmmoShot = true;
                        SetCountAmmo();
                        other.GetComponent<Pickup_Health>().pickup();
                    }
                }
            }
        }
    }
}


