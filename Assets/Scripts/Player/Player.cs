using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    //TODO offload health into a Health.cs script
    //backing field
    [SerializeField] int _maxHealth = 3;
    [SerializeField] float _waitTime = 10f;
    [SerializeField] AudioClip _powerDownSound;

    public Material[] material;
    Renderer rend;

    private GameObject player;
    public Text treasureText;
    private float _currentTreasure;
    public bool invincible = false;

    //property. Can be retreived, but not set
    int _currentHealth;

    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        ProcessMovement();
        PlayerUI();
    }

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
    }

    private void PlayerUI()
    {
        treasureText.text = "Treasure: " + _currentTreasure.ToString();
    }

    public void IncreaseHealth (int amount)
    {
        //_currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _currentHealth += amount;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if (invincible == false)
        {
            _currentHealth -= amount;
            Debug.Log("Player's health: " + _currentHealth);
        }

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void IncreaseTreasure(int amount)
    {
        _currentTreasure += amount;
        Debug.Log("Player's treasure: " + _currentTreasure);
    }

    public void PowerUp()
    {
        Debug.Log("Invincibility activate!");
        invincible = true;
        rend.sharedMaterial = material[1];
        StartCoroutine("PowerUpDuration");
    }


    IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(_waitTime);
        PowerDown();
    }

    private void PowerDown()
    {
        Debug.Log("Invincibility deactivate");
        invincible = false;
        AudioHelper.PlayClip2D(_powerDownSound, 1f);
        rend.sharedMaterial = material[0];
    }

    public void Kill()
    {
        if (invincible == false)
        {
            gameObject.SetActive(false);
            //play particles
            //play sounds
        }

    }
}
