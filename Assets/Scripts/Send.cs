using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Net;

public class Send : MonoBehaviour {

	public InputField inputIPAddress;

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

		byte[] ip;

		ip = retrieveIpAddress();

		sc.send(ip, port, System.Text.Encoding.UTF8.GetBytes(message));
	}


	byte[] retrieveIpAddress()
	{
		byte[] ip = new byte[] { 127,0,0,1 };

		//	かなり適当なパース
		if(inputIPAddress != null)
		{
			string[] ips = inputIPAddress.text.Split(new char[] { '.' });
			if(ips.Length == 4)
			{
				for(int i = 0; i < ips.Length; i++)
				{
					int n;
					if(Int32.TryParse(ips[i], out n))
					{
						ip[i] = (byte)n;
					}
				}
			}
		}

		return ip;
	}

}
