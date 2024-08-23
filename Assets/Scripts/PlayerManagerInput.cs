using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerInput : MonoBehaviour
{
    private GameObject Child;
    private PlayerInputManager player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerInputManager>();
        Child = this.gameObject.transform.GetChild(0).gameObject;
        Child = player.playerPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
