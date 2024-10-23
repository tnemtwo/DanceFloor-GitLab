using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FavoriteMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip _favoriteSong;

    private Animator _animation;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
        _animation.enabled = false;
    }

    public AudioClip StartDancing()
    {
        _animation.enabled = true;
        return _favoriteSong;
    }

    public void StopDancing()
    {
        _animation.enabled = false;
    }
}
