// See https://aka.ms/new-console-template for more information
using System.Timers;

MyTimer.Start();

public static class MyTimer
{
    private static int Count = 0;
    private static readonly string[] Dungeons = ["GC Duos", "GC Trios", "FC Solos", "FC Trios", "R Solos", "R Duos",];
    public static void Start()
    {
        while (true)
        {
            Console.WriteLine("Enter the current dungeon in rotation:");
            Console.WriteLine("1. GC Duos, \n2. GC Trios,  \n3. FC Solos, \n4. FC Trios, \n5. R Solos, \n6. R Duos,");
            var selection = Console.ReadLine();

            int parsedInt;
            if (int.TryParse(selection, out parsedInt))
            {
                if (parsedInt <= 6 || parsedInt >= 1)
                {
                    Count = parsedInt - 1;
                    break;
                }
                else { Console.WriteLine("Invalid selection"); }
            }
            else { Console.WriteLine("Invalid selection"); }
        }

        var timer = new System.Timers.Timer(60000);
        timer.Elapsed += new ElapsedEventHandler(DetermineCurrentDungeon);
        timer.Enabled = true;

        GC.KeepAlive(timer);

        Console.WriteLine("Press \'q\' to exit");
        while (Console.Read() != 'q') ;
    }

    public static void DetermineCurrentDungeon(object? source, ElapsedEventArgs e)
    {
        Console.Clear();
        Console.WriteLine("Press \'q\' to exit");
        int minutes = e.SignalTime.Minute;
        if (minutes % 5 == 0)
        {
            Count++;
        }

        if (Count > 5) Count = 0;

        var remainingRotasForRS = 4 - Count;
        if (remainingRotasForRS < 0) remainingRotasForRS = 6 + remainingRotasForRS;

        var remainingRotasForICS = 2 - Count;
        if (remainingRotasForICS < 0) remainingRotasForICS = 6 + remainingRotasForICS;

        Console.WriteLine($"Current rotation: {Dungeons[Count]}");
        Console.WriteLine($"Time remaining until Frost Cavers solos: {remainingRotasForRS * 5 - minutes % 5} minutes");
        Console.WriteLine($"Time remaining until Ruins solos: {remainingRotasForICS * 5 - minutes % 5} minutes");
    }
}
