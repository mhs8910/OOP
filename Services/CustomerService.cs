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
            List<Customer> existingCustomers = _repository.GetAll();
            if (existingCustomers.Count >= 1000)
            {
                return false;
            }

            // A phone number identifies a customer, so do not store duplicates.
            for (int i = 0; i < existingCustomers.Count; i++)
            {
                if (existingCustomers[i].Phone == customer.Phone)
                {
                    return false;
                }
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
        // Returns the customer within the maximum and minimum system capacity range or null.
        public Customer FindCustomerBySystemCapacity(int SystemCapacitymaximum, int SystemCapacityminimum)
        {
            List<Customer> customers = _repository.GetAll();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].SystemCapacity > SystemCapacityminimum && customers[i].SystemCapacity < SystemCapacitymaximum)
                {
                    return customers[i];
                }
            }
            return null;
        }

        //Returns the customer with this System Type, or null
        public Customer FindCustomerBySystemType(string SystemType)
        {
            List<Customer> customers = _repository.GetAll();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].SystemType == SystemType)
                {
                    return customers[i];
                }
            }
            return null;
        }
        public Customer FindCustomerByName(string name)
        {
            List<Customer> customers = _repository.GetAll();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Name == name)
                {

                }
            }
            return null;
        }
    }
}
