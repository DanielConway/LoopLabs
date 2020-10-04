using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public void PlayClip(AudioClip c){
        source.clip=c;
        source.Play();
    }

}
