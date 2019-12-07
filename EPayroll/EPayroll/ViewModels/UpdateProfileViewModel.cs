using EPayroll.ServiceModels;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class UpdateProfileViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;

        public UpdateProfileViewModel(IEmployeeService employeeService, INavigationService navigationService) : base(navigationService)
        {
            _employeeService = employeeService;
            var details = employeeService.GetDetail(new Guid ("515f1407-f98f-4f15-8459-08d778b630cb")).Result;
            Name = details.Name;
            Position = details.Position;
            Age = details.Age;
            Gender = details.Gender;
            IdentifyNumber = details.IdentifyNumber;
            Mode = details.SalaryMode;
            Level = details.SalaryLevel;
        }
        #region 
        public string Name { get; set; }
        public PositionServiceViewModel Position { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string IdentifyNumber { get; set; }
        public SalaryModeServiceViewModel Mode { get; set; }
        public SalaryLevelServiceViewModel Level { get; set; }
        #endregion
        public ImageSource ImageSource { get { return ImageSource.FromResource("EPayroll.Images.hana.jpg"); } }
    }
}