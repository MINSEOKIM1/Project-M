using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmPlayer;

    [SerializeField] private AudioClip bgmClip;
    void Start()
    {
        bgmPlayer.clip = bgmClip;
        BgmStart();
    }

    void BgmStart()
    {
        bgmPlayer.Play();
    }
}
