using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event System.Action OnMove;
    public Button moveButton;
    public Character character;

    private void Awake()
    {
        OnMove += character.MoveNextTile;
        moveButton.onClick.AddListener(OnMove.Invoke);
    }
}
