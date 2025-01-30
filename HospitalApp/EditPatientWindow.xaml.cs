using System;
using System.Windows;

namespace HospitalApp
{
    public partial class EditPatientWindow : Window
    {
        private HospitalEntities _context;
        private Patients _selectedPatient;

        public EditPatientWindow(HospitalEntities context, Patients selectedPatient)
        {
            InitializeComponent();
            _context = context;
            _selectedPatient = selectedPatient;

            txtName.Text = _selectedPatient.Name;
            txtSurname.Text = _selectedPatient.SureName;
            dpBirthDate.SelectedDate = _selectedPatient.BirthDate;
            txtDiagnosis.Text = _selectedPatient.Diagnosis;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _selectedPatient.Name = txtName.Text;
            _selectedPatient.SureName = txtSurname.Text;
            _selectedPatient.BirthDate = dpBirthDate.SelectedDate ?? DateTime.Now;
            _selectedPatient.Diagnosis = txtDiagnosis.Text;

            _context.SaveChanges();
            DialogResult = true;
            this.Close();
        }
    }
}