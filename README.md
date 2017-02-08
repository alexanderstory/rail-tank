# rail-tank
Scripts from the Unity game RailTank

These are the scripts and apks for the unreleased Android game RailTank.

GameController.cs controls the flow of the game.

PlayerController.cs controls the player character (the tank).

AimInput.cs controls the touch input for aiming the tanks gun.

TankInput.cs controls the touch input for moving the tank.

ObjectPooler.cs creates a pool of objects like enemies and bolts in order to reuse them when they're destroyed.

TextController.cs controls the text on screen.

EnemyHoverController.cs controls the regular green enemies.

EnemySniperController.cs controls the red enemies that aims at the player.

DestroyShotByBoundary.cs destroys a game object that leaves the game window.

DestroyByTime.cs destroys a game object after a certain time.

BoltMover.cs moves the bolts that are fired by the player and the enemies.

AndroidAudioController.cs plays the sound effects.
