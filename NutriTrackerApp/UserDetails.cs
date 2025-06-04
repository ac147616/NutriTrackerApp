using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Passwordkey { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double UserWeight { get; set; }
        public double UserHeight { get; set; }
        public DateOnly SignUpDate { get; set; }
        public UserDetails(int userID, string firstName, string lastName, string emailID, string passwordkey, int age, string gender, double userWeight, double userHeight, DateOnly signUpDate)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            EmailID = emailID;
            Passwordkey = passwordkey;
            Age = age;
            Gender = gender;
            UserWeight = userWeight;
            UserHeight = userHeight;
            SignUpDate = signUpDate;

        }
    }
}