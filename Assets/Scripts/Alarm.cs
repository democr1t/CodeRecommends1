using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    private float _maxVolume;
    private float _minVolume;
    private float _volumeModifier;
    private WaitForSeconds _volumeChangeDelay;
    private Coroutine _increase;
    private Coroutine _decrease;
    

    private void Awake()
    {
        _volumeModifier = 0.05f;
        _maxVolume = 1.0f;
        _minVolume = 1.0f;
        _volumeChangeDelay = new WaitForSeconds(0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_decrease != null)
        {
            StopCoroutine(_decrease);
        }

        if (other.TryGetComponent<Player>(out Player player))
        {
            _sound.Play();
            _increase = StartCoroutine(VolumeIncrease());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_increase);
        _decrease = StartCoroutine(VolumeDecrease());

        if (_sound.volume == _minVolume)
        {
            _sound.Stop();
        }
    }

    private IEnumerator VolumeIncrease()
    {
        while (_sound.volume < _maxVolume)
        {
            yield return _volumeChangeDelay;
            _sound.volume += _volumeModifier;
        }
    }

    private IEnumerator VolumeDecrease()
    {
        while (_sound.volume > _minVolume)
        {
            yield return _volumeChangeDelay;
            _sound.volume -= _volumeModifier;
        }
    } 
}
