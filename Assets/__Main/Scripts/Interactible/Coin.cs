using UnityEngine;

public class Coin : MonoBehaviour
{
    private BoxCollider _collider;
    private AudioSource _audioSource;

    [SerializeField] int coinAmount;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _collider.enabled = false;
            GameManager.instance.addNumberCoins(coinAmount);
            _audioSource.PlayOneShot(_audioSource.clip);
            Destroy(gameObject, 0.5f);
        }
    }


}
