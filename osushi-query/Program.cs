using System.Text;
using HVR.Osushi;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Starting...");

        var avtr = File.ReadAllText("response-avtr.json", Encoding.UTF8).Trim();
        var root = File.ReadAllText("response.json", Encoding.UTF8).Trim().Replace("$$AVTR$$", avtr);
        var query = new OsushiQuery(root, avtr);
        query.Start();

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
        
        query.Stop();
    }
}