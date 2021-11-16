using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioClips : MonoBehaviour
{
        public AudioClip playerWalking; //make sure you assign an actual clip here in the inspector
        [SerializeField] Transform player;
    
}
