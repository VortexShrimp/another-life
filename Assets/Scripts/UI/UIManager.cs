using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomTextMesh;

    void OnEnable()
    {
        RoomNotifier.OnNewRoomEntered += OnNewRoomEntered;
    }

    void OnDisable()
    {
        RoomNotifier.OnNewRoomEntered -= OnNewRoomEntered;
    }

    void OnNewRoomEntered(Room room)
    {
        roomTextMesh.text = room.ToString();
    }
}
