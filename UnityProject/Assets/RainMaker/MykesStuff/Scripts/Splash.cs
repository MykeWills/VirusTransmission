using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{

    //public AudioSource SplatterSound;
    public GameObject Splatter;
    public GameObject Bullet;
    void OnCollisionEnter()
    {
        GameObject expl = Instantiate(Splatter, transform.position, Quaternion.identity) as GameObject;
        //SplatterSound.Play();
        Bullet.SetActive(false);
        Destroy(gameObject); // destroy the grenade
        Destroy(expl, 3); // delete the exp
    }
}
