using System.Net.Sockets;
TcpClient client = new TcpClient();
client.Connect("localhost", 12000);
Console.WriteLine("Connected to server...");
// Start a thread to receive messages
Thread receiveThread = new Thread(() => ReceiveMessages(client));
receiveThread.Start();
// Send messages
StreamWriter writer = new StreamWriter(client.GetStream());
while (true)
{
    string message = Console.ReadLine();
    writer.WriteLine(message);
    writer.Flush();
}
void ReceiveMessages(TcpClient client)
{
    StreamReader reader = new StreamReader(client.GetStream());
    while (client.Connected)
    {
        string message = reader.ReadLine();
        if (message != null)
        {
            Console.WriteLine(message);
        }
    }
}