using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class EvilCarUser
    {
        public enum UserType { CUSTOMER, ADMIN, FLEET_MANAGER };

        protected string firstName;
        protected string lastName;
        protected string userName;
        protected Guid userID;
        protected UserType userType;

        //intern constructor to inherit by Customer class
        protected EvilCarUser(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            userID = Guid.NewGuid();
        }

        //offical constructor where type of user must be entered
        public EvilCarUser(string firstName, string lastName, string userName, UserType type)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            userType = type;
            userID = Guid.NewGuid();
        }

        //Getter and Setter
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string UserName { get => userName; set => userName = value; }
        public Guid UserID { get => userID; }
        public string Type { get => userType.ToString(); }

    }
}
