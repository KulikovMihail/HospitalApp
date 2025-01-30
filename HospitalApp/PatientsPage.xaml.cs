using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalApp
{
    /// <summary>
    /// Логика взаимодействия для PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : Page
    {
        private HospitalEntities _context = new HospitalEntities();
        private Employee CurrentUser;

        public PatientsPage(Employee loggedInEmployee)
        {
            InitializeComponent();
            CurrentUser = loggedInEmployee;
            LoadPatients();
            SetButtonVisibility(CurrentUser.UserRole);
        }

        private void SetButtonVisibility(Role userRole)
        {
            // Скрытие кнопок редактирования и удаления для интернов
            if (userRole == Role.Intern)
            {
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                // Если есть другие кнопки, которые нужно скрыть, вы можете сделать это здесь
            }
        }

        private void LoadPatients(string filter = null)
        {
            try
            {
                var patientsQuery = _context.Patients.AsQueryable();

                // Применяем фильтр
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    var lowerFilter = filter.ToLower();
                    DateTime birthDateFilter;

                    // Проверяем, является ли фильтр датой
                    bool isDate = DateTime.TryParse(filter, out birthDateFilter);

                    // Выполняем фильтрацию
                    patientsQuery = patientsQuery
                        .Where(p => p.Name.ToLower().Contains(lowerFilter) ||
                                    p.SureName.ToLower().Contains(lowerFilter) ||
                                    p.Diagnosis.ToLower().Contains(lowerFilter)); // Сравниваем только по дате без времени
                }

                dgPatients.ItemsSource = patientsQuery.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addPatientWindow = new AddPatientWindow(_context);
            if (addPatientWindow.ShowDialog() == true)
            {
                LoadPatients();
            }
        }

        private void dgPatients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgPatients.SelectedItem == null) return;
            var selected = dgPatients.SelectedItem as Patients;
            if (selected == null) return;

            var editPatientWindow = new EditPatientWindow(_context, selected);
            editPatientWindow.ShowDialog();
            LoadPatients();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is Patients selectedPatient)
            {
                EditPatientWindow editWindow = new EditPatientWindow(_context, selectedPatient);
                if (editWindow.ShowDialog() == true) // если окно закрылось с успешным результатом
                {
                    LoadPatients(); // обновляем список пациентов
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента для редактирования.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem == null) return;
            var selected = dgPatients.SelectedItem as Patients;
            if (selected == null) return;

            // Подтверждение удаления
            if (MessageBox.Show($"Вы уверены, что хотите удалить пациента {selected.Name}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Patients.Remove(selected);
                    _context.SaveChanges();
                    LoadPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пациента: {ex.Message}");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadPatients(txtSearch.Text);
        }
    }
}