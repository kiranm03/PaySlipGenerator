using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaySlipGenerator.Model;

namespace PaySlipGenerator.Test.Unit.Model
{
    [TestClass]
    public class EmployeeTest
    {
        private Employee _subject;
        private const string FIRST_NAME = "TestFirstName";
        private const string LAST_NAME = "TestLastName";
        private const double ANNUAL_SALARY = 140000;
        private const float SUPER_RATE = 9.5F;

        [TestMethod]
        public void PersonCtor_ValidData_ReturnsPerson()
        {
            //Arrange
            var expected = typeof(Employee);
            //Act
            var actual = new Employee(FIRST_NAME, LAST_NAME, ANNUAL_SALARY, SUPER_RATE);
            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.GetType());
        }        

        [TestMethod]
        public void Name_ValidData_ReturnsByConcatingFirstNameLastName()
        {
            //Arrange
            var expected = $"{FIRST_NAME} {LAST_NAME}";
            _subject = new Employee(FIRST_NAME, LAST_NAME, ANNUAL_SALARY, SUPER_RATE);
            //Act
            var actual = _subject.Name;
            //Assert
            Assert.AreEqual(expected, actual);
        }        
    }    
}
