using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

public class KeyboardSimulator
{
    // Import the user32.dll to use the keybd_event function
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    private const int KEYEVENTF_KEYDOWN = 0x0000;
    private const int KEYEVENTF_KEYUP = 0x0002;

    public static void Main()
    {
        TcpListener server = null;
        try
        {
            int port = 8888;
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");

            server = new TcpListener(localAddr, port);
            server.Start();

            Console.WriteLine("Waiting for a connection...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] bytes = new byte[256];
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string input = Encoding.UTF8.GetString(bytes, 0, i).Trim();
                    Console.WriteLine($"Received: {input}");

                    // Split the input into keys based on some delimiter, e.g., comma
                    string[] keys = input.Split(',');

                    foreach (string key in keys)
                    {
                        string trimmedKey = key.Trim();
                        byte? vkCode = ConvertKeyToVirtualKey(trimmedKey);
                        if (vkCode.HasValue)
                        {
                            SimulateKeyPress(vkCode.Value);
                        }
                        else
                        {
                            Console.WriteLine($"Unsupported key: {trimmedKey}");
                        }
                    }
                }

                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"SocketException: {e}");
        }
        finally
        {
            server?.Stop();
        }
    }

    // Convert key string to virtual key code (VK code)
    private static byte? ConvertKeyToVirtualKey(string key)
    {
        switch (key.ToUpper())
        {
            case "A": return 0x41;
            case "B": return 0x42;
            case "C": return 0x43;
            case "D": return 0x44;
            case "E": return 0x45;
            case "F": return 0x46;
            case "G": return 0x47;
            case "H": return 0x48;
            case "I": return 0x49;
            case "J": return 0x4A;
            case "K": return 0x4B;
            case "L": return 0x4C;
            case "M": return 0x4D;
            case "N": return 0x4E;
            case "O": return 0x4F;
            case "P": return 0x50;
            case "Q": return 0x51;
            case "R": return 0x52;
            case "S": return 0x53;
            case "T": return 0x54;
            case "U": return 0x55;
            case "V": return 0x56;
            case "W": return 0x57;
            case "X": return 0x58;
            case "Y": return 0x59;
            case "Z": return 0x5A;
            case "0": return 0x30;
            case "1": return 0x31;
            case "2": return 0x32;
            case "3": return 0x33;
            case "4": return 0x34;
            case "5": return 0x35;
            case "6": return 0x36;
            case "7": return 0x37;
            case "8": return 0x38;
            case "9": return 0x39;
            // Add more cases for other keys if needed
            default: return null; // Return null if the key is not supported
        }
    }

    // Simulate a key press using the virtual key code
    private static void SimulateKeyPress(byte vkCode)
    {
        keybd_event(vkCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
        keybd_event(vkCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
    }
}
