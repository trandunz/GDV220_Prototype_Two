using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;

    public static AudioManager Instance
    { 
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Audio Manager is Null");
            }
            return _instance;
        }
    }

    private void Start()
    {
        _instance = this;
    }

    public static void PlayAudio(AudioSource _sound, Vector3 _placeToPlaySound, float _timeTillDestroyed)
    {
        Destroy(Instantiate(_sound, _placeToPlaySound, new Quaternion(0, 0, 0, 0)), _timeTillDestroyed);
    }

    public static void PlayAudioForever(AudioSource _sound, Vector3 _placeToPlaySound)
    {
        Instantiate(_sound, _placeToPlaySound, new Quaternion(0, 0, 0, 0));
    }
}
