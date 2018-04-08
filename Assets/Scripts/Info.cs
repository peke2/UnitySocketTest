using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Info : MonoBehaviour {

	public Text hostNameText = null;

	// Use this for initialization
	void Start ()
	{
		hostNameText.text = "Host Name["+ SocketComponent.getLocalHostName() + "]";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
