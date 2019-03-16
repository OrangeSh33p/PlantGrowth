using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown(KeyCode.G)) {
			Tip randomTip = Chunk.RandomChunk().RandomTip();
			if (randomTip) Grow (randomTip.RandomPossibleChunk(), randomTip.transform);
		}
	}

	void Grow (Chunk chunk, Transform parentTip) {
		Debug.Log("Spawning "+chunk.name+" on "+parentTip.name);
		Instantiate (
			chunk,
			parentTip.position,
			parentTip.rotation * Quaternion.Euler(0,0,Random.Range(-60f,60f)),
			parentTip.parent
		);
		Destroy(parentTip.gameObject);
		parentTip.parent.parent.GetComponent<Chunk>().tips.Remove(parentTip.GetComponent<Tip>());
	}
}
