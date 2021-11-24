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
    public static SoundManager Instance = null;


    [Header("Attributes")]
    [SerializeField] List<Sound> sounds;

    [Header("Component References")]
    [SerializeField] AudioSource source;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void PlaySound(SoundType t)
    {
        source.clip = sounds.Find(x => x.type == t).sound;
        source.Play();
    }
}
