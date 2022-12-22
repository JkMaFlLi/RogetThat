using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatServer
{
    TcpListener server;
    TcpClient client;

    public ChatServer()
    {
        // Create a new TcpListener object and start listening for incoming connections on IP address 0.0.0.0 and port 5000
        server = new TcpListener(IPAddress.Any, 5000);
        server.Start();

        // Accept an incoming connection and create a TcpClient object for it
        client = server.AcceptTcpClient();

        // Get the network stream from the TcpClient object
        NetworkStream stream = client.GetStream();

        // Create a StreamReader and StreamWriter to read and write strings over the network
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);

        // Display the ASCII art
        Console.WriteLine(@"
 _______ __ __  ______   ______   ______  ______   ______ 
/       |  |  |/      | /      | /      |/      | /      |
$$$$$$/   $$/  $$$$$$/ /$$$$$$ |/$$$$$$/ $$$$$$/ /$$$$$$ |
  $$ | __ $$ |$$ |__$$/ $$ |  $$/$$ |__$$/$$ |__$$/$$ |  $$
 _$$ |/  |$$ |$$   
/$$    |$$ |$$$$$$/$$ |$$    |/$$$$$$/$$    |$$ |  $$
/$$$$$$/$$/ $$ |_____ $$ | $$$$$$/ $$$$$$/ $$$$$$$/$$/   $$/
$$    $$/  $$       |$$ |/     $$/$$    $$/$$       |
 $$$$$$/    $$$$$$$/ $$/ $$$$$$$/  $$$$$$/  $$$$$$$/ $$/
                                                         ");

        // Start a loop to continuously read input from the client and write it to the console
        while (true)
        {
            // Read a line of text from the client
            string message = reader.ReadLine();

            // If the message is "goodbye", break out of the loop and end the program
            if (message.ToLower() == "goodbye")
            {
                break;
            }

            // Write the message to the console with a timestamp
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Client: " + message);

            // Read a line of text from the console
            message = Console.ReadLine();

            // Send the message to the client with a timestamp
            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Server: " + message);
            writer.Flush();
        }

        // Close the TcpClient and TcpListener objects
        client.Close();
        server.Stop();
    }

    static void Main(string[] args)
    {
        new ChatServer();
    }
}
