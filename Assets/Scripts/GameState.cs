using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance {get; private set;}

    public Room CurrentRoom {get; private set;}
    public bool IsPlayerInNPCRange {get; private set;}

    public bool InTirednessCooldown {get; private set;}
    public bool InHygeneCooldown {get; private set;}
    public bool InHappinessCooldown {get; private set;}

    // These traits are out of 100
    public int playerTiredness; // 5 mins
    public int playerHygene; // 3 minutes
    public int playerHappiness; // 1 minute

    private float _tirednessDecaySeconds = 9f;
    private float _hygeneDecaySeconds = 6f;
    private float _happinessDecaySeconds = 3f;

    private float _tirednessTimer;
    private float _hygeneTimer;
    private float _happinessTimer;

    void OnEnable()
    {
        RoomNotifier.OnRoomStay += OnRoomStay;
        NPCController.OnEnteredRange += OnPlayerEnteredNPCRange;
        NPCController.OnExitedRange += OnPlayerExitedNPCRange;
    }

    void OnDisable()
    {
        RoomNotifier.OnRoomStay -= OnRoomStay;
        NPCController.OnEnteredRange -= OnPlayerEnteredNPCRange;
        NPCController.OnExitedRange -= OnPlayerExitedNPCRange;
    }

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

        // A high number so that it hits the default case.
        CurrentRoom = (Room)99;
        IsPlayerInNPCRange = false;

        InTirednessCooldown = false;
        InHygeneCooldown = false;
        InHappinessCooldown = false;
    }

    void Update()
    {
        DecayTraits();

        HandleTraitInteractions();

        AdjustTraitDecayRates();
    }

    //
    // Private class methods.
    //

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

    void HandleTraitInteractions()
    {
        if (Input.GetKey(KeyCode.E))
        {
            switch (CurrentRoom)
            {
                case Room.Bathroom:
                case Room.GuestBathroom:
                    if (InHygeneCooldown == true)
                    {
                        break;
                    }

                    playerHygene += 5;
                    if (playerHygene >= 100)
                    {
                        playerHygene = 100;
                    }
                    StartCoroutine(HygeneCooldown(5f));
                    break;
                case Room.LivingRoom:
                    if (InHappinessCooldown == true)
                    {
                        break;
                    }

                    playerHappiness += 3;
                    if (playerHappiness >= 100)
                    {
                        playerHappiness = 100;
                    }
                    StartCoroutine(HappinessCooldown(5f));
                    break;
                case Room.Bedroom:
                    if (InTirednessCooldown == true)
                    {
                        break;
                    }

                    if (playerTiredness <= 50)
                    {
                        playerTiredness += 50;
                        if (playerTiredness >= 100)
                        {
                            playerTiredness = 100;
                        }
                    }
                    StartCoroutine(TirednessCooldown(120f));
                    break;
            }
        }
    }

    void AdjustTraitDecayRates()
    {
        // The dirtier player is, faster their happiness decreases.
        if (playerHygene <= 50 || playerTiredness <= 50)
        {
            _happinessDecaySeconds = 1f;
        }
        else if (playerHygene <= 25)
        {
            _happinessDecaySeconds = 2f;
        }
        else
        {
            _happinessDecaySeconds = 3f;
        }
    }

    //
    // Custom events.
    //

    void OnRoomStay(Room room)
    {
        CurrentRoom = room;
    }

    void OnPlayerEnteredNPCRange()
    {
        IsPlayerInNPCRange = true;
    }

    void OnPlayerExitedNPCRange()
    {
        IsPlayerInNPCRange = false;
    }

    //
    // Coroutines
    //

    IEnumerator TirednessCooldown(float seconds)
    {
        InTirednessCooldown = true;
        yield return new WaitForSeconds(seconds);
        InTirednessCooldown = false;
    }

    IEnumerator HygeneCooldown(float seconds)
    {
        InHygeneCooldown = true;
        yield return new WaitForSeconds(seconds);
        InHygeneCooldown = false;
    }

    IEnumerator HappinessCooldown(float seconds)
    {
        InHappinessCooldown = true;
        yield return new WaitForSeconds(seconds);
        InHappinessCooldown = false;
    }
}
