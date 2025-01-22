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
    /// Логика взаимодействия для MedicationsPage.xaml
    /// </summary>
    public partial class MedicationsPage : Page
    {
        private Entities _context = new Entities();

        public MedicationsPage()
        {
            InitializeComponent();
            LoadMedications();
        }

        private void LoadMedications()
        {
            dgMedications.ItemsSource = _context.Medications.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var m = new Medications
            {
                Name = "Анальгин",
                Description = "Обезболивающее",
                Quantity = 100
            };
            _context.Medications.Add(m);
            _context.SaveChanges();
            LoadMedications();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedications.SelectedItem == null) return;
            var med = dgMedications.SelectedItem as Medications;
            if (med == null) return;

            // В реальном проекте - окно редактирования, здесь упрощённо:
            med.Quantity += 10;
            _context.SaveChanges();
            LoadMedications();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedications.SelectedItem == null) return;
            var med = dgMedications.SelectedItem as Medications;
            if (med == null) return;

            _context.Medications.Remove(med);
            _context.SaveChanges();
            LoadMedications();
        }
    }
}
