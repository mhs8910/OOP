using System.Collections.Generic;

namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: rules for customer records.
    public class CustomerService
    {
        private CustomerRepository _repository;
        private SolarCalculatorService _calculator;

        public CustomerService()
        {
            _repository = new CustomerRepository();
            _calculator = new SolarCalculatorService();
        }

        // Calculates the project price automatically, then saves.
        // Returns false if the storage is full (1000 customers).
        public bool AddCustomer(Customer customer)
        {
            if (_repository.GetAll().Count >= 1000)
            {
                return false;
            }

            customer.ProjectPrice = _calculator.CalculatePrice(customer.SystemCapacity);
            _repository.Save(customer);
            return true;
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        // Returns the customer with this phone number, or null.
        public Customer FindCustomerByPhone(string phone)
        {
            List<Customer> customers = _repository.GetAll();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Phone == phone)
                {
                    return customers[i];
                }
            }
            return null;
        }

        // Removes the customer from the list and writes the list back to the file.
        public bool DeleteCustomerByPhone(string phone)
        {
            List<Customer> customers = _repository.GetAll();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Phone == phone)
                {
                    customers.RemoveAt(i);
                    _repository.SaveAll(customers);
                    return true;
                }
            }
            return false;
        }

        public void ExportCustomers()
        {
            _repository.ExportToTxt(_repository.GetAll());
        }
    }
}
