using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    public static List<string> Nouns = new List<string>();
    public static List<string> PlurNouns = new List<string>();
    public static List<string> Verbs = new List<string>();
    public static List<string> Adjectives = new List<string>();
    public static List<string> Verbsing = new List<string>();
    public static List<string> Sentences = new List<string>();
    public static string? Sentence;
    public static string StartupDirectory = Environment.CurrentDirectory;
    public static void FillList(List<string> list, string dir)
    {
        try
        {
            List<string> GetLines = File.ReadAllLines(StartupDirectory + "/Assets/" + dir).ToList();
            foreach (string Line in GetLines)
            {
                list.Add(Line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void Main (string[] args)
    {
        var ReplaceNouns = new Regex(Regex.Escape("$N"));
        var ReplaceVerbs = new Regex(Regex.Escape("$V"));
        var ReplaceVerbsing = new Regex(Regex.Escape("$IV"));
        var ReplaceAdjs = new Regex(Regex.Escape("$A"));
        var ReplacePNouns = new Regex(Regex.Escape("$P-N"));
        FillList(Nouns, "Nouns.txt");
        FillList(PlurNouns, "PlurNouns.txt");
        FillList(Verbs, "Verbs.txt");
        FillList(Verbsing, "Verbsing.txt");
        FillList(Adjectives, "Adjectives.txt");
        FillList(Sentences, "Sentences.txt");
        Console.WriteLine("Welcome to Lexi's crappy quote generator!\nPress enter to generate a new quote.\nEnter c to clear the console.\nEnter e to exit.\n");
        Loop:
        do
        {
            Sentence = Sentences[RandomNumberGenerator.GetInt32(0, Sentences.Count)];
            while (Sentence.Contains("$N"))
            {
                Sentence = ReplaceNouns.Replace(Sentence, Nouns[RandomNumberGenerator.GetInt32(0, Nouns.Count)], 1);
            }
            while (Sentence.Contains("$V"))
            {
                Sentence = ReplaceVerbs.Replace(Sentence, Verbs[RandomNumberGenerator.GetInt32(0, Verbs.Count)], 1);
            }
            while (Sentence.Contains("$P-N"))
            {
                Sentence = ReplacePNouns.Replace(Sentence, PlurNouns[RandomNumberGenerator.GetInt32(0, PlurNouns.Count)], 1);
            }
            while (Sentence.Contains("$IV"))
            {
                Sentence = ReplaceVerbsing.Replace(Sentence, Verbsing[RandomNumberGenerator.GetInt32(0, Verbsing.Count)], 1);
            }
            while (Sentence.Contains("$A"))
            {
                Sentence = ReplaceAdjs.Replace(Sentence, Adjectives[RandomNumberGenerator.GetInt32(0, Adjectives.Count)], 1);
            }
            Sentence = Sentence.ToLower();
            Console.WriteLine(char.ToUpper(Sentence[0]) + Sentence.Substring(1));
            Sentence = Console.ReadLine().ToLower();
        }
        while (Sentence == "");
        if (Sentence == "e")
        {
            Console.WriteLine("Exiting...");
            System.Environment.Exit(0);
        }
        else if (Sentence == "c")
        {
            Console.Clear();
            goto Loop;
        }
        else
        {
            goto Loop;
        }
    }
}