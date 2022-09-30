using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Button _hamburgerDisabled;
    [SerializeField] private Button _hamburgerEnabled;
    [SerializeField] private Button _pizzaDisabled;
    [SerializeField] private Button _pizzaEnabled;
    [SerializeField] private ParticleSystem _upgradeParticle;

    private int _countEnters;

    private void Start()
    {
        _upgradeParticle.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<TruckMovement>(out TruckMovement truckMovement))
        {
            if(_countEnters == 0)
            {
                _upgradeParticle.Play();
                _countEnters++;
                _hamburgerDisabled.gameObject.SetActive(false);
                _hamburgerEnabled.gameObject.SetActive(true);
            }
            else if(_countEnters == 1)
            {
                _upgradeParticle.Play();
                _countEnters++;
                _pizzaDisabled.gameObject.SetActive(false);
                _pizzaEnabled.gameObject.SetActive(true);
            }
        }
    }
}
