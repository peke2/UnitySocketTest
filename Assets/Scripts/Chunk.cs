using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class Chunk
{
	public static void writeToFile(byte[] data)
	{
		Int32 nameLength;
		nameLength = BitConverter.ToInt32(data, 0); 

		int nameIndex = sizeof(Int32); 
		string fileName = System.Text.Encoding.UTF8.GetString(data, nameIndex, nameLength);

		string path = Application.persistentDataPath;
		string fullPath = Path.Combine(path, fileName);

		int dataIndex = nameIndex + nameLength;
		Int32 dataLength = BitConverter.ToInt32(data, dataIndex); 
		dataIndex += sizeof(Int32); 

		if(File.Exists(fullPath)) {
			File.Delete(fullPath);
		}
		using(FileStream fs = File.Create(fullPath)) {
			fs.Write(data, dataIndex, dataLength);
		}
	}

	public static byte[] createChunk(string name, byte[] data)
	{
		int total = 0;
		int nameLength = name.Length;
		int dataLength = data.Length;

		total += sizeof(Int32);
		total += nameLength;
		total += sizeof(Int32);
		total += dataLength;

		byte[] output = new byte[total];

		byte[] nl = BitConverter.GetBytes(nameLength);
		byte[] dl = BitConverter.GetBytes(dataLength);
		byte[] nm = System.Text.Encoding.UTF8.GetBytes(name);

		int offset = 0;
		Buffer.BlockCopy(nl, 0, output, 0, sizeof(Int32));
		offset += sizeof(Int32);
		Buffer.BlockCopy(nm, 0, output, offset, nameLength);
		offset += nameLength;
		Buffer.BlockCopy(dl, 0, output, 0, sizeof(Int32));
		offset += sizeof(Int32);
		Buffer.BlockCopy(data, 0, output, offset, dataLength);
		offset += dataLength;

		return output;
	}
}
