using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputManager : MonoBehaviour
{
    private static Controls _controls = new Controls();

    public static void Init(Player MyPlayer)
    {
        _controls.Game.Jump.performed += ctx =>
        {
            MyPlayer.Jump();
        };
    }

    public static void GameMode()
    {
        _controls.Game.Enable();
    }
}
