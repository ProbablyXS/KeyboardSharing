<h1 align="center">KeyboardSharing</h1>

<p align="center">
  A simple application that captures keyboard inputs and sends them over TCP to a remote server.
</p>

## Overview

KeyboardSharing is a C# application that hooks into your keyboard to capture key presses. It then sends these captured key presses over a TCP connection to a specified server. This can be used for remote key logging, input synchronization, or other related purposes.

## Key Features

- **Keyboard Hook:** Captures all key presses on the host machine.
- **TCP Transmission:** Sends captured key press data to a specified server in real-time.
- **Persistent Connection:** Maintains a continuous TCP connection to efficiently transmit data.
- **Cross-Application Monitoring:** Works in the background and captures key presses from any application.

## Prerequisites

- **.NET Framework:** Ensure you have .NET Framework 4.x installed.
- **IDE:** Visual Studio or any C#-compatible IDE.
- **Basic Knowledge:** Familiarity with C# and TCP/IP networking concepts.

## How to Build and Use

### 1. Clone or Download the Project

If the project is hosted in a repository, clone it using Git:

```bash
git clone https://github.com/your-repo/keyboard-sharing.git
