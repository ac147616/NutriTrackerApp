using System.Text;
using System.Threading.Tasks;

namespace NutriTrackerApp
{
    //This is my class for the table users.tblUserDetails
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
        public string SignUpDate { get; set; } //This is a string variable because its easier to get input this way, later when it is passed into the DB it is converted to Date
        public UserDetails(int userID, string firstName, string lastName, string emailID, string passwordkey, int age, string gender, double userWeight, double userHeight, string signUpDate)
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