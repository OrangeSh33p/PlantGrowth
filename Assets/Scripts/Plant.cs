using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	void Grow (Chunk chunk, Transform parentTip) {
		Instantiate (
			chunk,
			parentTip.position,
			parentTip.rotation,
			parentTip
		);
	}
}
