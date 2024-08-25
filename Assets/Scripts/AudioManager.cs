using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource ringAudio;
    // Start is called before the first frame update
    void Start()
    {
        ringAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeRingChime()
    {

        ringAudio.Play();
    }
}
