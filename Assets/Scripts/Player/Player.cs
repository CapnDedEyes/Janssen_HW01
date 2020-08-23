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

    Vector3 offset;

    public Text treasureText;
    private float _currentTreasure;

    //property. Can be retreived, but not set
    int _currentHealth;

    /*public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set
        {
            //value represents the new value we're trying to set
            //make sure new value is not above max
            if(value > _maxHealth)
            {
                value = _maxHealth;
            }
            //assign the newly adjusted value
            _currentHealth = value;
        }
    }*/

    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
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
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
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

    public void Bounce()
    {
        transform.position = _ballMotor.transform.position + offset;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        //play particles
        //play sounds
    }
}
