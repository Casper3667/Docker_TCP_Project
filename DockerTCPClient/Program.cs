using System.Net.Sockets;
TcpClient client = new();
client.Connect("localhost", 12000);
Console.WriteLine("Connected to server...");
// Start a thread to receive messages
Thread receiveThread = new(() => ReceiveMessages(client));
receiveThread.Start();
// Send messages
StreamWriter writer = new(client.GetStream());
while (true)
{
    string message = Console.ReadLine();
    writer.WriteLine(message);
    writer.Flush();
}
void ReceiveMessages(TcpClient client)
{
    StreamReader reader = new(client.GetStream());
    while (client.Connected)
    {
        string message = reader.ReadLine();
        if (message != null)
        {
            Console.WriteLine(message);
        }
    }
}