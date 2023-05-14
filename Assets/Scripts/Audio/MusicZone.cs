using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{

    public AudioSource audioSource;
    public float fadeTime;//müzik yavaþça baþlasýn diye
    private float targetVolum;


    private void Start()
    {
        //oyunun baþýnda þarký baþlasýn istemiyoruz
        targetVolum = 0.0f;
        audioSource.volume = 0.0f;
    }

    private void Update()
    {
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolum, (1.0f / fadeTime) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolum = 1.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolum = 0.0f;
        }
    }



}
