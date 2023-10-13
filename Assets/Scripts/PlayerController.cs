using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    private float _powerupStrength = 15.0f;

    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;

    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject rocketPrafab;
    private GameObject _tmpRocket;
    private Coroutine _powerCountdown;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position
            + new Vector3(0, -0.5f, 0);

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) { 
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (_powerCountdown != null)
            {
                StopCoroutine(_powerCountdown);
            }

            _powerCountdown = StartCoroutine(PowerupCountdownRoutine());

        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && 
            currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = 
                collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =
                (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * _powerupStrength,
                ForceMode.Impulse);

            Debug.Log("Player collided with: " + collision.gameObject.name 
                + " withpowerup set to " + currentPowerUp.ToString());

        }
    }

    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            _tmpRocket = Instantiate(rocketPrafab,transform.position + Vector3.up,
                Quaternion.identity);
            _tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
}
