using UnityEngine;

public class Coin : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private AudioSource _audioSource;

    [SerializeField] int coinAmount;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            GameManager.instance.addNumberCoins(coinAmount);
            _audioSource.PlayOneShot(_audioSource.clip);
            Destroy(gameObject, 0.5f);
        }
    }


}
