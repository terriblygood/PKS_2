using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections.ObjectModel;
using System.IO;



namespace Network;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LoadNetworkInterfaces();
        LoadUrlHistory();
    }
    private void LoadUrlHistory()
    {
        if (File.Exists(HistoryFilePath))
        {
            var lines = File.ReadAllLines(HistoryFilePath);
            foreach (var line in lines)
            {
                urlHistory.Add(line);
            }
            lbUrlHistory.ItemsSource = urlHistory;
        }
    }
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        base.OnClosing(e);
        SaveUrlHistory();
    }

    private void SaveUrlHistory()
    {

        File.WriteAllLines(HistoryFilePath, urlHistory);
    }

    private void LoadNetworkInterfaces()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        Interfaces.ItemsSource = interfaces;
        Interfaces.DisplayMemberPath = "Name"; 
    }
    private void Interfaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Interfaces.SelectedItem is NetworkInterface ni)
        {
            InterfaceDetails.Children.Clear();

            var ipProps = ni.GetIPProperties();

            AddDetail($"Имя: {ni.Name}");
            AddDetail($"Описание: {ni.Description}");
            AddDetail($"Тип: {ni.NetworkInterfaceType}");
            AddDetail($"Состояние: {ni.OperationalStatus}");
            AddDetail($"Скорость: {ni.Speed} бит/с");
            AddDetail($"MAC-адрес: {ni.GetPhysicalAddress()}");
            
            foreach (var unicast in ipProps.UnicastAddresses) // Перебираем все IP-адреса
            {
                if (unicast.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    AddDetail($"IP: {unicast.Address}");
                    AddDetail($"Маска подсети: {unicast.IPv4Mask}");
                }
            }
        }
    }

    private void AddDetail(string text)
    {
        InterfaceDetails.Children.Add(new TextBlock { Text = text, Margin = new Thickness(0, 2, 0, 2) }); // Добавляем текстовый блок в панель
    }
    private void AnalyzeUrl_Click(object sender, RoutedEventArgs e)
    {
        string url = tbUrlInput.Text.Trim();
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) && uri != null)
        {
            string result = $"Схема: {uri.Scheme}\n" +
                            $"Хост: {uri.Host}\n" +
                            $"Порт: {uri.Port}\n" +
                            $"Путь: {uri.AbsolutePath}\n" +
                            $"Параметры запроса: {uri.Query}\n" +
                            $"Фрагмент: {uri.Fragment}\n";
            result += AnalyzeHost(uri.Host);

            tbUrlResults.Text = result;
            SaveUrlToHistory(url);
        }
        else
        {
            tbUrlResults.Text = "Некорректный URL.";
        }
    }
    private string AnalyzeHost(string host)
    {
        string result = "";
        try
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(host, 2000); // таймаут 2000 мс
            result += $"Ответ ping: {(reply.Status == IPStatus.Success ? "Доступен" : "Не доступен")}\n";

            //DNS
            IPAddress[] addresses = Dns.GetHostAddresses(host);
            result += "DNS-адреса: " + string.Join(", ", addresses.Select(ip => ip.ToString())) + "\n";
            //Адреса
            foreach (var ip in addresses)
            {
                if (IPAddress.IsLoopback(ip))
                    result += $"Адрес {ip} - Loopback\n";
                else if (IsLocalIpAddress(ip))
                    result += $"Адрес {ip} - Локальный\n";
                else
                    result += $"Адрес {ip} - Публичный\n";
            }
        }
        catch (Exception ex)
        {
            result += "Ошибка при анализе хоста: " + ex.Message + "\n";
        }
        return result;
    }

    // Ip - локальный?
    private bool IsLocalIpAddress(IPAddress ipAddress)
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList.Any(localIp => localIp.Equals(ipAddress));
    }
    //Сохранение истории проверенных URL
    private ObservableCollection<string> urlHistory = new ObservableCollection<string>();

    private void SaveUrlToHistory(string url)
    {
        if (!urlHistory.Contains(url))
        {
            urlHistory.Add(url);
            lbUrlHistory.ItemsSource = urlHistory;
        }
    }

    private const string HistoryFilePath = "url_history.txt";

}