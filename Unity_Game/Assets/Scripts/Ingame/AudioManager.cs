using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    // ** ����� Ŭ��
    public AudioClip bgmClip;
    // ** ����� ����
    public float bgmVolume;
    // ** ����� �÷��̾�
    AudioSource bgmPlayer;
    // ** ����� ȿ��
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    // ** ȿ���� Ŭ��
    public AudioClip[] sfxClips;
    // ** ȿ���� ����
    public float sfxVolume;
    // ** ȿ���� ä��
    public int channels;
    // ** ȿ���� �÷��̾�
    AudioSource[] sfxPlayers;
    // ** ȿ���� ä�� ���
    int channelIndex;
    // ** ȿ���� ���
    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win}

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // ** ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // ** ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    // ** ����� ���
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    // ** ����� ȿ��
    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    // ** ȿ���� ���
    public void PlayeSfx(Sfx sfx)
    {
        for (int index=0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}