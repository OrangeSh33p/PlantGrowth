using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An extremity of a chunk, that other chunks can attach to
public class Tip : MonoBehaviour {
	public List<GameObject> possibleChunks;	//The list of chunk prefabs you can attach to this tip
	GameObject attachedChunk; //The chunk that is currently attached to the tip (null of none)
}
