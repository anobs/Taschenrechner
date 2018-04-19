using System;
using System.Collections.Generic;
using System.Linq;

namespace Taschenrechner
{
    class Program
    {
        /// <summary>
        /// Deklarationsblock
        /// </summary>
        private double _operand = 0;
        private const string _menu = "\n\rMenu:\n\r"
                            + "\n\r\t[Enter]\t\tRechnen"
                            + "\n\r\t[h]\t\tDiese Hilfe"
                            + "\n\r\t[ESC]\t\tBeenden"
                            + "\n\r\n\rTaste drücken . . .";
        private string[] Operations = { "+","-","*","/","%","²","³","|","^" };

        /* **************************************
         * application layer (application) ******
         * ************************************ */
        /// <summary>
        /// Programmeinstiegspunkt
        /// </summary>
        /// <param name="args">String array der Komandozeilenparameter</param>
        static void Main(string[] args)
        {
            Program p = new Program();
            p.run();
        }
        /// <summary>
        /// Öffentliche Methode, um das Programm über eine Instanz zu starten,
        /// erwartet gültige Taste für Menüauswahl.
        /// </summary>
        public void run()
        {
            Console.WriteLine(_menu);
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                runMenu(key.Key);
                Console.WriteLine(_menu);
                key = Console.ReadKey();
            }
        }
        /// <summary>
        /// Menüstart über Tastendruck
        /// </summary>
        /// <param name="key">Taste die gedrückt wurde</param>
        void runMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine("\n\rErgebnis:\n\r\n\r\t{0}", compute());
                    break;
                case ConsoleKey.H:
                    return;
                default:
                    return;
            }
        }
        
        /* **************************************
         * business layer (controller) **********
         * ************************************ */
        /// <summary>
        /// Der eigentliche Rechner, lädt Operand und Operation sowie wenn notwendig den Operator und gibt das berechnete Ergebnis zurück.
        /// </summary>
        /// <returns>Double, das berechnetes Ergebnis</returns>
        double compute()
        {
            _operand = loadOperand();
            switch (loadOperation())
            {
                case "-":
                    return (_operand -= loadOperator());
                case "*":
                    return (_operand *= loadOperator());
                case "/":
                    return (_operand /= loadOperator());
                case "%":
                    return (_operand %= loadOperator());
                case "^":
                    return (_operand = Math.Pow(_operand, loadOperator()));
                case "²":
                    return (_operand *= _operand);
                case "³":
                    return (_operand = _operand * _operand * _operand);
                case "|":
                    return (_operand = Math.Sqrt(_operand));
                default:
                    return (_operand += loadOperator());
            }
        }
        /// <summary>
        /// Lädt den Operand von stdin.
        /// </summary>
        /// <returns>String, den Operand oder Ausgangswert</returns>
        double loadOperand()
        {
            string eingabe = request("operand");
            if (eingabe != "")
            {
                while (!double.TryParse(eingabe, out _operand)) { eingabe = request("operand"); }
            }
            return _operand;
        }
        /// <summary>
        /// Lädt die geforderte Operation von stdin.
        /// </summary>
        /// <returns>String, eine gültige Operation</returns>
        string loadOperation()
        {
            string op = "";
            while (!Operations.Contains(op)) { op = request("operation"); }
            return op;
        }
        /// <summary>
        /// Lädt den Operator von stdin.
        /// </summary>
        /// <returns>String, die geforderte Operation</returns>
        double loadOperator()
        {
            string s = "";
            double op;
            while (!double.TryParse(s, out op)) { s = request("operator"); }
            return op;
        }

        /* **************************************
         * i/o layer (gateway) ******************
         * ************************************ */
        /// <summary>
        /// Methode zur Entgegennahme der Benutzereingaben.
        /// </summary>
        /// <param name="subject">Subjekt das angefordert wird (Operand, Operation[, Operator])</param>
        /// <returns>Das angeforderte Subjekt</returns>
        string request(string subject)
        {
            switch (subject)
            {
                case "operand":
                    Console.WriteLine("\n\rBitte Operand eingeben ({0}):", _operand);
                    break;
                case "operation":
                    Console.WriteLine("Bitte gültige Operation angeben:");
                    break;
                case "operator":
                    Console.WriteLine("\n\rBitte Operator angeben:");
                    break;
            }
            return Console.ReadLine().Trim();
        }
    }
}
