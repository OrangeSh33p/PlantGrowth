using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A bit of the plant (root, branch, leaf...)
public class Chunk : MonoBehaviour {
	public List<Tip> tips; //The tips of this chunk
	
	//CHUNKS LIST
	private static List<Chunk> _chunks;
	public static List<Chunk> chunks { 
		get { 
			if (_chunks == null) _chunks = new List<Chunk>(); 
			return _chunks; 
		} 
	}

	void Awake () {
		chunks.Add(this);
	}

	void Destry () {
		chunks.Remove(this);
	}

	public static Chunk RandomChunk () {
		return chunks[Random.Range(0,chunks.Count)];
	}

	public Tip RandomTip () {
		return tips[Random.Range(0,tips.Count)];
	}
}
