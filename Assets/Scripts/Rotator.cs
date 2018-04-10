using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("persistentDataPath=[" + Application.persistentDataPath + "]");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, 1);
	}
}
