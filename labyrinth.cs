using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace labyrinth
{
    class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);
            //prevents window size change
            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            //sets specific window size
            Console.SetWindowSize(80, 40);
            Console.SetBufferSize(80, 40);

            //reads player save file
            Player player = readSaveFile();
            Console.Clear();
            fillEmptyLines(15);
            fillEmptyChars(10);
            Console.WriteLine("Welcome back, {0}", player.Name);
            Thread.Sleep(2000);
            //loads menu
            mainMenu(player);
        }

        static void mainMenu(Player player)
        {
            int menuItem = 0;
            bool loopMenu = true;
            ConsoleKeyInfo cki;
            while (loopMenu)
            {
                Console.Clear();
                Console.WriteLine("[arrows] - navigation, [Enter] - select");
                fillEmptyLines(14);
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Select Level    ");
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  How To Play     ");
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 2)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Reset Score     ");
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 3)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Exit            ");
                Console.ResetColor();
                fillEmptyLines(10);
                fillEmptyChars(10);
                Console.WriteLine("{0} score: {1}", player.Name, player.Score);
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (menuItem == 0) { menuItem = 3; } else { menuItem--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (menuItem == 3) { menuItem = 0; } else { menuItem++; }
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Enter:
                        switch (menuItem)
                        {
                            case 1://how to play button
                                howToPlayMenu();
                                break;
                            case 2://reset score button
                                resetMenu(player);
                                break;
                            case 3://exit button
                                loopMenu = false;
                                break;
                            default://select level button
                                levelMenu(player);
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void levelMenu(Player player)
        {
            int menuItem = 0;
            bool loopMenu = true;
            ConsoleKeyInfo cki;
            while (loopMenu)
            {
                Console.Clear();
                Console.WriteLine("[Escape] - back, [arrows] - navigation, [Enter] - select");
                fillEmptyLines(4);
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("          Select Level                                                          ");
                Console.ResetColor();
                Console.WriteLine();
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Level 01       ");
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Level 02       ");
                Console.ResetColor(); Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 2)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Level 03       ");
                Console.ResetColor(); Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 3)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Level 04       ");
                Console.ResetColor(); Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 4)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("  Level 05       ");
                Console.ResetColor();
                fillEmptyLines(16);
                fillEmptyChars(10);
                Console.WriteLine("{0} score: {1}", player.Name, player.Score);
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (menuItem == 0) { menuItem = 4; } else { menuItem--; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (menuItem == 4) { menuItem = 0; } else { menuItem++; }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Escape:
                        loopMenu = false;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Enter:
                        switch(menuItem)
                        {
                            case 0:
                                playLevel(player, "01");
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void playLevel(Player player, string level)
        {
            ConsoleKeyInfo cki;
            try
            {
                bool loopLevel = true;
                int exitX = 0;
                int exitY = 0;
                int playerX = 0;
                int playerY = 0;
                int[,] map = new int[80,40];
                using(StreamReader sr = new StreamReader("levels\\" + level + ".lvl"))
                {
                    exitX = Int32.Parse(sr.ReadLine());
                    exitY = Int32.Parse(sr.ReadLine());
                    playerX = Int32.Parse(sr.ReadLine());
                    playerY = Int32.Parse(sr.ReadLine());
                    string line;
                    for(int i=0; i < 40; i++)
                    {
                        line = sr.ReadLine();
                        for(int j=0; j < 80; j++)
                        {
                            map[j, i] = Int32.Parse(line[j].ToString());
                        }
                    }
                }
                while(loopLevel)
                {
                    Console.Clear();
                    for (int i = 0; i < 39; i++)
                    {
                        for (int j = 0; j < 79; j++)
                        {
                            if (i == playerY && j == playerX)
                                Console.Write("O");
                            else
                            {
                                if (i == exitY && j == exitY)
                                    Console.Write("X");
                                else
                                {
                                    if (map[j, i] == 0)
                                        Console.Write(" ");
                                    else
                                        Console.Write(map[j, i]);
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    cki = Console.ReadKey();
                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (playerY != 0 && map[playerX, playerY-1] == 0)
                                playerY--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (playerY != 38 && map[playerX, playerY+1] == 0)
                                playerY++;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (playerX != 0 && map[playerX-1, playerY] == 0)
                                playerX--;
                            break;
                        case ConsoleKey.RightArrow:
                            if (playerX != 78 && map[playerX + 1, playerY] == 0)
                                playerX++;
                            break;
                        case ConsoleKey.Escape:
                            loopLevel = false;
                            break;
                        
                        case ConsoleKey.Enter:
                            break;
                        default:
                            break;
                    }
                    if(playerX==exitX && playerY==exitY)
                    {
                        player.AddScore(100);
                        save(player);
                        loopLevel = false;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox((IntPtr)0, e.ToString(), "My Message Box", 0);
            }
        }

        static void howToPlayMenu()
        {
            bool loopMenu = true;
            ConsoleKeyInfo cki;
            while (loopMenu)
            {
                Console.Clear();
                Console.WriteLine("[Escape or LeftArrow] - back");
                fillEmptyLines(4);
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("          How To Play                                                           ");
                Console.ResetColor();
                Console.WriteLine();
                fillEmptyChars(10);
                Console.WriteLine("Legend:");
                fillEmptyChars(10);
                Console.WriteLine("O - your character");
                fillEmptyChars(10);
                Console.WriteLine("X - labyrinth exit");
                fillEmptyChars(10);
                Console.WriteLine("1 - labyrinth wall");
                Console.WriteLine();
                fillEmptyChars(10);
                Console.WriteLine("Navigate through the labyrinth using [arrows] to find the exit");
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        loopMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }

        static void resetMenu(Player player)
        {
            int menuItem = 0;
            bool loopMenu = true;
            ConsoleKeyInfo cki;
            while (loopMenu)
            {
                Console.Clear();
                Console.WriteLine("[Escape] - back, [arrows] - navigation, [Enter] - select");
                fillEmptyLines(4);
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("          Reset Score                                                           ");
                Console.ResetColor();
                Console.WriteLine();
                fillEmptyChars(10);
                Console.WriteLine("Are you sure you want to reset your score?");
                Console.WriteLine();
                Console.ResetColor();
                fillEmptyChars(8);
                if (menuItem == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("  Yes             ");
                Console.ResetColor();
                fillEmptyChars(18);
                if (menuItem == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("  No              ");
                Console.ResetColor();
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.LeftArrow:
                        if (menuItem == 0) { menuItem = 1; } else { menuItem = 0; }
                        break;
                    case ConsoleKey.Escape:
                        loopMenu = false;
                        break;
                    case ConsoleKey.Enter:
                        if (menuItem == 0)
                        {
                            player.ResetScore();
                        }
                        loopMenu = false;
                        break;
                    default:
                        break;
                }
                save(player);
            }
        }

        static void save(Player player)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("player.save", false))
                {
                    sw.WriteLine(player.Name);
                    sw.WriteLine(player.Score);
                }
            }
            catch (Exception e)
            {

            }
        }

        static Player readSaveFile()
        {
            Player player = new Player();
            try
            {
                if (File.Exists("player.save"))
                {
                    //read save file
                    using (StreamReader sr = new StreamReader("player.save"))
                    {
                        player.Name = sr.ReadLine();
                        player.AddScore(Int32.Parse(sr.ReadLine()));
                    }
                } else
                {
                    //create save file
                    fillEmptyLines(15);
                    fillEmptyChars(10);
                    Console.WriteLine("Savefile not found");
                    fillEmptyChars(10);
                    Console.WriteLine("Please enter player name below to create new savefile");
                    fillEmptyChars(10);
                    player = new Player(Console.ReadLine(), 0);
                    Console.Clear();
                    using (StreamWriter sw = new StreamWriter("player.save",false))
                    {
                        sw.WriteLine(player.Name);
                        sw.WriteLine(player.Score);
                    }
                    fillEmptyLines(15);
                    fillEmptyChars(10);
                    Console.WriteLine("Savefile successfully created");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {

            }
            return player;
        }

        static void fillEmptyChars(int num)
        {
            for(int i = 0; i < num; i++)
            {
                Console.Write(" ");
            }
        }

        static void fillEmptyLines(int num)
        {
            for(int i = 0; i < num; i++)
            {
                Console.WriteLine();
            }
        }
    }

    class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int score;
        public int Score
        {
            get { return score; }
        }
        public Player()
        {
            this.name = "player";
            this.score = 0;
        }
        public Player(string name,int score)
        {
            this.name = name;
            this.score = score;
        }
        public void AddScore(int num)
        {
            this.score += num;
        }
        public void ResetScore()
        {
            this.score = 0;
        }
    }
}
