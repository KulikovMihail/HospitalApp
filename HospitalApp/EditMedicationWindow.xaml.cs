using HospitalApp.Models;
using System;
using System.Windows;

namespace HospitalApp
{
    public partial class EditMedicationWindow : Window
    {
        private HospitalEntities _context;
        private Medicines _selectedMedication;

        public EditMedicationWindow(HospitalEntities context, Medicines selectedMedication)
        {
            InitializeComponent();
            _context = context;
            _selectedMedication = selectedMedication;

            txtName.Text = _selectedMedication.Name;
            txtDosage.Text = _selectedMedication.Dosage;
            txtStock.Text = _selectedMedication.Stock.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _selectedMedication.Name = txtName.Text;
            _selectedMedication.Dosage = txtDosage.Text;
            _selectedMedication.Stock = int.Parse(txtStock.Text);

            _context.SaveChanges();
            DialogResult = true;
            this.Close();
        }
    }
}