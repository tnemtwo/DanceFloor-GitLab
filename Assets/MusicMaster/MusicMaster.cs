using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicMaster : MonoBehaviour
{
    private AudioSource _audioSource;
    private List<FavoriteMusic> _favoriteMusics = new List<FavoriteMusic>();
    private int _currentIndex = 0;
    private float _timer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _favoriteMusics = FindObjectsOfType<FavoriteMusic>().ToList();

        // Randomize List
        int listCount = _favoriteMusics.Count;
        while (listCount > 1)
        {
            listCount--;
            int randomIndex = Random.Range(0, listCount + 1);
            FavoriteMusic music = _favoriteMusics[randomIndex];
            _favoriteMusics[randomIndex] = _favoriteMusics[listCount];
            _favoriteMusics[listCount] = music;
        }
    }

    private void PlayNextSong()
    {
        int previousIndex = _currentIndex;
        _currentIndex = (_currentIndex + 1 >= _favoriteMusics.Count) ? 0 : _currentIndex + 1;

        _favoriteMusics[previousIndex].StopDancing();
        AudioClip clip = _favoriteMusics[_currentIndex].StartDancing();

        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    private void Update()
    {
        if (_favoriteMusics.Count == 0)
            return;

        _timer += Time.deltaTime;
        if (_timer >= (_audioSource.clip == null ? 0 : _audioSource.clip.length))
        {
            _timer = 0;
            PlayNextSong();
        }
    }
}
