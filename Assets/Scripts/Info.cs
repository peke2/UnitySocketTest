using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Info : MonoBehaviour {

	public Text myIpAddressText = null;

	void Awake()
	{
		//	画面サイズの変更テスト
		/*
		float aspect = (float)Screen.width / Screen.height;
		int w = 800;
		int h = (int)(w / aspect);
		Screen.SetResolution(w, h, true);
		*/
	}


	// Use this for initialization
	void Start ()
	{
		myIpAddressText.text = "IP Address["+ SocketComponent.getMyIPAddress() + "]";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
