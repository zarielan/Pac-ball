# Pac-ball!

A game by Luiz!

## Overview

The game is based off of PAC-MAN, wherein a player navigates through a maze, collecting points as he avoids all four ghosts. The game that I made is similar in terms of mechanics, basing my making of the game from Unity’s Roll-a-ball tutorial.

The player is a ball. Using the arrow keys (or WASD keys) to navigate, you must collect all 80 points in the maze, while avoiding all four red enemy balls. A collision with an enemy ball will kill you. You will win if you manage to collect all points without dying.

Each ball has its own path it follows.

In the maze, there are green spots. These are safe spaces, because the enemy balls don't travel there.

## Components

* **Rigidbody**. This is a component that I added to all moving objects in the scene. The player ball and all enemy balls have this component. What does this is to allow the object to be affected by physics.

* **Script**. This is an important component, because essentially we control the game from these. I put all code logic into scripts I attach to each object, that allow them to do certain things like, move when I press a key, do this when I click, or disappear upon collision, among many others.

* **Canvas Group**. I used this component for the UI. What this does is it allows me to hide or show a UI by changing its alpha and block raycasts option. By setting the alpha to 0 and set it to not block raycasts, all its child UI objects will effectively be hidden. Doing the opposite would show them.

## Scripts

### Camera
This script makes the camera follow the player ball as it moves in its x and z axis. I used `Vector3.SmoothDamp` so that the movement of the camera is smooth, with a bit of some elasticity to it.

````C#
public GameObject player;
private Vector3 velocity = Vector3.zero;
private float fixedY;

private void Start()
{
	fixedY = transform.position.y;
}

void LateUpdate()
{
	Vector3 newPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.1f);
	newPos.y = fixedY;
	transform.position = newPos;
}
````

### Player
The player ball gets pushed with a force by `GameObject.AddForce`, but only if the player hasn't picked up all the pickups yet, and if it should be asking inputs, and if it isn't dead yet. If the player collides with a pickup, it adds to its points and makes the pickup disappear. If instead, it collides with an enemy ball, it dies.

````C#
private Rigidbody player;
private Vector3 move;
public float speed;
public int points;
private int totalPickups;
public bool acceptInputs;
public bool isDead;

void Start ()
{
	player = GetComponent<Rigidbody>();
	points = 0;
	acceptInputs = false;
	isDead = false;

	totalPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
}

private void FixedUpdate()
{
	if (points < totalPickups && acceptInputs && !isDead)
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		move = new Vector3(moveHorizontal, 0f, moveVertical);

		player.AddForce(move * speed);
	}
}

private void OnTriggerEnter(Collider other)
{
	if (other.gameObject.CompareTag("Pick Up"))
	{
		other.gameObject.SetActive(false);
		points++;
	}

	if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy2")))
	{
		isDead = true;
	}
}
````

### Enemy (1-4)
The enemy class mostly contains its code to move along its path. Based off of [this code from the Unity Community Forums](https://forum.unity.com/threads/making-objects-move-in-repeating-path.438404/). Basically, its route is hardcoded. It starts a Coroutine wherein until a ball reaches its next waypoint, it must move straight to it. Once it has reached it, the waypoint will move to the next one, and the process repeats all over again. This script also makes it so that it ignores collisions with other enemy balls.

````C#
public float speed;
private Vector3[] waypoints;
private int i;
private const float minDistance = 0f;

private void Start()
{
	i = 0;

	waypoints = new Vector3[] {
		// List of all points to go through
		// Different for each ball
	};

	StartCoroutine(Move());
}

private void GetNewWaypoint()
{
	int newWaypoint = i + 1;

	if (newWaypoint > waypoints.Length - 1)
		newWaypoint = 0;

	i = newWaypoint;

	StartCoroutine(Move());
}

private IEnumerator Move()
{
	float distance = Vector3.Distance(transform.position, waypoints[i]);

	while (distance > minDistance)
	{
		distance = Vector3.Distance(transform.position, waypoints[i]);

		transform.position = Vector3.MoveTowards(transform.position, waypoints[i], speed * Time.deltaTime);

		if (distance <= minDistance)
		{
			GetNewWaypoint();
		}

		yield return null;
	}
}

private void OnCollisionEnter(Collision collision)
{
	if (collision.gameObject.CompareTag("Enemy2") || collision.gameObject.CompareTag("Enemy"))
	{
		Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
	}
}
````

[TODO ROFL]

## Releases

### 1.1.0
- Fixed dying even upon winning
- Added the ability to start a new game
- Made sure that the timer only starts on the beginning of the actual gameplay

### 1.0.0
* First Release!