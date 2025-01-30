using System;
using System.Windows;

namespace HospitalApp
{
    public partial class AddPatientWindow : Window
    {
        private HospitalEntities _context;

        public AddPatientWindow(HospitalEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newPatient = new Patients
            {
                Name = txtName.Text,
                SureName = txtSurname.Text,
                BirthDate = dpBirthDate.SelectedDate ?? DateTime.Now,
                Diagnosis = txtDiagnosis.Text
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();
            DialogResult = true;
            this.Close();
        }
    }
}