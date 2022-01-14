using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    private float _volumeModifier;
    private Coroutine _increase;
    private Coroutine _decrease;
    private WaitForSeconds _volumeChangeDelay;

    private void Awake()
    {
        _volumeModifier = 0.05f;
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

        if (_sound.volume == 0)
        {
            _sound.Stop();
        }
    }

    private IEnumerator VolumeIncrease()
    {
        while (_sound.volume < 1)
        {
            yield return _volumeChangeDelay;
            _sound.volume += _volumeModifier;
        }
    }

    private IEnumerator VolumeDecrease()
    {
        while (_sound.volume > 0)
        {
            yield return _volumeChangeDelay;
            _sound.volume -= _volumeModifier;
        }
    } 
}
