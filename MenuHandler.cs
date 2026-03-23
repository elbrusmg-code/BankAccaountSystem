namespace BankAccountSystem.UI
{
    public static class MenuHandler
    {
        // Rənglər
        private static readonly ConsoleColor PrimaryColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
        private static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        private static readonly ConsoleColor SecondaryColor = ConsoleColor.DarkCyan;

        public static void ShowHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = PrimaryColor;
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║         🏦  AZƏR BANK — İDARƏETMƏ SİSTEMİ       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════╣");
            Console.WriteLine($"║  {CenterText(title, 48)}  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowMenuItem(int number, string icon, string text)
        {
            Console.ForegroundColor = SecondaryColor;
            Console.Write($"  [{number}]");
            Console.ResetColor();
            Console.WriteLine($"  {icon}  {text}");
        }

        public static void ShowExitItem(int number)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [{number}]  ←  Geri");
            Console.ResetColor();
        }

        public static void ShowPrompt()
        {
            Console.WriteLine();
            Console.ForegroundColor = PrimaryColor;
            Console.Write("  ➤  Seçiminiz: ");
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = SuccessColor;
            Console.WriteLine($"  ✔  {message}");
            Console.ResetColor();
            Pause();
        }

        public static void ShowError(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine($"  ✖  Xəta: {message}");
            Console.ResetColor();
            Pause();
        }

        public static void ShowWarning(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = WarningColor;
            Console.WriteLine($"  ⚠  {message}");
            Console.ResetColor();
            Pause();
        }

        public static void ShowInfo(string label, string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  {label,-20}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void ShowDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ──────────────────────────────────────────────");
            Console.ResetColor();
        }

        public static string ReadInput(string label)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  {label}: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            var input = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.ResetColor();
            return input;
        }

        public static decimal ReadDecimal(string label)
        {
            while (true)
            {
                var input = ReadInput(label);
                if (decimal.TryParse(input, out var value) && value > 0)
                    return value;
                Console.ForegroundColor = ErrorColor;
                Console.WriteLine("  ✖  Düzgün məbləğ daxil edin.");
                Console.ResetColor();
            }
        }

        public static int ReadInt(string label)
        {
            while (true)
            {
                var input = ReadInput(label);
                if (int.TryParse(input, out var value) && value > 0)
                    return value;
                Console.ForegroundColor = ErrorColor;
                Console.WriteLine("  ✖  Düzgün rəqəm daxil edin.");
                Console.ResetColor();
            }
        }

        public static DateTime ReadDate(string label)
        {
            while (true)
            {
                var input = ReadInput($"{label} (dd.MM.yyyy)");
                if (DateTime.TryParseExact(input, "dd.MM.yyyy",
                    null, System.Globalization.DateTimeStyles.None, out var date))
                    return date;
                Console.ForegroundColor = ErrorColor;
                Console.WriteLine("  ✖  Düzgün tarix daxil edin. Nümunə: 15.06.1990");
                Console.ResetColor();
            }
        }

        public static bool Confirm(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = WarningColor;
            Console.Write($"  ⚠  {message} (b/x): ");
            Console.ResetColor();
            var input = Console.ReadLine()?.Trim().ToLower();
            return input == "b";
        }

        public static int ReadMenuChoice(int min, int max)
        {
            while (true)
            {
                ShowPrompt();
                var input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out var choice) && choice >= min && choice <= max)
                    return choice;
                Console.ForegroundColor = ErrorColor;
                Console.WriteLine($"  ✖  Zəhmət olmasa {min}-{max} arasında seçin.");
                Console.ResetColor();
            }
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Davam etmək üçün Enter basın...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static string CenterText(string text, int width)
        {
            if (text.Length >= width) return text;
            int totalPadding = width - text.Length;
            int leftPadding = totalPadding / 2;
            return text.PadLeft(text.Length + leftPadding).PadRight(width);
        }
    }
}