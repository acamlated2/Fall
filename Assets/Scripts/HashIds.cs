using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIds : MonoBehaviour
{
    public int playerState;

    void Awake()
    {
        playerState = Animator.StringToHash("States");
    }
}
