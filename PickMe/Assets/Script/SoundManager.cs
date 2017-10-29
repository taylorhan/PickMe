using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource pickup;

	public void PickupPlay(){
		pickup.Play ();
	}
}
