// ============================================================
// Project : Solar Business Management System (C#, 4-layer version)
// Author  : Muhammad Hassan Shafiq
//
//   UI Layer          -> /UI            (WriteLine / ReadLine only)
//   Business Logic    -> /Services      (rules and calculations)
//   Data Access Layer -> /Repositories  (all file handling)
//   Models Layer      -> /Models        (data classes, inheritance)
//   Common            -> /Common        (static helper classes)
//
// Flow:  UI  ->  Service  ->  Repository  ->  .txt file
// ============================================================

namespace SolarManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenuUI mainMenu = new MainMenuUI();
            mainMenu.Show();
        }
    }
}
