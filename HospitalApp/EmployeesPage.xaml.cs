using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApp
{
    public partial class EmployeesPage : Page
    {
        private HospitalEntities _context;

        public EmployeesPage()
        {
            InitializeComponent();
            _context = new HospitalEntities();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            dgEmployees.ItemsSource = _context.Employees.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow(_context);
            if (addEmployeeWindow.ShowDialog() == true)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is Employees selectedEmployee)
            {
                // Реализовать окно редактирования для сотрудника
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для редактирования.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is Employees selectedEmployee)
            {
                _context.Employees.Remove(selectedEmployee);
                _context.SaveChanges();
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
            }
        }
    }
}