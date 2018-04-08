using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Info : MonoBehaviour {

	public Text myIpAddressText = null;

	// Use this for initialization
	void Start ()
	{
		myIpAddressText.text = "IP Address["+ SocketComponent.getMyIPAddress() + "]";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
