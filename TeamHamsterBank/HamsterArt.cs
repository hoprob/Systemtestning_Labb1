using System;
using System.Text;
using System.Threading;

namespace TeamHamsterBank
{
    public class HamsterArt
    {
        public static void HeadLine(string str)
        {
            Console.Write(str, Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
        }
        public static void HamsterWelcome()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            PrintLogo();
            HamsterDance(12, 10);
            Console.OutputEncoding = Encoding.Default;
            Console.CursorVisible = true;
        }
        static string Logo()
        {
            return      "▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫" +
                        "▫▫▄▫▫▫▫▫▄▫▫▄■■■■▄▫▫▄▄▫▫▫▫▫▄▄▫▫▄■■■■■▫▫■■■■■■■▫■■■■■■■▫■■■■■■▄▫▫" +
                        "▫▫█▫▫▫▫▫█▫█▫▫▫▫▫▫█▫█▫▀■▫■▀▫█▫▫█▫▫▫▫▫▫▫▫▫▫█▫▫▫▫█▫▫▫▫▫▫▫█▫▫▫▫▫█▫▫" +
                        "▫▫█■■■■■█▫█■■■■■■█▫█▫▫▫▀▫▫▫█▫▫▀■■■■■▄▫▫▫▫█▫▫▫▫█■■■▫▫▫▫█■■■■■▀▫▫" +
                        "▫▫█▫▫▫▫▫█▫█▫▫▫▫▫▫█▫█▫▫▫▫▫▫▫█▫▫▫▫▫▫▫▫█▫▫▫▫█▫▫▫▫█▫▫▫▫▫▫▫█▫▫▫▀▄▫▫▫" +
                        "▫▫▀▫▫▫▫▫▀▫▀▫▫▫▫▫▫▀▫▀▫▫▫▫▫▫▫▀▫▫▀▀▀▀▀▀▀▫▫▫▫▀▫▫▫▫▀▀▀▀▀▀▀▫▀▫▫▫▫▫▀▫▫" +
                        "▫▫▫▫▫▫▫▫■■■▄▫▫▫▫▫▄■■■■▄▫▫▄▄▫▫▫▫▄▫▄▫▫▫▫▫▄▫■■■■■■■▫▄▄▫▫▫▫▄▫▫▫▫▫▫▫" +
                        "▫▫▫▫▫▫▫▫█▫▫▫█▫▫▫█▫▫▫▫▫▫█▫█▫▀■▫▫█▫█▫▫▫■▀▫▫█▫▫▫▫▫▫▫█▫▀■▫▫█▫▫▫▫▫▫▫" +
                        "▫▫▫▫▫▫▫▫█■■■▀▄▫▫█■■■■■■█▫█▫▫▀■▫█▫█■■▀▫▫▫▫█■■■▫▫▫▫█▫▫▀■▫█▫▫▫▫▫▫▫" +
                        "▫▫▫▫▫▫▫▫█▫▫▫▫▫█▫█▫▫▫▫▫▫█▫█▫▫▫▀■█▫█▫▫▫▀■▫▫█▫▫▫▫▫▫▫█▫▫▫▀■█▫▫▫▫▫▫▫" +
                        "▫▫▫▫▫▫▫▫▀▀▀▀▀▀▫▫▀▫▫▫▫▫▫▀▫▀▫▫▫▫▫▀▫▀▫▫▫▫▫▀▫▀▀▀▀▀▀▀▫▀▫▫▫▫▫▀▫▫▫▫▫▫▫" +
                        "▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫";
        }
        static void PrintLogo()
        {
            char[] logoArray = Logo().ToCharArray();
            int count = 0;
            for (int row = 0; row < 12; row++)
            {
                for (int col = 0; col < 63; col++)
                {
                    if (logoArray[count] == '▫')
                        Console.Write(logoArray[count].ToString(), Console.ForegroundColor = ConsoleColor.DarkYellow);
                    else
                        Console.Write(logoArray[count].ToString(), Console.ForegroundColor = ConsoleColor.Yellow);
                    count++;
                }
                Console.Write("\n");
            }
            Console.ResetColor();
        }
        static void PrintHamster(char[] hamster)
        {
            int count = 0;
            for (int row = 0; row < 12; row++)
            {
                for (int col = 0; col < 28; col++)
                {
                    if (hamster[count] == '▫')
                        Console.Write(hamster[count].ToString(), Console.ForegroundColor = ConsoleColor.DarkYellow);
                    else
                        Console.Write(hamster[count].ToString(), Console.ForegroundColor = ConsoleColor.Yellow);
                    count++;
                }
                Console.Write("\n");
            }
            Console.ResetColor();
        }
        static void HamsterDance(int padTop, int rounds)
        {
            string hamster1 = "\t\t     ▫▫▫▫▫▫_▫▫▫▫▫▫▫_▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫( \\_____/ )▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫/  ●   ●  \\▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫(     ᵜ     )▫▫▫▫" +
                              "\t\t     ▫▫▫▫/           \\▫▫▫▫" +
                              "\t\t     ▫▫▫/  \\ \\   / /  \\▫▫▫" +
                              "\t\t     ▫▫|    ᵕᵕ   ᵕᵕ    |▫▫" +
                              "\t\t     ▫▫|               |▫▫" +
                              "\t\t     ▫▫|               |▫▫" +
                              "\t\t     ▫▫\\               /▫▫" +
                              "\t\t     ▫▫▫▫ᵕᵕᵕ¯¯¯¯¯¯¯ᵕᵕᵕ▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫";
            string hamster2 = "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫_▫▫▫▫▫▫▫_▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫( \\_____/ )▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫/  ●   ●  \\▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫(     ᵜ     )▫▫▫▫" +
                              "\t\t     ▫▫▫▫/           \\▫▫▫▫" +
                              "\t\t     ▫▫▫/  \\ \\   / /  \\▫▫▫" +
                              "\t\t     ▫▫|    ᵕᵕ   ᵕᵕ    |▫▫" +
                              "\t\t     ▫▫|               |▫▫" +
                              "\t\t     ▫▫\\               /▫▫" +
                              "\t\t     ▫▫▫▫ᵕᵕᵕ¯¯¯¯¯¯¯ᵕᵕᵕ▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫";

            string hamster3 = "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫_▫▫▫▫▫▫▫_▫▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫( \\_____/ )▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫/  ●   ●  \\▫▫▫▫▫" +
                              "\t\t     ▫▫▫▫(     ᵜ     )▫▫▫▫" +
                              "\t\t     ▫▫▫▫/           \\▫▫▫▫" +
                              "\t\t     ▫▫▫/  \\ \\   / /  \\▫▫▫" +
                              "\t\t     ▫▫|    ᵕᵕ   ᵕᵕ    |▫▫" +
                              "\t\t     ▫▫\\               /▫▫" +
                              "\t\t     ▫▫▫▫ᵕᵕᵕ¯¯¯¯¯¯¯ᵕᵕᵕ▫▫▫▫" +
                              "\t\t     ▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫▫";
            char[] h1 = hamster1.ToCharArray();
            char[] h2 = hamster2.ToCharArray();
            char[] h3 = hamster3.ToCharArray();
            for (int i = 0; i < rounds; i++)
            {
                Console.CursorTop = padTop;
                Console.CursorLeft = 0;
                PrintHamster(h1);
                Thread.Sleep(100);
                Console.CursorTop = padTop;
                Console.CursorLeft = 0;
                PrintHamster(h2);
                Thread.Sleep(100);
                Console.CursorTop = padTop;
                Console.CursorLeft = 0;
                PrintHamster(h3);
                Thread.Sleep(100);
                Console.CursorTop = padTop;
                Console.CursorLeft = 0;
                PrintHamster(h2);
                Thread.Sleep(100);
            }
        }
    }
}
