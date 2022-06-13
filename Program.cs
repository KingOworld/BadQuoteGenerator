using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Program
{
    /*
     * Regex cheatsheet:
     * !NOUN - Noun
     * !PN   - Plural noun
     * !ADJ  - Adjective
     * !VRB  - Verb
     * !VING - Verb ending with ing
     * !VD   - Past-tense verb
     * !NUM  - Random number
     * !NAM  - Random Name
     * !SYM  - Random punctuation symbol (~ . ! ? ...)
     */

    public static List<string> Sentences = new List<string>();
    public static List<string> Nouns = new List<string>();
    public static List<string> PluralNouns = new List<string>();
    public static List<string> Adjectives = new List<string>();
    public static List<string> Verbs = new List<string>();
    public static List<string> VerbsIng = new List<string>();
    public static List<string> VerbsEs = new List<string>();
    public static List<string> VerbsEd = new List<string>();
    public static List<string> Names = new List<string>();
    public static List<string> Symbols = new List<string>() {"~", ".", "!", "?", "..."};

    public static string? Sentence = String.Empty;
    public static int Count = 1;
    public static List<string> QuoteQueue = new List<string>();

    public static void Main(string[] args)
    {
        Init();
        Console.WriteLine("Welcome to Lexi\'s quote generator!\nPress (almost) any key to generate a new quote.\nPress C to change how many quotes appear at once.\nPress S to save a quote to Saved.TXT.\nPress E to exit the program.\nPress escape to clear the console.\nPress H to display this message again.\n");
        while (true)
        {
            for (int i = 0; i < Count; i++)
            {
                var ReplaceNouns = new Regex(Regex.Escape("!NOUN"));
                var ReplacePlurNouns = new Regex(Regex.Escape("!PN"));
                var ReplaceAdjs = new Regex(Regex.Escape("!ADJ"));
                var ReplaceVerbs = new Regex(Regex.Escape("!VRB"));
                var ReplaceVerbsIng = new Regex(Regex.Escape("!VING"));
                var ReplaceVerbsEd = new Regex(Regex.Escape("!VD"));
                var ReplaceNums = new Regex(Regex.Escape("!NUM"));
                var ReplaceNames = new Regex(Regex.Escape("!NAM"));
                var ReplaceSyms = new Regex(Regex.Escape("!SYM"));
                Sentence = Sentences[RandomNumberGenerator.GetInt32(0, Sentences.Count)];
                while (Sentence.Contains("!NOUN"))
                {
                    Sentence = ReplaceNouns.Replace(Sentence, Nouns[RandomNumberGenerator.GetInt32(0, Nouns.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!PN"))
                {
                    Sentence = ReplacePlurNouns.Replace(Sentence, PluralNouns[RandomNumberGenerator.GetInt32(0, PluralNouns.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!ADJ"))
                {
                    Sentence = ReplaceAdjs.Replace(Sentence, Adjectives[RandomNumberGenerator.GetInt32(0, Adjectives.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!VRB"))
                {
                    Sentence = ReplaceVerbs.Replace(Sentence, Verbs[RandomNumberGenerator.GetInt32(0, Verbs.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!VING"))
                {
                    Sentence = ReplaceVerbsIng.Replace(Sentence, VerbsIng[RandomNumberGenerator.GetInt32(0, VerbsIng.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!VD"))
                {
                    Sentence = ReplaceVerbsEd.Replace(Sentence, VerbsEd[RandomNumberGenerator.GetInt32(0, VerbsEd.Count)].ToLower(), 1);
                }
                while (Sentence.Contains("!NUM"))
                {
                    Sentence = ReplaceNums.Replace(Sentence, RandomNumberGenerator.GetInt32(0, 13).ToString(), 1);
                }
                while (Sentence.Contains("!NAM"))
                {
                    Sentence = ReplaceNames.Replace(Sentence, Names[RandomNumberGenerator.GetInt32(0, Names.Count)], 1);
                }
                while (Sentence.Contains("!SYM"))
                {
                    Sentence = ReplaceSyms.Replace(Sentence, Symbols[RandomNumberGenerator.GetInt32(0, Symbols.Count)], 1);
                }
                Console.WriteLine(char.ToUpper(Sentence[0]) + Sentence.Substring(1));
                QuoteQueue.Add(Sentence);
            }
        ReInput:
            ConsoleKeyInfo CKI = Console.ReadKey(true);
            switch (CKI.Key)
            {
                case ConsoleKey.C:
                    Console.WriteLine("\nEnter a number that will determine how many quotes will generate.");
                    try
                    {
                        Count = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                    }
                    catch {
                        Console.WriteLine("\nInvalid number.\n");
                        goto ReInput;
                    }
                    if (Count < 1)
                    {
                        Console.WriteLine("\nNumber cannot be less than 1. Reverted back to 1.\n");
                        Count = 1;
                    }
                    break;
                case ConsoleKey.S:
                    try
                    {
                        if (QuoteQueue.Count == 1)
                        {
                            List<string>? Lines = File.ReadLines(Environment.CurrentDirectory + @"\Assets\Saved.TXT").ToList();
                            Lines.Add($"{QuoteQueue[0]}");
                            File.WriteAllLines(Environment.CurrentDirectory + @"\Assets\Saved.TXT", Lines);
                            Console.WriteLine($"\nSaved \"{QuoteQueue[0]}\" to Saved.TXT!\n");
                        }
                        else
                        {
                            int Choice = MultipleChoice("Save which quote?", QuoteQueue.ToArray());
                            List<string>? Lines = File.ReadLines(Environment.CurrentDirectory + @"\Assets\Saved.TXT").ToList();
                            Lines.Add($"{QuoteQueue[Choice]}");
                            File.WriteAllLines(Environment.CurrentDirectory + @"\Assets\Saved.TXT", Lines);
                            Console.WriteLine($"\nSaved \"{QuoteQueue[Choice]}\" to Saved.TXT!\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nSomething went wrong saving your quote...\n");
                    }
                    goto ReInput;
                case ConsoleKey.E:
                    Console.WriteLine("\nExiting...\n");
                    Environment.Exit(0);
                    break;
                case ConsoleKey.H:
                    Console.WriteLine("\nWelcome to Lexi\'s quote generator!\nPress (almost) any key to generate a new quote.\nPress C to change how many quotes appear at once.\nPress S to save a quote to Saved.TXT.\nPress E to exit the program.\nPress escape to clear the console.\nPress H to display this message again.\n");
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    break;
            }
            QuoteQueue.Clear();
        }
    }

    public static void Init()
    {
        Sentences = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Sentences.TXT").ToList();
        Nouns = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Nouns.TXT").ToList();
        PluralNouns = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\PlurNouns.TXT").ToList();
        Adjectives = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Adjectives.TXT").ToList();
        Verbs = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Verbs.TXT").ToList();
        VerbsIng = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Verbsing.TXT").ToList();
        VerbsEs = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Verbes.TXT").ToList();
        VerbsEd = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Verbeds.TXT").ToList();
        Names = File.ReadAllLines($"{Environment.CurrentDirectory}\\Assets\\Names.TXT").ToList();
    }

    public static int MultipleChoice(string Dialogue, params string[] Choices)
    {
        ConsoleKeyInfo CKI;
        bool Loop = true;
        int Choice = 0;
        while (Loop)
        {
            Console.Clear();
            Console.WriteLine(Dialogue);
            int i = 0;
            foreach (string S in Choices)
            {
                if (i == Choice)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine(S);
                i++;
            }
            while (Console.KeyAvailable) Console.ReadKey(true);
            CKI = Console.ReadKey(true);
            switch (CKI.Key)
            {
                case ConsoleKey.UpArrow: case ConsoleKey.W:
                    if (Choice > 0)
                    {
                        Choice--;
                    }
                    break;
                case ConsoleKey.DownArrow: case ConsoleKey.S:
                    if (Choice < Choices.Length - 1)
                    {
                        Choice++;
                    }
                    break;
                case ConsoleKey.Spacebar: case ConsoleKey.Enter:
                    Loop = false;
                    break;
            }
        }
        return Choice;
    }

}