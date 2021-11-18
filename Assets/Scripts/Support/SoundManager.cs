using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip sound;
        public SoundType type;
    }


    [Header("Attributes")]
    [SerializeField] List<Sound> sounds;

    [Header("Component References")]
    [SerializeField] AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PlaySound(SoundType t)
    {

    }
}
