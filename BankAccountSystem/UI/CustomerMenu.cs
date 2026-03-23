using BusinessLogic.Dtos;
using BusinessLogic.Services.Contract;

namespace BankAccountSystem.UI
{
    public class CustomerMenu
    {
        private readonly ICustomerService _customerService;

        public CustomerMenu(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void Show()
        {
            while (true)
            {
                MenuHandler.ShowHeader("MÜŞTƏRİ İDARƏETMƏSİ");
                MenuHandler.ShowMenuItem(1, "👤", "Yeni müştəri əlavə et");
                MenuHandler.ShowMenuItem(2, "📋", "Bütün aktiv müştərilər");
                MenuHandler.ShowMenuItem(3, "🔍", "FİN ilə axtar");
                MenuHandler.ShowMenuItem(4, "📧", "Email ilə axtar");
                MenuHandler.ShowMenuItem(5, "✏️", "Müştəri məlumatlarını yenilə");
                MenuHandler.ShowMenuItem(6, "🗑️", "Müştərini sil");
                MenuHandler.ShowExitItem(0);

                var choice = MenuHandler.ReadMenuChoice(0, 6);

                switch (choice)
                {
                    case 1: AddCustomer(); break;
                    case 2: ListAllActive(); break;
                    case 3: SearchByNationalId(); break;
                    case 4: SearchByEmail(); break;
                    case 5: UpdateCustomer(); break;
                    case 6: DeleteCustomer(); break;
                    case 0: return;
                }
            }
        }

        private void AddCustomer()
        {
            MenuHandler.ShowHeader("YENİ MÜŞTƏRİ");

            var dto = new CreateCustomerDto
            {
                FullName = MenuHandler.ReadInput("Ad Soyad"),
                NationalId = MenuHandler.ReadInput("FİN"),
                Email = MenuHandler.ReadInput("Email"),
                Phone = MenuHandler.ReadInput("Telefon"),
                DateOfBirth = MenuHandler.ReadDate("Doğum tarixi")
            };

            try
            {
                _customerService.Add(dto);
                MenuHandler.ShowSuccess("Müştəri uğurla əlavə edildi.");
            }
            catch (Exception ex)
            {
                MenuHandler.ShowError(ex.Message);
            }
        }

        private void ListAllActive()
        {
            MenuHandler.ShowHeader("AKTİV MÜŞTƏRİLƏR");

            var page = 1;
            var pageSize = 10;

            while (true)
            {
                var customers = _customerService.GetAllActive(page, pageSize);

                if (!customers.Any())
                {
                    MenuHandler.ShowWarning("Müştəri tapılmadı.");
                    return;
                }

                foreach (var c in customers)
                    PrintCustomer(c);

                MenuHandler.ShowDivider();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  Səhifə: {page}  |  Göstərilən: {customers.Count}");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("  [N] Növbəti səhifə   [G] Geri   [0] Çıx");
                Console.ResetColor();
                Console.Write("  ➤  ");

                var key = Console.ReadLine()?.Trim().ToUpper();

                if (key == "N") page++;
                else if (key == "G" && page > 1) page--;
                else if (key == "0") return;
            }
        }

        private void SearchByNationalId()
        {
            MenuHandler.ShowHeader("FİN İLƏ AXTAR");

            var nationalId = MenuHandler.ReadInput("FİN");
            var customer = _customerService.GetByNationalId(nationalId);

            if (customer == null)
            {
                MenuHandler.ShowWarning("Müştəri tapılmadı.");
                return;
            }

            PrintCustomer(customer);
            MenuHandler.ShowDivider();
            Console.ReadLine();
        }

        private void SearchByEmail()
        {
            MenuHandler.ShowHeader("EMAİL İLƏ AXTAR");

            var email = MenuHandler.ReadInput("Email");
            var customer = _customerService.GetByEmail(email);

            if (customer == null)
            {
                MenuHandler.ShowWarning("Müştəri tapılmadı.");
                return;
            }

            PrintCustomer(customer);
            MenuHandler.ShowDivider();
            Console.ReadLine();
        }

        private void UpdateCustomer()
        {
            MenuHandler.ShowHeader("MÜŞTƏRİ YENİLƏ");

            var id = MenuHandler.ReadInt("Müştəri ID");
            var customer = _customerService.GetById(id);

            if (customer == null)
            {
                MenuHandler.ShowWarning("Müştəri tapılmadı.");
                return;
            }

            PrintCustomer(customer);
            MenuHandler.ShowDivider();

            var dto = new UpdateCustomerDto
            {
                FullName = MenuHandler.ReadInput("Yeni ad soyad"),
                Email = MenuHandler.ReadInput("Yeni email"),
                Phone = MenuHandler.ReadInput("Yeni telefon")
            };

            try
            {
                _customerService.Update(id, dto);
                MenuHandler.ShowSuccess("Müştəri uğurla yeniləndi.");
            }
            catch (Exception ex)
            {
                MenuHandler.ShowError(ex.Message);
            }
        }

        private void DeleteCustomer()
        {
            MenuHandler.ShowHeader("MÜŞTƏRİ SİL");

            var id = MenuHandler.ReadInt("Müştəri ID");
            var customer = _customerService.GetById(id);

            if (customer == null)
            {
                MenuHandler.ShowWarning("Müştəri tapılmadı.");
                return;
            }

            PrintCustomer(customer);
            MenuHandler.ShowDivider();

            if (!MenuHandler.Confirm("Müştərini silmək istədiyinizdən əminsiniz?"))
            {
                MenuHandler.ShowWarning("Əməliyyat ləğv edildi.");
                return;
            }

            try
            {
                _customerService.SoftDelete(id);
                MenuHandler.ShowSuccess("Müştəri uğurla silindi.");
            }
            catch (Exception ex)
            {
                MenuHandler.ShowError(ex.Message);
            }
        }

        private static void PrintCustomer(CustomerDto c)
        {
            MenuHandler.ShowDivider();
            MenuHandler.ShowInfo("ID:", c.Id.ToString());
            MenuHandler.ShowInfo("Ad Soyad:", c.FullName);
            MenuHandler.ShowInfo("FİN:", c.NationalId);
            MenuHandler.ShowInfo("Email:", c.Email);
            MenuHandler.ShowInfo("Telefon:", c.Phone);
            MenuHandler.ShowInfo("Doğum tarixi:", c.DateOfBirth.ToString("dd.MM.yyyy"));
            MenuHandler.ShowInfo("Qeydiyyat:", c.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
            MenuHandler.ShowInfo("Hesab sayı:", c.AccountCount.ToString());
        }
    }
}