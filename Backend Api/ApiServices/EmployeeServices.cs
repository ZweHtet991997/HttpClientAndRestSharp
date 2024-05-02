using ShareEntities;

namespace Backend_Api.ApiServices
{
    public class EmployeeServices
    {
        public List<EmployeeEntities> EmployeeDataSource()
        {
            List<EmployeeEntities> lstEmployee = new List<EmployeeEntities>();
            #region DataSource
            lstEmployee.Add(new EmployeeEntities { Id = 1, Name = "Mikal", EmployeeId = 0001, Email = "mikal@gmail.com", PhoneNo = "99893822111", Position = "Software Engineer", Department = "IT" });
            lstEmployee.Add(new EmployeeEntities { Id = 2, Name = "Smith", EmployeeId = 0002, Email = "smith@gmail.com", PhoneNo = "19093822742", Position = "QA", Department = "IT" });
            lstEmployee.Add(new EmployeeEntities { Id = 3, Name = "John", EmployeeId = 0003, Email = "john@gmail.com", PhoneNo = "19893822992", Position = "Project Management", Department = "IT" });
            lstEmployee.Add(new EmployeeEntities { Id = 4, Name = "Sindy", EmployeeId = 0004, Email = "sindy@gmail.com", PhoneNo = "62903822107", Position = "Business Analyst", Department = "IT" });
            lstEmployee.Add(new EmployeeEntities { Id = 5, Name = "Mendy", EmployeeId = 0005, Email = "mendy@gmail.com", PhoneNo = "684357822382", Position = "Data Engineer", Department = "IT" });
            #endregion
            return lstEmployee;
        }
        public List<EmployeeEntities> EmployeeList()
        {
            return EmployeeDataSource();
        }

        public int Add(EmployeeEntities entitie)
        {
            if (entitie != null)
            {
                return 1;
            }
            return 0;
        }

        public EmployeeEntities GetEmployeeById(int Id)
        {
            if (Id != 0)
            {
                var employee = EmployeeDataSource().Where(x => x.Id == Id).FirstOrDefault();
                return employee;
            }
            return null;
        }

        public int Update(EmployeeEntities entitie)
        {
            if (entitie != null)
            {
                return 1;
            }
            return 0;
        }

        public int Delete(int Id)
        {
            if (Id != 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
