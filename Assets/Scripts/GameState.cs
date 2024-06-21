using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance {get; private set;}

    // These traits are out of 100
    public int playerTiredness; // 5 mins
    public int playerHygene; // 3 minutes
    public int playerHappiness; // 1 minute

    private const float _tirednessDecaySeconds = 30f;
    private const float _hygeneDecaySeconds = 18f;
    private const float _happinessDecaySeconds = 6f;

    private float _tirednessTimer;
    private float _hygeneTimer;
    private float _happinessTimer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _tirednessTimer = Time.time;
        _hygeneTimer = Time.time;
        _happinessTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        DecayTraits();
    }

    // Incrementally decay the traits as time goes on.
    void DecayTraits()
    {
        _tirednessTimer += Time.deltaTime;
        _hygeneTimer += Time.deltaTime;
        _happinessTimer += Time.deltaTime;

        if (_tirednessTimer >= _tirednessDecaySeconds)
        {
            playerTiredness -= 1;

            if (playerTiredness <= 0)
            {
                playerTiredness = 0;
            }

            _tirednessTimer = 0;
        }

        if (_hygeneTimer >= _hygeneDecaySeconds)
        {
            playerHygene -= 1;

            if (playerHygene <= 0)
            {
                playerHygene = 0;
            }

            _hygeneTimer = 0;
        }

        if (_happinessTimer >= _happinessDecaySeconds)
        {
            playerHappiness -= 1;

            if (playerHappiness <= 0)
            {
                playerHappiness = 0;
            }

            _happinessTimer = 0;
        }
    }
}
