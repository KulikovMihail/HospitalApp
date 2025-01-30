using System;
using System.Windows;

namespace HospitalApp
{
    public partial class MainWindow : Window
    {
        private Employee currentUser;

        public MainWindow()
        {
            InitializeComponent();
            currentUser = new Employee { Name = "Имя сотрудника", UserRole = Role.Doctor }; // пример
            OpenPatientsPage();
        }

        private void OpenPatientsPage()
        {
            // Мы не используем navigate здесь, просто открываем PatientsPage
            PatientsPage patientsPage = new PatientsPage(currentUser);
            MainFrame.Navigate(patientsPage);
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            // Передаем currentUser при вызове PatientsPage
            PatientsPage patientsPage = new PatientsPage(currentUser);
            MainFrame.Navigate(patientsPage);
        }

        private void btnMedications_Click(object sender, RoutedEventArgs e)
        {
            // Здесь предполагается, что MedicationsPage также принимает currentUser
            // Если нет, измените его соответствующим образом
            MainFrame.Navigate(new MedicationsPage());
        }
    }
}