using FormulaAPI.Entities;

namespace PV178_HW02.ui;

public class UI
{
    public static int ReadInt(String napis_pre_uzivatela, int diapason) 
    {
        int n = 0;
        String s;

        try {
            Console.Write(napis_pre_uzivatela);
            s = Console.ReadLine();
            n = Int32.Parse(s);
        } catch (Exception e) {
            Console.WriteLine("nepodarilo se");
            n = ReadInt(napis_pre_uzivatela, diapason);
        }

        if (n < 1 || n > diapason + 1)
        {
            Console.WriteLine("nepodarilo se");
            n = ReadInt(napis_pre_uzivatela, diapason);
        }
        return n;
    }

    public static void PrintStart()
    {
        Console.WriteLine("Welcome to program" +
                          "\nWhat would you like to choose?" +
                          "\n1.Check technical issue for specific driver" +
                          "\n2.Check technical issue for every driver specif nationality");
        
    }
    
}