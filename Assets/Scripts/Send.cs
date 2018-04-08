using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;

public class Send : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onClick()
	{
		SocketComponent sc = GetComponent<SocketComponent>();

		int port = 9081;
		string message = "Hello ワールド";

		sc.send("peke2-10", port, System.Text.Encoding.UTF8.GetBytes(message));
	}

}
