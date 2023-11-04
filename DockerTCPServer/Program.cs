using System.Net;
using System.Net.Sockets;
TcpListener server = new(IPAddress.Any, 12000);
server.Start();
Console.WriteLine("Server started... listening on port 12000");
while (true)
{
    TcpClient client = server.AcceptTcpClient();
    Thread clientThread = new(() => HandleClient(client));
    clientThread.Start();
}
void HandleClient(TcpClient client)
{
    Guid clientId = Guid.NewGuid();
    Console.WriteLine($"Client {clientId} connected.");
    // Send client name to the client
    StreamWriter writer = new(client.GetStream());
    writer.Flush();
    // Receive and send messages
    StreamReader reader = new(client.GetStream());
    try
    {
        while (client.Connected)
        {
            string message = reader.ReadLine();
            if (message != null)
            {
                Console.WriteLine($"Received message from {clientId}: {message}");
                //Echoing message
                writer.WriteLine(message + " From server");
                writer.Flush();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception occurred for client {clientId}: {ex.Message}");
    }
    finally
    {
        Console.WriteLine($"Client disconnected: {clientId}");
        client.Dispose();
    }
}