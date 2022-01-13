using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;

    private float _volumeModifier = 0.1f;
    private Coroutine _increase;
    private Coroutine _decrease;

    private void OnTriggerEnter(Collider other)
    {
        if (_decrease != null)
        {
            StopCoroutine(_decrease);
        }

        if (other.TryGetComponent<Player>(out Player player))
        {
            _alarm.Play();
            _increase = StartCoroutine(VolumeIncrease());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_increase);
        _decrease = StartCoroutine(VolumeDecrease());

        if (_alarm.volume == 0)
        {
            _alarm.Stop();
        }
    }

    private IEnumerator VolumeIncrease()
    {
        while (_alarm.volume < 1)
        {
            yield return new WaitForSeconds(0.5f);
            _alarm.volume += _volumeModifier;
        }
    }

    private IEnumerator VolumeDecrease()
    {
        while (_alarm.volume > 0)
        {
            yield return new WaitForSeconds(0.5f);
            _alarm.volume -= _volumeModifier;
        }
    } 
}
