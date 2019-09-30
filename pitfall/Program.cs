using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace pitfall
{
    public static class Program
    {
        //initialize variables
        const int W = 60;
        const int H = 30;

        //Print char
        static void print(int x,int y, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor= color;
            Console.Write(symbol);
        }
        static void printWall(int x, int y, string symbol,ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.Write(symbol);
            Console.BackgroundColor = ConsoleColor.White;
        }
        //Print sting
        static void printString(int x,int y,string text,ConsoleColor color)
        {

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        struct Player
        {
            private int _x,_y,_score,_hit;
            private char _symbol;

            public int hit
            {
                get { return _hit; }
                set { _hit = value; }
            }
            public int x
            {
                get { return _x; }
                set
                {
                    if (value <= 1)
                    {
                        _x = 0;
                    }
                    else if (value >= W - 1)
                    {
                        _x = W - 1;
                    }
                    else { _x = value; }
                        
                }
            }
            public int y
            {
                get { return _y; }
                set { _y = value; }
            }
            public int score
            {
                get { return _score; }
                set { _score = value; }
            }
            public char symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }
            public void moveL()
            {
                x -= 1;
            }
            public void moveR()
            {
                x += 1;
            }
            
        }

        struct Wall
        {
            private int _x, _y;
            private char _symbol;
            private string _text;
           
            

            public int x
            {
                get { return _x; }
                set { _x = value; }
            }
            public int y
            {
                get { return _y; }
                set { _y = value; }
            }
            public char symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }
            public string text
            {
                get { return _text; }
                set { _text = value; }
            }
           
        }

        struct item
        {
            private int _x, _y, _result;
            private char _symbol;

            public int x
            {
                get { return _x; }
                set { _x = value; }
            }
            public int y
            {
                get { return _y; }
                set { _y = value; }
            }
            public int result
            {
                get { return _result; }
                set { _result = value; }
            }
            public char symbol
            {
                get { return _symbol; }
                set { _symbol = value; }
            }
        }
        
        static void Main(string[] args)
        {
            //game seting
            Random rand = new Random();

            //set screen
            Console.BufferHeight = Console.WindowHeight = H;
            Console.BufferWidth = Console.WindowWidth = W;

            //player
            Player P1 = new Player();
            P1.x = W / 2;
            P1.y = H / 2;
            P1.symbol = 'Y';
            P1.score = 0;
            P1.hit = 10;
                        

            ////start screen
            screen01 sc = new screen01();
            sc.startScreen();
            Console.ReadKey();


            //Wall
            List<Wall> wallList = new List<Wall>();

            //hit
            List<item> hitList = new List<item>();

            bool gameRunning = true;
            while (gameRunning==true)
            {
                //user input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if(keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        P1.moveL();
                    }
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        P1.moveR();
                    }
                    if(keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        Console.ReadKey();
                    }
                }

                //Wall create

                int spaceWall = rand.Next(10, 25);
                //int pointOld = 25;
                int point = rand.Next(18, 30);


                item newHit = new item();

                //hit
                int randHit = rand.Next(1, 10);
                if (randHit <= 1)
                {
                    newHit.x = rand.Next(point + 1, point + spaceWall - 1);
                    newHit.y = H - 1;
                    newHit.symbol = 'H';
                    newHit.result = 1;
                    hitList.Add(newHit);
                }
                List<item> newHitList = new List<item>();
                for (int i = 0; i < hitList.Count; i++)
                {
                    item oldHit = hitList[i];

                    if (oldHit.y > 1)
                    {
                        item newMoveHit = new item();
                        newMoveHit.x = oldHit.x;
                        newMoveHit.y = oldHit.y -= 1;
                        if ((newMoveHit.x == P1.x) && (newMoveHit.y == P1.y))
                        {
                            P1.hit += newHit.result;
                        }

                        newMoveHit.symbol = oldHit.symbol;
                        //collision detection

                        newHitList.Add(newMoveHit);
                    }
                }
                hitList = newHitList;


                Wall newWallLeft = new Wall();
                Wall newWallRight = new Wall();
                int c = 0;
                //wall left
                if(c==0)
                {
                    newWallLeft.y = H - 1;
                    newWallLeft.x = 0;
                    
                    for (int i = 1; i <= point; i++)
                    {
                        newWallLeft.text += ' ';
                    }
                    wallList.Add(newWallLeft);
                    c = 1;
                }


                //wall right
                if(c==1)
                {
                    newWallRight.y = H - 1;
                    newWallRight.x = point + spaceWall;
                    for (int i = point + spaceWall+1; i <= W - 1; i++)
                    {
                        newWallRight.text += ' ';
                    }
                    wallList.Add(newWallRight);
                    c = 0;
                }

                List<Wall> newWallList = new List<Wall>();
                for (int i = 0; i < wallList.Count; i++)
                {
                    Wall oldWall = wallList[i];
                    if (oldWall.y > 1)
                    {
                        Wall newMoveWall = new Wall();
                        newMoveWall.x = oldWall.x;
                        newMoveWall.y = oldWall.y-=1;
                        newMoveWall.text = oldWall.text;
                        
                        if (newMoveWall.y == P1.y)
                        {
                            for (int n = newMoveWall.x; n <= newMoveWall.x + newMoveWall.text.Length-1; n++)
                            {
                                if (n == P1.x)
                                {
                                    P1.hit--;
                                }
                            }
                            P1.score++;
                        }
                        
                        newWallList.Add(newMoveWall);
                    }

                    
                }
                
                //point = pointOld;
                wallList = newWallList;
                
             


                //update screen
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                
                for (int i = 0; i < wallList.Count; i++)
                {
                    Wall thisWall = wallList[i];
                    if (thisWall.y >= 1)
                    {
                        printWall(thisWall.x, thisWall.y, thisWall.text, ConsoleColor.Black);
                    }                    
                }
                for(int i =0; i < hitList.Count; i++)
                {
                    item thisHit = hitList[i];
                    if(thisHit.y >= 2)
                    {
                        print(thisHit.x,thisHit.y,thisHit.symbol,ConsoleColor.Green);
       
                    }
                }


                
                print(P1.x, P1.y, P1.symbol,ConsoleColor.Black);
                printString(1, 0, "Score: " + P1.score,ConsoleColor.Black);
                printString(W-10, 0, "Hit: " + P1.hit,ConsoleColor.Black);

                if(P1.hit == 0)
                {
                    gameRunning = false;
                }
                Thread.Sleep(100);
            }
            
            while(gameRunning==false)
            {                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                
                string score = "You score is "+P1.score;
                int lenScore = score.Length;
                printString(W / 2 - 4, H / 2 - 2, "Game Over",ConsoleColor.White);
                printString(W / 2 - lenScore/2, H / 2-1, score,ConsoleColor.White);

                string exit = "Press Enter to exit";
                int lenExit = exit.Length;
                printString(W / 2 - lenExit / 2, H / 2 + 2, exit, ConsoleColor.White);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        System.Environment.Exit(1);
                    }
                   
                }
                Thread.Sleep(100);
            }
        }
    }
}
