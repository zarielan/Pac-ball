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

[TODO ROFL]

## Releases

### 1.1.0
- Fixed dying even upon winning
- Added the ability to start a new game
- Made sure that the timer only starts on the beginning of the actual gameplay

### 1.0.0
* First Release!