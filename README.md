# VL-Budgerigar Code Repository

# Virtual LMS System (基于 Unity3D 的虚拟学习管理系统)

## Project introduction

This is a virtual learning system developed based on Unity3D, which aims to provide users with immersive learning experience. The system functions include virtual classroom, role customization, course management, interaction and so on. The project supports multi-person online interaction.

## Directory structure
```
VirtualLMS/
│
└ ── Assets/ # The main resource folder for the Unity project
│ ├── Scenes/ # Project scene file
│ ├── Scripts/ # C# script file
│ │ ├── NpcBehaviour_test.cs
│ │ ├── PlayerBehaviour.cs
│ │ ├── GameControl.cs
│ │ ├── InventoryUI.cs
│ │ ├── CameraBehaviour.cs
│ │ ├── ChangeColor.cs
│ │ ├── IntroSequence.cs
│ │ ├── MediaPlayer.cs
│ │ ├── ScreenController.cs
│ │ ├── PickupObject.cs
│ │ ├── door.cs
│ │ ├── Inventory.cs
│ │ ├── Mouselook.cs
│ │ ├── Player.cs
│ │ └── WhiteboardInteraction.cs
│ ├── Prefabs/ # Unity
│ ├── Models/ # 3D Model resources
│ ├── Materials/ # Materials file
│ └── Plugins/ # External plugins and third-party libraries
│
├── ProjectSettings/ # Project Configuration folder for the Unity project
├── Packages/ # Unity Package Management configuration
├── README.md # Project description file
├──.gitignore # Git Ignore files
└ -- -- LICENSE # License file
```
## Function module

**NPC Behaviour (NpcBehaviour_test.cs)**
Responsible for controlling the behavior logic of non-player characters (NPCS), including interaction, dialogue, and movement.

**Player Behaviour (PlayerBehaviour.cs)**
Defines the player's behavior and interaction logic, including the player's movement in the virtual environment, picking up items and other operations.

**Game Control (GameControl.cs)**
Manage the global control logic of the game, including scene switching, game state management, progress saving and other functions.

**Inventory UI (InventoryUI.cs)**
Implement the item bar interface in the virtual learning management system, where players can view and manage their items.

Camera Behaviour (CameraBehaviour.cs)
Control the behavior of the camera, allowing the player to rotate the Angle of view, move the camera, and more in the virtual environment.

**Change Color (ChangeColor.cs)**
Used to change the color of objects in a scene, often associated with interactive or customized features.

**Intro Sequence (IntroSequence.cs)**
Work with guided scenes in a game or learning system, showing sequence animations or tutorial content.

**Media Player (MediaPlayer.cs)**
Realize the multimedia playback function, can play video or audio resources, support the multimedia display of learning content.

**Screen Controller (ScreenController.cs)**
It is used to control screen objects in a virtual environment, usually to display content or perform screen interactions.

**Pickup Object (PickupObject.cs)**
Handles the logic of the player picking up items in the virtual environment that can be added to the inventory.

**Door Interaction (door.cs)**
Define the behavior of the door, including the interactive logic of opening and closing the door, which can trigger animation or sound effects.

**Inventory (Inventory.cs)**
Manage item data in the system and support item addition, removal and storage operations.

**Mouse Look (Mouselook.cs)**
Realize the mouse control of the first person perspective, allowing the player to control the rotation of the perspective through the mouse.

**Player (Player.cs)**
Define the player's basic attributes and functions, including the character's status and ability to interact.

**Whiteboard Interaction (WhiteboardInteraction.cs)**
Handles the interaction logic between the player and the whiteboard, allowing the player to perform actions or display content on the whiteboard.

## Prerequisites

Before you start, make sure you have met the following requirements:

- **Operating system**: Windows 10 / macOS 10.15 or later
- **Unity Version**: Unity 2021.3.5f1 or later
- **Git**: Git is required to clone code repositories and version control

## Installation guide

### Clone warehouse
Clone the project repository with Git:

```bash
git clone https://github.com/
```
### Set up the Unity project
1. Open Unity Hub and select "Add" to import the project folder.
2. After opening the project, Unity will automatically load all resources and packages.

### Project dependency
This project uses the Unity package Manager to automatically install dependent plug-ins and libraries. When you open a project, Unity automatically parses and installs the dependencies specified in the Packages/manifest.json file. If additional packages need to be installed, make sure they are listed in manifest.json.

## Instructions for use

### Build project

If you want to build the project, follow these steps:
1. In the Unity Editor, click File &gt; Build Settings... .
2. Select the target platform (for example, Windows or macOS).
3. Click the Build button and select the export path. Unity will generate the project executable file.

### Project function
• Virtual classroom: A 3D simulated classroom environment in which users can interact with learning.
Character customization: Allows users to customize the appearance of their avatars.
• Course Management: Teachers can create, modify, and delete courses.
• Multi-person interaction: Support multiple users online at the same time for real-time discussion and collaboration.