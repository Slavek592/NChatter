using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NChatterServer.Main
{
    public class Server
    {
        public static void Main()
        {
            StartServer();
        }

        public static void StartServer()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPointOne = new IPEndPoint(ipAddress, 2727);
            IPEndPoint localEndPointTwo = new IPEndPoint(ipAddress, 7272);
            
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPointOne);
            listener.Listen(10);
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");  
                Socket clientSocket = listener.Accept();
                
                byte[] bytes = new Byte[1024];
                string data = null;
                
                while (true)
                { 
  
                    int numByte = clientSocket.Receive(bytes); 
                  
                    data += Encoding.ASCII.GetString(bytes, 
                        0, numByte);

                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                
                Console.WriteLine("Text received -> {0} ", data);

                Console.WriteLine("Write your message.");

                string userMessage = Console.ReadLine() + " <EOF>";

                if (userMessage == "end <EOF>")
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            
                byte[] message = Encoding.ASCII.GetBytes(userMessage);
                
                clientSocket.Send(message);
                
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }
    }
}
