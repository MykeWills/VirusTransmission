using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Pickup_Health : MonoBehaviour {

    public float respawnTime = 15;
    private float timeAcquired;
    private Vector3 startLoc;
    public bool active = true;
    public List<Transform> objects;

    // Use this for initialization
    void Start() {
        foreach (Transform child in transform) {
            objects.Add(child);
        }
        timeAcquired = Time.time;
    }

    public void pickup() {
        for (int i = 0; i < objects.Count; i++) {
            objects[i].gameObject.SetActive(false);
        }
        active = false;
        timeAcquired = Time.time;
    }

    private void Update() {
        if (!active) {
            if (Time.time > timeAcquired + respawnTime) {
                for (int i = 0; i < objects.Count; i++) {
                    objects[i].gameObject.SetActive(true);
                }
                active = true;
            }
        }
    }
}
