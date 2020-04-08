//В свободное время одноклассники Вася и Петя любят играть в различные логические игры: морской бой, крестики-нолики, шахматы, шашки и многое другое. Ребята уже испробовали и поиграли
//во всевозможные классические игры подобного рода, включая компьютерные. Однажды им захотелось сыграть во что-нибудь новое, но ничего подходящего найти не удалось.
//Тогда Петя придумал следующую игру «Угадайка»: Играют двое участников. Первый загадывает любое трехзначное число, такое что первая и последняя цифры отличаются
//друг от друга более чем на единицу. Далее загадавший число игрок переворачивает загаданное число, меняя первую и последнюю цифры местами, таким образом получая еще одно число.
//Затем из максимального из полученных двух чисел вычитается минимальное. Задача второго игрока – угадать по первой цифре полученного в результате вычитания числа само это число.
//Например, если Вася загадал число 487,
//то перестановкой первой и последней цифры он получит число 784. После чего ему придется вычесть из 784 число 487, в результате чего получится число 297, которое и должен отгадать Петя
//по указанной первой цифре «2», взятой из этого числа. Петя успевает лучше Васи по математике, поэтому практически всегда выигрывает в играх такого типа. Но в данном случае Петя схитрил и
//специально придумал такую игру, в которой он не проиграет Васе в любом случае. Дело в том, что придуманная Петей игра имеет выигрышную стратегию, которая заключается в следующем: искомое
//число всегда является трехзначным и вторая его цифра всегда равна девяти, а для получения значения последней достаточно отнять от девяти первую, т.е. в рассмотренном выше случае последняя
//цифра равна 9-2=7. Помогите Пете еще упростить процесс отгадывания числа по заданной его первой цифре, написав соответствующую программу.

using System;
using static System.Console;


namespace GameTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstDigit = 0, convertedDigit = 0, trueDigit = 0, finalAnswer = 0;
            GameLogic gl = new GameLogic();

            firstDigit = gl.PrintStartMenu();
            convertedDigit = gl.ChangeDigits(firstDigit);
            trueDigit = gl.TrueDigit(firstDigit, convertedDigit);
            finalAnswer = gl.Game(trueDigit);

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"\nPlayer 1 number is: {trueDigit}, \nyour or computer answer is: {finalAnswer}\nCongratulation! Player 2 win!");
            ResetColor();
        }
    }

    public class GameLogic
    {
        public int PrintStartMenu()
        {
            while (true)
            {
                Write("Player 1, please, write 3-digit number (first digit must be larger or smaller of the last digit): ");
                int digit = Convert.ToInt32(ReadLine());

                if ((digit > 99) && (digit < 999))
                {
                    if (digit.ToString()[0] == digit.ToString()[2])
                    {
                        ForegroundColor = ConsoleColor.DarkRed;
                        WriteLine("First digit must be larger or smaller of the last digit. Please, try again!\n");
                        ResetColor();
                    }
                    else
                    {
                        Clear();
                        return digit;
                    }
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"numbers must be in the range: 100 - 998. Please, try again!\n");
                    ResetColor();
                }
            }
        }

        public int ChangeDigits(int _playerOneNumber)
        {
            char[] tempNum = _playerOneNumber.ToString().ToCharArray();
            Array.Reverse(tempNum);
            string answer = new string(tempNum);
                
            return Convert.ToInt32(answer);
        }

        public int TrueDigit(int _firstDigit, int _convertedDigit)
        {
            if (_firstDigit > _convertedDigit)
                return _firstDigit - _convertedDigit;
            else
                return _convertedDigit - _firstDigit;
        }

        public int Game(int _trueDigit)
        {
            while (true)
            {
                int userAnswer = 0;

                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine($"Guess the number by the first digit! First digit is: {_trueDigit.ToString()[0]}");
                Write($"Guess yourself? (Yes = 'Y', No = 'N') if answer 'NO', the computer will guess itself! :");
                ResetColor();

                string tempAnswer = ReadKey().KeyChar.ToString().ToLower();
                char lowerAnswer = Convert.ToChar(tempAnswer);

                switch (lowerAnswer)
                {
                    case 'y':
                    {
                        Write($"\n\nInsert full number (first digit is {_trueDigit.ToString()[0]}): ");
                        userAnswer = Convert.ToInt32(ReadLine());

                        if (userAnswer == _trueDigit)
                        {
                            return userAnswer;
                        }
                        else
                        {
                            ForegroundColor = ConsoleColor.DarkRed;
                            WriteLine($"\nWrong! Try again.\n");
                            ResetColor();
                        }

                        break;
                    }
                    case 'n':
                    {
                        ForegroundColor = ConsoleColor.DarkGreen;
                        WriteLine($"\n\n1. First digit is {_trueDigit.ToString()[0]}\n" +
                                  $"2. Second digit always matters 9\n" +
                                  $"3. Third number formula is {_trueDigit.ToString()[1]} - {_trueDigit.ToString()[0]} = {_trueDigit.ToString()[2]}\n");
                        ResetColor();

                        int computerAnswerThirdNum = Convert.ToInt32(_trueDigit.ToString()[1]) -
                                                     Convert.ToInt32(_trueDigit.ToString()[0]);
                        int compAnswer = Convert.ToInt32($"{_trueDigit.ToString()[0]}9{computerAnswerThirdNum}");

                        return compAnswer;
                    }
                    default:
                    {
                        ForegroundColor = ConsoleColor.DarkRed;
                        WriteLine($"\nError! The answer can only be 'Y' or 'N'! Try again!\n"); 
                        ResetColor();
                        break;
                    }
                }
            }
        }
    }
}
