using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name; // Tên của sound effect để gọi
        public AudioClip clip; // File âm thanh
        [Range(0f, 1f)]
        public float volume = 1f; // Âm lượng
        [Range(-3f, 3f)]
        public float pitch = 1f; // Tốc độ phát (cao độ)
        public bool loop = false; // Có lặp lại không
        public bool playOnAwake = false; // Phát khi khởi tạo không
    }

    public List<Sound> sounds; // Danh sách các sound effect

    private Dictionary<string, AudioSource> audioSourceDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ SoundManager tồn tại giữa các scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSourceDictionary = new Dictionary<string, AudioSource>();

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;
            source.playOnAwake = s.playOnAwake;

            audioSourceDictionary.Add(s.name, source);

            if (s.playOnAwake)
            {
                source.Play();
            }
        }
    }


    public void PlayBtnClickSfx()
    {
        Play("buttonClick");
    }
    
    // Phát sound effect theo tên
    public void Play(string name)
    {
        if (audioSourceDictionary.ContainsKey(name))
        {
            audioSourceDictionary[name].Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    // Dừng sound effect theo tên
    public void Stop(string name)
    {
        if (audioSourceDictionary.ContainsKey(name))
        {
            audioSourceDictionary[name].Stop();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    // Dừng tất cả các sound effect
    public void StopAll()
    {
        foreach (var source in audioSourceDictionary.Values)
        {
            source.Stop();
        }
    }

    // Kiểm tra xem sound effect có đang phát không
    public bool IsPlaying(string name)
    {
        if (audioSourceDictionary.ContainsKey(name))
        {
            return audioSourceDictionary[name].isPlaying;
        }
        Debug.LogWarning("Sound: " + name + " not found!");
        return false;
    }

    // Thay đổi âm lượng của một sound effect đang phát
    public void SetVolume(string name, float volume)
    {
        if (audioSourceDictionary.ContainsKey(name))
        {
            audioSourceDictionary[name].volume = volume;
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    // Thay đổi cao độ (pitch) của một sound effect đang phát
    public void SetPitch(string name, float pitch)
    {
        if (audioSourceDictionary.ContainsKey(name))
        {
            audioSourceDictionary[name].pitch = pitch;
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }
}