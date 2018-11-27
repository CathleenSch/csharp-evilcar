using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.BL {
    class Services {
        public string generateUsername (string firstName, string lastName, string userType) {
            string username = firstName[0] + lastName;
            int count = 1;

            XmlManager xml = new XmlManager("/*need location*/");

            for (int i = 1; i < firstName.Length; i++) {
                try {
                    xml.searchSystemUser(username);
                } catch (Exception e) {
                    break;
                }

                username = "";
                for (int j = 0; j <= i; j++) {
                    username = username + firstName[j];
                }
                username = username + lastName;
            }

            while (true) {
                username = firstName + lastName + count;
                try {
                    xml.searchSystemUser(username);
                } catch (Exception e) {
                    break;
                }
                count++;
            }

            return username;
        }
    }
}
