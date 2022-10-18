using System;

namespace NumberMethodLab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string funcStr = "x - sqrt(ln(x+2)) = 0";
            const string funcFirstDerStr = "1 - 1 / ((2x + 4) * sqrt(ln(x+2)))";
            const string funcSecondDerStr = "(2 * ln(x + 2) + 1) / ((4x^2 + 16x + 16) * ln^1.5(x + 2))";
            const string canonicalView = "x = sqtr(ln(x+2))";
            const string canonicalViewDer = "1 + x - sqrt(ln(x+2)) / K ^ 2";
            double limitA = 0.8f;
            double limitB = 1.7f;
            string immobPoint = "";
            double resultFuncA = 0;
            double resultFuncB = 0;
            double resultFuncFirstDerA = 0;
            double resultFuncFirstDerB = 0;
            double resultFuncSecondDerA = 0;
            double resultFuncSecondDerB = 0;
            double E = 0.00001;
            double K = 0;
            double resultFiA = 0;
            double resultFiB = 0;
            double resultFiFirstDerA = 0;
            double resultFiFirstDerB = 0;
            double root = 0;
            double stop = 0;
            bool convergence = true;

            showFuncStr(funcStr, funcFirstDerStr, funcSecondDerStr);

            resultFuncA = func(limitA);
            resultFuncB = func(limitB);
            resultFuncFirstDerA = funcDerFir(limitA);
            resultFuncFirstDerB = funcDerFir(limitB);
            resultFuncSecondDerA = funcDerSec(limitA);
            resultFuncSecondDerB = funcDerSec(limitB);

            showFuncResult(resultFuncA, resultFuncB, resultFuncFirstDerA, resultFuncFirstDerB, resultFuncSecondDerA, resultFuncSecondDerB);

            switch (switchKey())
            {
                case "1":
                    {
                        if (resultFuncA > 0 && resultFuncSecondDerA > 0)
                        {
                            immobPoint = "A";
                            Console.WriteLine("Неподвижная точка = " + immobPoint);

                        }
                        else if (resultFuncB > 0 && resultFuncSecondDerB > 0)
                        {
                            immobPoint = "B";
                            Console.WriteLine("Неподвижная точка = " + immobPoint);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка");
                        }

                        Console.WriteLine();

                        Console.WriteLine("n\ta\tb\th");

                        root = combination(limitA, limitB, immobPoint, E);

                        break;
                    }
                case "2":
                    {
                        showFiStr(canonicalView, canonicalViewDer);
                        K = calcK(calcN(resultFuncFirstDerA, resultFuncFirstDerB), resultFuncFirstDerA);
                        resultFiA = Fi(limitA, K);
                        resultFiB = Fi(limitB, K);
                        resultFiFirstDerA = FiDerFir(limitA, K);
                        resultFiFirstDerB = FiDerFir(limitB, K);

                        showFiResult(resultFiA, resultFiB, resultFiFirstDerA, resultFiFirstDerB);

                        convergence = calcConvergence(resultFiFirstDerA, resultFiFirstDerB);

                        if (convergence == true)
                        {
                            stop = calcStop(resultFiFirstDerA, resultFiFirstDerB, E);
                        }
                        else
                        {
                            stop = E;
                        }

                        Console.WriteLine("Значение критерия останова = " + stop);

                        Console.WriteLine();

                        Console.WriteLine("n\ta\tb\th");

                        root = iteracion(limitA, stop, K);
                        break;
                    }

            }

            Console.WriteLine("\nКорень = " + Math.Round(root, 5));

            Console.ReadLine();

        }

        private static void showFiStr(string canonicalView, string canonicalViewDer)
        {
            Console.WriteLine("Фи: " + canonicalView);
            Console.WriteLine("Производная Фи: " + canonicalViewDer);
        }

        private static string switchKey()
        {
            return Console.ReadLine();
        }

        private static void showFuncStr(string funcStr, string funcDerFirStr, string funcDerSecStr)
        {
            Console.WriteLine("Функция: " + funcStr);
            Console.WriteLine("Первая производная: " + funcDerFirStr);
            Console.WriteLine("Вторая производная: " + funcDerSecStr);
        }

        private static double func(double x)
        {
            double result = 0;
            result = x - Math.Sqrt(Math.Log(x + 2, Math.E));
            return result;
        }

        private static double funcDerFir(double x)
        {
            double result = 0;
            result = 1 - 1 / ((2 * x + 4) * Math.Sqrt(Math.Log(x + 2, Math.E)));
            return result;
        }

        private static double funcDerSec(double x)
        {
            double result = 0;
            result = 1 / (2 * Math.Log(x + 2, Math.E) + 1) / ((Math.Pow(4 * x, 2) + 16 * x + 16) * Math.Pow(Math.Log((x + 2), Math.E), 1.5));
            return result;
        }

        private static void showFiResult(double resultFiA, double resultFiB, double resultFiDerFirA, double resultFiDerFirB)
        {
            Console.WriteLine("Результат Фи, при Х = А: " + Math.Round(resultFiA, 5));
            Console.WriteLine("Результат Фи, при Х = Б: " + Math.Round(resultFiB, 5));
            Console.WriteLine("Результат первой производной Фи, при Х = А: " + Math.Round(resultFiDerFirA, 5));
            Console.WriteLine("Результат первой производной Фи, при Х = Б: " + Math.Round(resultFiDerFirB, 5));
        }

        private static void showFuncResult(double resultFuncA, double resultFuncB, double resultFuncDerFirA, double resultFuncDerFirB, double resultFuncDerSecA, double resultFuncDerSecB)
        {
            Console.WriteLine("Результат функции, при Х = А: " + Math.Round(resultFuncA, 5));
            Console.WriteLine("Результат функции, при Х = Б: " + Math.Round(resultFuncB, 5));
            Console.WriteLine("Результат первой производной, при Х = А: " + Math.Round(resultFuncDerFirA, 5));
            Console.WriteLine("Результат первой производной, при Х = Б: " + Math.Round(resultFuncDerFirB, 5));
            Console.WriteLine("Результат второй производной, при Х = А: " + Math.Round(resultFuncDerSecA, 5));
            Console.WriteLine("Результат второй производной, при Х = Б: " + Math.Round(resultFuncDerSecB, 5));
        }

        private static double chordImmobB(double limitA, double limitB)
        {
            double xn = limitA;
            double Fxn = 0;
            double b_xn = 0;
            double Fb_Fxn = 0;
            double FxnB_xn = 0;
            double h = 0;
            double result = 0;

            Fxn = func(xn);

            b_xn = limitB - xn;

            Fb_Fxn = func(limitB) - func(xn);

            FxnB_xn = Fxn * (limitB - xn);

            h = FxnB_xn / Fb_Fxn;

            result = xn - h;

            return result;
        }

        private static double chordImmobA(double limitA, double limitB)
        {
            double xn = limitB;
            double Fxn = 0;
            double Fxn_Fa = 0;
            double FxnXn_a = 0;
            double h = 0;
            double result = limitB;

            Fxn = func(xn);

            Fxn_Fa = Fxn - func(limitA);

            FxnXn_a = Fxn * (xn - limitA);

            h = FxnXn_a / Fxn_Fa;

            result = xn - h;

            xn = result;

            return result;
        }

        private static double newton(double x)
        {
            return x - func(x) / funcDerFir(x);
        }

        private static double combination(double limitA, double limitB, string immobility, double E)
        {
            int inc = 0;
            double limitAHelp = 0;
            double limitBHelp = 0;

            Console.WriteLine(inc + "\t" + Math.Round(limitA, 5) + "\t" + Math.Round(limitB, 5) + "\t" + Math.Round((limitB - limitA), 5));

            if (immobility == "A")
            {
                while ((limitB - limitA) > E)
                {
                    inc++;

                    limitAHelp = newton(limitA);
                    limitBHelp = chordImmobB(limitA, limitB);
                    limitA = limitAHelp;
                    limitB = limitBHelp;

                    Console.WriteLine(inc + "\t" + Math.Round(limitA, 5) + "\t" + Math.Round(limitB, 5) + "\t" + Math.Round((limitB - limitA)), 5);
                }
            }

            else
            {
                while ((limitB - limitA) > E)
                {
                    inc++;

                    limitBHelp = newton(limitB);
                    limitAHelp = chordImmobA(limitA, limitB);
                    limitA = limitAHelp;
                    limitB = limitBHelp;

                    Console.WriteLine(inc + "\t" + Math.Round(limitA, 5) + "\t" + Math.Round(limitB, 5) + "\t" + Math.Round((limitB - limitA)), 5);
                }
            }

            return (limitA + limitB) / 2;
        }

        private static double calcN(double resultFuncDerFirA, double resultFuncDerFirB)
        {
            double result = Math.Max(Math.Abs(resultFuncDerFirA), Math.Abs(resultFuncDerFirB));
            return result;
        }

        private static double calcK(double N, double resultFuncDerFirA)
        {
            double K = N / 2 + 0.5;

            if (resultFuncDerFirA > 0 && K > 0 || resultFuncDerFirA < 0 && K < 0)
            {
                return K;
            }
            else
            {
                return -K;
            }
        }

        private static double Fi(double x, double K)
        {
            return x - func(x) / K;
        }

        private static double FiDerFir(double x, double K)
        {
            return 1 + func(x) / Math.Pow(K, 2);
        }

        private static bool calcConvergence(double resultFiDerFirA, double resultFiDerFirB)
        {
            if (resultFiDerFirA < 0 && resultFiDerFirB < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static double calcStop(double resultFiDerFirA, double resultFiDerFirB, double E)
        {
            return (1 - Math.Max(Math.Abs(resultFiDerFirA), Math.Abs(resultFiDerFirB))) * E;
        }

        private static double iteracion(double x, double stop, double K)
        {
            double help = 0;
            int inc = 0;
            while ((x - help) > Math.Abs(stop))
            {
                help = x;
                x = Fi(x, K);
                Console.WriteLine(inc + "\t" + Math.Round(help, 5) + "\t" + Math.Round(x, 5) + "\t" + Math.Round((x - help), 5));
                inc++;
            }

            return x;
        }
    }
}
