using EPayroll.ServiceModels;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;

        public ProfileViewModel(IEmployeeService employeeService, INavigationService navigationService) : base(navigationService)
        {
            _employeeService = employeeService;

            var details = employeeService.GetDetail(new Guid("515f1407-f98f-4f15-8459-08d778b630cb"));
            Name = details.Name;
            Position = details.Position.Name;
            Age = details.Age;
            Gender = details.Gender;
            IdentifyNumber = details.IdentifyNumber;
            Mode = details.SalaryMode.Mode;
            Level = details.SalaryLevel.Level;
        }

        private string _name;
        private string _position;
        private int _age;
        private bool _gender;
        private string _identifyNumber;
        private string _mode;
        private string _level;

        #region 
        public string Name { get { return _name; } set { SetValue(ref _name, value); } }
        public string Position { get { return _position; } set { SetValue(ref _position, value); } }
        public int Age { get { return _age; } set { SetValue(ref _age, value); } }
        public bool Gender { get { return _gender; } set { SetValue(ref _gender, value); } }
        public string IdentifyNumber { get { return _identifyNumber; } set { SetValue(ref _identifyNumber,value); } }
        public string Mode { get { return _mode; } set { SetValue(ref _mode,value); } }
        public string Level { get { return _level; } set { SetValue(ref _level,value); } }
        #endregion
        public ImageSource ImageSource { get { return ImageSource.FromResource("EPayroll.Images.hana.jpg"); } }
    }
}
