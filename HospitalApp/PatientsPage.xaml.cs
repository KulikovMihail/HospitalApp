using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalApp
{
    /// <summary>
    /// Логика взаимодействия для PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {
        // Это класс контекста, сгенерированный EDMX. 
        // Может называться HospitalDBEntities, HospitalModelContainer и т.п.
        // Проверьте точное имя в вашем .Context.cs
        private Entities _context = new Entities();

        public PatientsPage()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            // Просто читаем всех пациентов из БД
            var patients = _context.Patients.ToList();
            dgPatients.ItemsSource = patients;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Примерно: Открываем диалоговое окно/или отдельную страницу
            // Упростим: прямо тут создадим нового пациента (в реальном проекте лучше диалог)
            var newPatient = new Patients
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Age = 30,
                Diagnosis = "Грипп"
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();   // вставка в БД
            LoadPatients();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem == null) return;
            // Приводим SelectedItem к типу Patients (сгенерированный класс)
            var selected = dgPatients.SelectedItem as Patients;
            if (selected == null) return;

            // Изменим какие-то поля (в реальном проекте - форма редактирования)
            selected.Diagnosis = "Изменённый диагноз";
            _context.SaveChanges();  // EF отслеживает изменения
            LoadPatients();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem == null) return;
            var selected = dgPatients.SelectedItem as Patients;
            if (selected == null) return;

            // Удаляем
            _context.Patients.Remove(selected);
            _context.SaveChanges();
            LoadPatients();
        }
    }
}
