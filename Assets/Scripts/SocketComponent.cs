using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Threading;

public class SocketComponent : MonoBehaviour
{

	Socket listener = null;

	const int PORT = 9081;

	ManualResetEvent allDone;

	bool isReceived = false;

	public bool enableSend = false;


	// Use this for initialization
	void Start()
	{

		string hostName = Dns.GetHostName();
		IPAddress[] addresses = Dns.GetHostAddresses(hostName);
		Debug.Log("ホスト名[" + hostName + "]");

		for(int i = 0; i < addresses.Length; i++)
		{
			IPAddress addr = addresses[i];
			Debug.Log("IPアドレス[" + addr.ToString() + "] Address Family[" + addr.AddressFamily.ToString() + "]");
		}

		if(enableSend == false)
		{
			StartCoroutine(receive());
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnDestroy()
	{
		if(listener != null)
		{
			listener.Close();
		}
	}

	IEnumerator receive()
	{
		//IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
		//IPAddress ipAddress = ipHostInfo.AddressList[0];
		IPAddress ipAddress = getMyIPAddress();
		IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

		listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		try
		{
			listener.Bind(localEndPoint);
			listener.Listen(100);
		}
		catch(System.Exception e)
		{
			Debug.Log(e.Message);
		}


		while(true)
		{
			//allDone.Reset();

			isReceived = false;
			listener.BeginAccept(new System.AsyncCallback(acceptCallback), listener);

			while(isReceived == false)
			{
				yield return null;
			}

			//allDone.WaitOne();

			//yield return null;
		}

	}

	void acceptCallback(System.IAsyncResult ar)
	{
		//allDone.Set();

		Socket socket = (Socket)ar.AsyncState;
		Socket handler = socket.EndAccept(ar);

		byte[] buffer = new byte[512];

		//[todo] データ受信時のバッファサイズ取得と確保
		int byteCount;
		byteCount = handler.Receive(buffer);


		string receivedText = System.Text.Encoding.UTF8.GetString(buffer);
		Debug.Log("受信：" + receivedText);

		isReceived = true;
	}


	public void send(byte[] ipAddr, int port, byte[] data)
	{
		IPAddress ipAddress = new IPAddress(ipAddr);
		IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, port);

		Socket socket;
		socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

		socket.Connect(remoteEndPoint);
		socket.Send(data, SocketFlags.None);
		socket.Shutdown(SocketShutdown.Both);
		socket.Close();
	}

	public static IPAddress getMyIPAddress()
	{
		IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());

		for(int i=0; i<addresses.Length; i++)
		{
			IPAddress a = addresses[i];

			if(a.AddressFamily == AddressFamily.InterNetwork)
			{
				//	LANのプライベートアドレスを判別
				//	ローカル内のプライベートアドレスは引っかからないようにする
				string ipstr = a.ToString();
				if(ipstr.StartsWith("192.168.0.") || ipstr.StartsWith("172.16."))
				{
					return a;
				}
			}
		}
		return null;
	}
}
