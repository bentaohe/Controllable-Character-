using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity {

    Animator anim;

    // Controls
    PlayerControls controls;

    // Movement
    Vector3 globalDir;

    // Status
    bool moving;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += dir => SetDir(dir.ReadValue<Vector2>());
        controls.Player.Move.canceled += _ => CancelMovement();
    }

    // Use this for initialization
    void Start ()
	{
        anim = GetComponent<Animator>(); 
	}

    void Update()
    {
        if (moving) Move(globalDir);
    }

    void SetDir(Vector2 dir)
    {
        moving = true;

        globalDir = dir;
    }

    // Move sprite in given direction
	void Move(Vector3 dir)
    {
		transform.position += dir * speed * Time.deltaTime;

		anim.SetFloat("MoveX", dir.x);
		anim.SetFloat("MoveY", dir.y);
    }

    void CancelMovement()
    {
        moving = false;

        // Cancel animator animations
        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", 0);
    }

    void OnEnable() { controls.Enable(); }
    void OnDisable() { controls.Disable(); }
}
