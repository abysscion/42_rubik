# Rubik's Cube solver
Ecole 42 project.

## Description
This project is about making a program able to solve any Rubik's Cube mix in a fractures of time. Program implemented in .NET Core and so could be run on different platforms (not only on Windows). It uses [Layer by layer](https://en.wikipedia.org/wiki/Layer_by_Layer) method (also known as Beginners method) containing 7 steps to solve Rubik's Cube.
*Note: This program is split by 5 steps instead of 7, because building cross and near edges is pretty straightforward, so they merged in one step on both sides.*

![Steps representation](/images/steps_representation.jpg)

## Installation
Choose one of your choice:

**Option 1**
Just download appropriate build for your OS. 

**Option 2**
You will need this first — [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download).

1. Download or `git clone` sources.
2. Run `./build.sh` to build executable.

## Usage
Program uses FRUBLD world-wide notation, familiarize yourself if needed: [link](https://ruwix.com/the-rubiks-cube/notation/)

It has two modes:

1. Instant solving returned in console if rotations sequence provided via string argument.
![Instant solve example](/images/instant_solve.gif)
2. Interactive mode is launched if no arguments provided. While in interactive mode controls are:
![Interactive mode example](/images/interactive.gif)
#### General controls
| Keyboard key | Action |
| :------------: | :------------: |
| Esc | Exit |
| Tab | Switch between interactive and command input mode |
| Q | Apply random sequence shuffle |
| Z | Solve current cube |
| P | Show performance test |
#### Cube controls
| Keyboard key | Action | Keyboard key | Action |
| :------------: | :------------: | :------------: | :------------: |
| T | Rotate **up** side **clockwise** | shift + T | Rotate **up** side **counter-clockwise** |
| M | Rotate **down** side **clockwise** | shift + M | Rotate **down** side **counter-clockwise** |
| G | Rotate **left** side **clockwise** | shift + G | Rotate **left** side **counter-clockwise** |
| J | Rotate **right** side **clockwise** | shift + J | Rotate **right** side **counter-clockwise** |
| Y | Rotate **front** side **clockwise**  | shift + Y | Rotate **front** side **counter-clockwise**  |
| N | Rotate **back** side **clockwise** | shift + N | Rotate **back** side **counter-clockwise** |