using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFJK : MonoBehaviour
{
    public SpriteRenderer sr;//便于给按键变颜色
    public ParticleSystem SnowEffect;
    public AudioSource audioSource;
    public AudioClip snowClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        snowClip = GetComponent<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
