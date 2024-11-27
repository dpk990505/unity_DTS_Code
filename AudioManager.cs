using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx//sfxClips에 들어간 오디오 순서대로 열거
    {
        Dead, Hit, LevelUp=3, Lose,  Melee, Range=7, select, Win
    }

    private void Awake()
    {
        Instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //호과음 플레이어 초기화

        GameObject sfxobject = new GameObject("Sfxplayer");
        sfxobject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++) {
            sfxPlayers[i] = sfxobject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake=false;
            sfxPlayers[i].bypassEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }

    }    

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }


    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0;i < sfxPlayers.Length; i++) { 
            int loopIndex =(i+channelIndex)%sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;
            if(sfx == Sfx.Hit|| sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0,2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
