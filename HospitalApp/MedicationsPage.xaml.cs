using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для MedicationsPage.xaml
    /// </summary>
    public partial class MedicationsPage : Page
    {
        private HospitalEntities _context = new HospitalEntities();

        public MedicationsPage()
        {
            InitializeComponent();
            _context = new HospitalEntities();
            LoadMedications();
        }

        private void LoadMedications()
        {
            dgMedications.ItemsSource = _context.Medicines.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMedicationWindow addWindow = new AddMedicationWindow(_context);
            if (addWindow.ShowDialog() == true)
            {
                LoadMedications(); // обновляем список препаратов
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedications.SelectedItem is Medicines selectedMedication)
            {
                EditMedicationWindow editWindow = new EditMedicationWindow(_context, selectedMedication);
                if (editWindow.ShowDialog() == true)
                {
                    LoadMedications(); // обновляем список препаратов
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите препарат для редактирования.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedications.SelectedItem == null) return;
            var selected = dgMedications.SelectedItem as Medicines;
            if (selected == null) return;
                if (MessageBox.Show($"Вы уверены, что хотите удалить препарат {selected.Name}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Medicines.Remove(selected);
                        _context.SaveChanges();
                        LoadMedications();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении препарата: {ex.Message}");
                    }
                }
                
            else
            {
                MessageBox.Show("Пожалуйста, выберите препарат для удаления.");
            }
        }
    }
}
