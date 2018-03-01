﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public int id;
	public states state;
	public MoveObject move;
	public TurnObject turnObject;
	public RotatingLoop rotatingLoop;

	public enum states
	{
		MOVE,
		IDLE,
		ROTATING
	}
	public void SetID(int id)
	{
		GetComponent<Colorate> ().SetOn (id);
		this.id = id;
	}
	void Update () {
		if (state == states.ROTATING)
			turnObject.OnUpdate ();
		else if (state == states.MOVE)
			move.OnUpdate();
		if (Mathf.Abs (transform.position.x) > 15 || Mathf.Abs (transform.position.y) > 15)
			Game.Instance.RemoveCar (this);
	}
	void OnTriggerEnter2D(Collider2D other)
	{		
		
		Flag flag = other.GetComponent<Flag> ();
		if (flag != null)
			flag.SetOn (id);
		
		ActivateOnTouch aactivateOnTouch = other.GetComponent<ActivateOnTouch> ();
		if (aactivateOnTouch != null)
			aactivateOnTouch.SetOn ();

		Bomb bomb = other.GetComponent<Bomb> ();
		Turn turn = other.GetComponent<Turn> ();
		RotationArrow rotationArrow = other.GetComponent<RotationArrow> ();

		if (turn != null) {	
			transform.position = other.transform.position;
			Vector3 rot = other.transform.eulerAngles;
			turnObject.Init (rot.z);
			RotateTo ();
		} else if (rotationArrow != null) {
			rotatingLoop.speed = rotationArrow.speed;
			rotatingLoop.enabled = true;
		} else if (bomb != null && bomb.carId == id)
			Game.Instance.RemoveCar (this);
	}
	void OnTriggerExit2D(Collider2D other)
	{		
		RotationArrow rotationArrow = other.GetComponent<RotationArrow> ();
		if (rotationArrow != null) {
			rotatingLoop.enabled = false;
		}
	}
	public void OnStateDone()
	{
		state = states.MOVE;
	}
	void RotateTo()
	{
		move.SetSpeed (0.5f);
		state = states.ROTATING;
	}
}