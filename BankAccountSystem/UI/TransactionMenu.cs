using BusinessLogic.Dtos;
using BusinessLogic.Services.Contract;

namespace BankAccountSystem.UI
{
    public class TransactionMenu
    {
        private readonly ITransactionService _transactionService;

        public TransactionMenu(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public void Show()
        {
            while (true)
            {
                MenuHandler.ShowHeader("TRANZAKSİYA İDARƏETMƏSİ");
                MenuHandler.ShowMenuItem(1, "📄", "Hesaba görə tranzaksiyalar");
                MenuHandler.ShowMenuItem(2, "📈", "Son 30 günün depozitləri");
                MenuHandler.ShowMenuItem(3, "💤", "İnaktiv hesablar");
                MenuHandler.ShowExitItem(0);

                var choice = MenuHandler.ReadMenuChoice(0, 3);

                switch (choice)
                {
                    case 1: GetByAccountId(); break;
                    case 2: GetLast30DaysDeposits(); break;
                    case 3: GetInactiveAccounts(); break;
                    case 0: return;
                }
            }
        }

        private void GetByAccountId()
        {
            MenuHandler.ShowHeader("HESABA GÖRƏ TRANZAKSİYALAR");

            var accountId = MenuHandler.ReadInt("Hesab ID");
            var transactions = _transactionService.GetByAccountId(accountId);

            if (!transactions.Any())
            {
                MenuHandler.ShowWarning("Bu hesaba aid tranzaksiya tapılmadı.");
                return;
            }

            foreach (var t in transactions)
                PrintTransaction(t);

            MenuHandler.ShowDivider();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  Cəmi: {transactions.Count} tranzaksiya");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void GetLast30DaysDeposits()
        {
            MenuHandler.ShowHeader("SON 30 GÜNÜN DEPOZİTLƏRİ");

            var transactions = _transactionService.GetLast30DaysDeposits();

            if (!transactions.Any())
            {
                MenuHandler.ShowWarning("Son 30 gündə depozit tapılmadı.");
                return;
            }

            foreach (var t in transactions)
                PrintTransaction(t);

            MenuHandler.ShowDivider();
            var total = transactions.Sum(t => t.Amount);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  Cəmi: {transactions.Count} əməliyyat");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Ümumi məbləğ: {total:N2} AZN");
            Console.ResetColor();
            Console.WriteLine();
            Console.ReadLine();
        }

        private void GetInactiveAccounts()
        {
            MenuHandler.ShowHeader("İNAKTİV HESABLAR");

            var transactions = _transactionService.GetInactiveAccounts();

            if (!transactions.Any())
            {
                MenuHandler.ShowWarning("İnaktiv hesab tapılmadı.");
                return;
            }

            foreach (var t in transactions)
                PrintTransaction(t);

            MenuHandler.ShowDivider();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  Cəmi: {transactions.Count} qeyd");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static void PrintTransaction(TransactionDto t)
        {
            MenuHandler.ShowDivider();
            MenuHandler.ShowInfo("ID:", t.Id.ToString());
            MenuHandler.ShowInfo("Hesab nömrəsi:", t.AccountNumber);
            MenuHandler.ShowInfo("Növ:", t.Type.ToString());
            MenuHandler.ShowInfo("Məbləğ:", $"{t.Amount:N2} AZN");
            MenuHandler.ShowInfo("Sonraki balans:", $"{t.BalanceAfter:N2} AZN");
            MenuHandler.ShowInfo("Tarix:", t.OccurredAt.ToString("dd.MM.yyyy HH:mm"));

            if (!string.IsNullOrEmpty(t.Description))
                MenuHandler.ShowInfo("Açıqlama:", t.Description);

            if (t.RelatedAccountId.HasValue)
                MenuHandler.ShowInfo("Əlaqəli hesab ID:", t.RelatedAccountId.Value.ToString());
        }
    }
}