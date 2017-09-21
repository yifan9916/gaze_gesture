using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
	private AudioSource _musicSource;
	public AudioClip bgm;
	private AudioSource _ambientSource;
	private List<AudioSource> _audioSourceList;

	void Awake ()
	{
		_musicSource = gameObject.AddComponent<AudioSource> ();
		_musicSource.playOnAwake = false;
		_musicSource.loop = true;

		_ambientSource = gameObject.AddComponent<AudioSource> ();
		_ambientSource.playOnAwake = false;
		_ambientSource.loop = true;
		_ambientSource.rolloffMode = AudioRolloffMode.Linear;

		_audioSourceList = new List<AudioSource> ();
		_audioSourceList.Add (_musicSource);
		_audioSourceList.Add (_ambientSource);
	}

	void Update ()
	{
		PlayBGMusic ();
	}

	public void PlayBGMusic ()
	{
		if (!_musicSource.isPlaying) 
		{
			_musicSource.Stop ();
			_musicSource.clip = bgm;
			_musicSource.Play ();
		}
	}

	public void StopBGMusic ()
	{
		_musicSource.Stop ();
		_musicSource.clip = null;
	}

	public void SetVolume (float volume)
	{
		for (int i = 0; i < _audioSourceList.Count; i++) 
		{
			_audioSourceList [i].volume = volume;
		}
	}
}