using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace ComputerPartsServerGUI
{
    public partial class MainForm : Form
    {
        private UdpClient udpServer;
        private ConcurrentDictionary<IPEndPoint, ClientInfo> clients = new ConcurrentDictionary<IPEndPoint, ClientInfo>();
        private readonly int maxRequestsPerHour = 10;
        private readonly int maxInactiveMinutes = 10;
        private readonly int maxActiveClients = 100;
        private Thread serverThread;
        private bool isRunning = false;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateLog(string message)
        {
            Invoke((MethodInvoker)delegate
            {
                listBoxLog.Items.Add(message); 
                Logger.Info(message);         
            });
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                try
                {
                    isRunning = true;
                    serverThread = new Thread(StartServer);
                    serverThread.IsBackground = true; 
                    serverThread.Start();
                    UpdateLog("Сервер запущен.");
                }
                catch (Exception ex)
                {
                    UpdateLog($"Ошибка запуска сервера: {ex.Message}");
                }
            }
        }

        private void StopServerButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                udpServer?.Close();
                serverThread?.Abort();
                UpdateLog("Сервер остановлен.");
            }
        }

        private void StartServer()
        {
            udpServer = new UdpClient(5500);
            UpdateLog("UDP сервер запущен и ожидает подключений...");

            while (isRunning)
            {
                try
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedData = udpServer.Receive(ref clientEndPoint);
                    string request = Encoding.UTF8.GetString(receivedData);
                    UpdateLog($"Запрос от {clientEndPoint}: {request}");

                    HandleClientRequest(clientEndPoint, request);
                    CleanupInactiveClients();
                }
                catch (Exception ex)
                {
                    UpdateLog($"Ошибка при получении данных: {ex.Message}");
                }
            }
        }


        private void HandleClientRequest(IPEndPoint clientEndPoint, string request)
        {
            if (!clients.ContainsKey(clientEndPoint))
            {
                if (clients.Count >= maxActiveClients)
                {
                    UpdateLog($"Отклонено новое подключение от {clientEndPoint}: достигнуто максимальное количество активных клиентов.");
                    return;
                }

                clients.TryAdd(clientEndPoint, new ClientInfo());
            }

            ClientInfo clientInfo = clients[clientEndPoint];
            clientInfo.LastActive = DateTime.Now;

            if (clientInfo.RequestCount >= maxRequestsPerHour)
            {
                SendResponse(clientEndPoint, "Вы превысили лимит запросов на час. Пожалуйста, попробуйте позже.");
                return;
            }

            clientInfo.RequestCount++;
            string response = ProcessRequest(request);
            SendResponse(clientEndPoint, response);
        }

        private string ProcessRequest(string request)
        {
            switch (request.ToLower())
            {
                case "процессор":
                    return "Цена процессора: 200$";
                case "видеокарта":
                    return "Цена видеокарты: 400$";
                case "оперативная память":
                    return "Цена оперативной памяти: 100$";
                default:
                    return "Запчасть не найдена. Попробуйте другой запрос.";
            }
        }

        private void SendResponse(IPEndPoint clientEndPoint, string response)
        {
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            udpServer.Send(responseData, responseData.Length, clientEndPoint);
        }

        private void CleanupInactiveClients()
        {
            var inactiveClients = new List<IPEndPoint>();

            foreach (var client in clients)
            {
                if ((DateTime.Now - client.Value.LastActive).TotalMinutes > maxInactiveMinutes)
                {
                    inactiveClients.Add(client.Key);
                }
            }

            foreach (var clientEndPoint in inactiveClients)
            {
                clients.TryRemove(clientEndPoint, out _);
                UpdateLog($"Отключен клиент {clientEndPoint} из-за неактивности.");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class ClientInfo
    {
        public DateTime LastActive { get; set; } = DateTime.Now;
        public int RequestCount { get; set; } = 0;
    }
}
