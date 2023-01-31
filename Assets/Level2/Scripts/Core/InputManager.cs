using System.Collections;
using System.Collections.Generic;
using Backtrack.Core;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Returns the GameManager.
    public static InputManager Instance => s_Instance;
    static InputManager s_Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput = new Vector2(0, 0);
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.Move(playerInput);
        }
    }
}
