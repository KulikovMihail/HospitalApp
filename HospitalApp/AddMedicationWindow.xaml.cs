using HospitalApp.Models;
using System.Windows;

namespace HospitalApp
{
    public partial class AddMedicationWindow : Window
    {
        private HospitalEntities _context;

        public AddMedicationWindow(HospitalEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newMedication = new Medicines // Предполагается, что у вас есть модель Medication
            {
                Name = txtName.Text,
                Dosage = txtDosage.Text,
                Stock = int.Parse(txtStock.Text)
            };

            _context.Medicines.Add(newMedication);
            _context.SaveChanges();
            DialogResult = true;
            this.Close();
        }
    }
}