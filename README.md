# CSC316_Project
EKU Spring 2017 - CSC316 3D Game Engine Project

## Project Proposal
The game is a 3D bullet-hell/fighting game where the player is confined to an
area which features a powerful enemy (or more than one) for player to defeat
before advancing to next area. Each enemy will have unique attack and/or
movement patterns that will require player to understand and memorize in order
to defeat. The player will not have to worry about killing lesser enemies or
performing menial tasks before finally encountering the powerful enemies
(aka boss). There will be at least two type of combats the player can choose to
use: close range (melee and the likes) or long range (projectiles like guns or
magic). The enemies could utilize either or both, or even have different kind
of mechanics that is inaccessible for player, throwing new challenges at the
player.

The game most likely will be using a fixed camera angle that won’t be movable
by player, perhaps top-down, that also will be the size of the player’s playing
area as well. The player will have 8-direction movements (up/down, left/right,
and combination of both), using WASD, however the player will always be facing
in the direction of the enemy. The spacebar will be player’s primary fire that
can deal damages to the enemy. In addition to primary spacebar, the player may
have secondary and maybe third(ary?) fire that can do greater damage at a cost
(health, time, resource, etc).

### ~~Milestone 1 - 3/24~~ COMPLETED
* Player movement
  * WASD
* Keep player within boundary of the screen 

### ~~Milestone 2 - 4/7~~ COMPLETED
* Basic combat - the player is able to perform a melee attack and shoot a
projectile
* Health system - the player and enemy has health and can lose health due to
attacks

### Milestone 3 - 4/21
* Basic AI - enemy able to make decisions based on player’s actions 
* Unique patterns for enemy - create few unique patterns of movements and
attacks for enemy that is challenging enough for player

### Milestone 4 - 5/5
* Menu
  * Allow player to pick close or long range combat
* Enemy #2 - implement unique patterns

## Caveat
If you chose to create meshes in Blender and export from Blender, by default,
it will not be rendered in game unless you set a unit (inches, meters, etc.) in
Blender. To keep consistency across all meshes, set unit to 'meter'.
