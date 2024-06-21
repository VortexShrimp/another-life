using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomTextMesh;
    [SerializeField] TextMeshProUGUI taskTextMesh;

    void OnEnable()
    {
        RoomNotifier.OnRoomEntered += OnNewRoomEntered;
    }

    void OnDisable()
    {
        RoomNotifier.OnRoomEntered -= OnNewRoomEntered;
    }
    
    void Update()
    {
        if (GameState.Instance.IsPlayerInNPCRange == true)
        {
            taskTextMesh.text = "Press 'E' to talk to your husband.";
        }
        else
        {
            switch (GameState.Instance.CurrentRoom)
            {
                case Room.Bathroom:
                case Room.GuestBathroom:
                    taskTextMesh.text = GameState.Instance.InHygeneCooldown ? "You need to wait before doing that again." : "Press 'E' to wash yourself.";
                    break;
                case Room.LivingRoom:
                    taskTextMesh.text = GameState.Instance.InHappinessCooldown ? "You need to wait before doing that again." : "Press 'E' to watch some TV.";
                    break;
                case Room.Bedroom:
                    if (GameState.Instance.playerTiredness >= 50)
                    {
                        taskTextMesh.text = "You can only nap once you are below 50% tiredness.";
                        break;
                    }
                    taskTextMesh.text = GameState.Instance.InTirednessCooldown ? "You need to wait before doing that again." : "Press 'E' to take a nap.";
                    break;
                default:
                    taskTextMesh.text = "";
                    break;
            }
        }
    }

    void OnNewRoomEntered(Room room)
    {
        roomTextMesh.text = room.ToString();
    }
}
