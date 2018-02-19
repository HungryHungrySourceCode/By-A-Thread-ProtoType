using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour {
    public Material mat;
    public Vector2 speed;

	void Update () {
        mat.mainTextureOffset += speed * Time.deltaTime;
	}
}
