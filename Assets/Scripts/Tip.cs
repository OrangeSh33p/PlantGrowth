using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An extremity of a chunk, that other chunks can attach to
public class Tip : MonoBehaviour {
	public List<Chunk> possibleChunks;	//The list of chunk prefabs you can attach to this tip

	public Chunk RandomPossibleChunk () {
		return possibleChunks[Random.Range(0,possibleChunks.Count)];
	}
}
