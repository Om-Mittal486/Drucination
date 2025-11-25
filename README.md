# Drucination

![Unity](https://img.shields.io/badge/Engine-Unity-blue?logo=unity)  
![Platform](https://img.shields.io/badge/Platform-PC%20%2F%20Web-lightgrey?logo=windows)  
![itch.io](https://img.shields.io/badge/Play%20on-itch.io-FA5C5C?logo=itchdotio)  
![YouTube](https://img.shields.io/badge/Watch-Walkthrough-FF0000?logo=youtube)  
![Status](https://img.shields.io/badge/Status-Playable-brightgreen)  
![Genre](https://img.shields.io/badge/Genre-Time%20Manipulation%20Platformer-orange)

A time-manipulation platformer created for **Parallax Jam**.  
Drucination blends fast-paced movement with the ability to rewind or stop time, letting players navigate challenging environments through creativity and precision.

## Table of Contents
- Introduction
- Installation
- Usage / Demo
- Features
- Dependencies
- Configuration
- Documentation / Code Structure
- Examples
- Troubleshooting
- Contributors

## Introduction
**Drucination** is a 2D story-driven pixel-art adventure set in a decaying world where time is fractured. Navigate a forgotten forest maze, using time manipulation to overcome challenges.

## Installation
Play on Itch.io: https://lucifer-playz.itch.io/drucination  
No installation required.

## Usage / Demo
Walkthrough: https://youtu.be/MGObxzTpzD4?si=O4CFdzHHqoTlGrA_

## Features
- Time-rewind ability  
- Time-stop ability  
- Tight 2D platforming  
- Puzzle-platforming  
- Atmospheric pixel art world  

## Dependencies
- Unity (2D URP)  
- C#  
- Input System  
- Cinemachine  
- VideoPlayer  
- Custom Time Control System  

## Configuration
End user: no setup.  
Developer: open in Unity.

## Documentation / Code Structure
Assets/
 ├── Scripts/
 │    ├── PlayerController.cs
 │    ├── TimeControlManager.cs
 │    ├── TimeAffectedObject.cs
 │    ├── CutsceneController.cs
 │    ├── LevelManager.cs
 │    ├── AudioManager.cs
 │    ├── UIController.cs
 │    └── ShaderEffectsController.cs
 ├── Shaders/
 ├── Scenes/
 ├── Art/
 ├── Prefabs/
 └── Cutscenes/

## Script Descriptions
- PlayerController.cs — player input and movement  
- TimeControlManager.cs — time mechanics  
- TimeAffectedObject.cs — objects influenced by time  
- CutsceneController.cs — cutscene triggers  
- LevelManager.cs — level transitions  
- AudioManager.cs — sound mixing  
- UIController.cs — UI handling  
- ShaderEffectsController.cs — shader effects  

## Troubleshooting
- Controller issues: check Input System  