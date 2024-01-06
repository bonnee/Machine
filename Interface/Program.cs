using Simulator;
using System.Diagnostics;

namespace Interface;

internal class Program
{
    private static Machine m;
    private static readonly Stopwatch s = new();

    public static void Main(string[] args)
    {
        var (symbols, delay) = ParseArgs(args);

        Console.WriteLine("Loading...");

        m = new Machine(new List<char>(symbols), File.ReadAllLines(args[0]));
        //m.Cycle += OnCycle;
        m.Finish += OnFinish;

        Run(delay);
    }

    private static (string, int) ParseArgs(string[] args)
    {
        var symbols = "_";
        var delay = 0;

        if (args.Length == 0)
        {
            throw new ArgumentException("No arguments provided.");
        }

        if (!File.Exists(args[0]))
        {
            throw new FileNotFoundException(args[0]);
        }

        switch (args.Length)
        {
            case 2:
                symbols = args[1];
                break;
            case 3:
                delay = Convert.ToInt32(args[2]);
                break;
        }

        return (symbols, delay);
    }

    private static void Run(int delay)
    {
        Console.WriteLine("Computing...");

        s.Start();
        m.Run(State.ZERO, delay);
        s.Stop();

        Console.WriteLine("Done.");
    }

    private static void OnFinish(object sender, MachineEventArgs e)
    {
        Print(e.Tape, e.Cycles, s.Elapsed);
    }

    private static void OnCycle(object sender, MachineEventArgs e)
    {
        Print(e.Tape);
    }

    private static void Print(string tape)
    {
        Console.WriteLine();

        foreach (var c in tape)
            Console.Write(c);
    }

    private static void Print(string tape, int cycles, TimeSpan elapsed)
    {
        Console.WriteLine();

        foreach (var c in tape)
            Console.Write(c);

        Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Cycles: {cycles} | Elapsed: {elapsed.TotalMilliseconds:F2}ms | Op/s: {cycles * 1000 / elapsed.TotalMilliseconds:F2}");
    }
}