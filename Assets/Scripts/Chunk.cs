using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A bit of the plant (root, branch, leaf...)
public abstract class Chunk : MonoBehaviour {
	public List<Tip> tips; //The tips of this chunk
}
