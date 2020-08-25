using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void _PowerUp(Player player);
    //protected abstract void _PowerDown(Player player);

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }

    [SerializeField] ParticleSystem _powerUpParticles;
    [SerializeField] AudioClip _powerUpSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _PowerUp(player);

            //spawn particles & sfx because we need to disable object
            Feedback();

            gameObject.SetActive(false);
        }
    }

    private void Feedback()
    {
        //particles
        if (_powerUpParticles != null)
        {
            _powerUpParticles = Instantiate(_powerUpParticles,
                transform.position, Quaternion.identity);
        }

        //audio
        //TODO - consider Object Pooling for performance
        if (_powerUpSound != null)
        {
            AudioHelper.PlayClip2D(_powerUpSound, 1f);
        }
    }
}
