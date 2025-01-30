using System.Windows;

namespace HospitalApp
{
    public partial class AddEmployeeWindow : Window
    {
        private HospitalEntities _context;

        public AddEmployeeWindow(HospitalEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newEmployee = new Employees
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Position = txtPosition.Text,
                Email = txtEmail.Text
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            DialogResult = true;
            this.Close();
        }
    }
}