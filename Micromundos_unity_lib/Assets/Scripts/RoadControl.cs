﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControl : MonoBehaviour {

	public int id;
	public Camera cam;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (ClientData.Instance.IsBlock(id)) {

			transform.eulerAngles = ClientData.Instance.GetBlockRotation(id);

			transform.localPosition = ClientData.Instance.GetBlockPositionAtNearPlane (id, cam);

		}
	}

	void OnCollisionEnter(Collision col){
		print (col.collider.name);
	}
}
