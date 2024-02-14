// See https://aka.ms/new-console-template for more information
using System.Timers;

MyTimer.Start();

public static class MyTimer
{
    //the dungeons, in the order they occur starting from 0 minutes in the hour
    private static readonly string[] Dungeons = ["GC Duos", "GC Trios", "FC Solos", "FC Trios", "R Solos", "R Duos",];
    
    public static void Start()
    {
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += new ElapsedEventHandler(DetermineCurrentDungeon);
        timer.Enabled = true;

        GC.KeepAlive(timer);

        Console.WriteLine("Press \'q\' to exit");
        while (Console.Read() != 'q') ;
    }

    public static void DetermineCurrentDungeon(object? source, ElapsedEventArgs e)
    {
        //set timer to one minute after intial
        var timer = (System.Timers.Timer)source!;
        timer.Interval = 60000;

        Console.Clear();
        Console.WriteLine("Press \'q\' to exit");

        //since the entire cycle repeats on a 30 minute interval we just keep the minutes to 0-30
        //this lets us just divide the minutes by 5 to get the dungeon index
        int minutes = e.SignalTime.Minute > 30 ? e.SignalTime.Minute - 30 : e.SignalTime.Minute;
        int currentDungeonIndex = (int)Math.Floor(minutes  / 5m);

        var remainingRotasForRS = 4 - currentDungeonIndex;
        if (remainingRotasForRS < 0) remainingRotasForRS = 6 + remainingRotasForRS;

        var remainingRotasForICS = 2 - currentDungeonIndex;
        if (remainingRotasForICS < 0) remainingRotasForICS = 6 + remainingRotasForICS;

        Console.WriteLine($"Current rotation: {Dungeons[currentDungeonIndex]}");
        int remainingFCSmins = remainingRotasForICS * 5 - minutes % 5;
        int remainingRSmins = remainingRotasForRS * 5 - minutes % 5;
        Console.WriteLine($"Time remaining until Frost Caverns solos: {(remainingFCSmins <= 0 ? "CURRENT DUNGEON" : $"{remainingFCSmins} minutes")}");
        Console.WriteLine($"Time remaining until Ruins solos: {(remainingRSmins <= 0 ? "CURRENT DUNGEON" : $"{remainingRSmins} minutes")}");
    }
}
