using EvilCar.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EvilCar.BL
{
    class XmlManager
    {
        private XmlDocument doc = new XmlDocument();
        private string location;

        public XmlManager(String location)
        {
            doc.Load(location);
            this.location = location;
        }

        //find a user based on their userName
        public EvilCarUser getUserInformation(string type, string searchInput)
        {
            EvilCarUser user = new EvilCarUser();

            try
            {
                user.FirstName = findInformation(type, "userName", searchInput, "firstName").ToString();
                user.LastName = findInformation(type, "userName", searchInput, "lastName").ToString();
                user.UserID = new Guid(findInformation(type, "userName", searchInput, "guid"));
                user.UserName = searchInput;

                //Customers can't auth against system => no need for a password
                if (type != "customer")
                {
                    user.Password = findInformation(type, "userName", searchInput, "password").ToString();
                }
            } catch (Exception ex)
            {
                throw new System.NullReferenceException("User Not Found");
            }
            
            return user;
        }

        //change the value of an already existing node attribute
        public void changeInformationBasedOnGuid(string element, string guid, string attribute, string value)
        {
            XmlNode node = findNodeBasedOnAttributeValue(element, "guid", guid);
            node.Attributes[attribute].Value = value;
            doc.Save(location);
        }

        //create a new user node
        public void newUserNode(EvilCarUser user)
        {
            string type;
            string nodeCollection;
            XmlNode collection; 

            //differ between Admin and Manager
            if (user.Type == EvilCarUser.UserType.ADMIN)
            {
                type = "admin";
                nodeCollection = "userDB/adminCollection";
            } else if (user.Type == EvilCarUser.UserType.FLEET_MANAGER)
            {
                type = "manager";
                nodeCollection = "userDB/managerCollection";
            } else
            {
                type = "customer";
                nodeCollection = "userDB/customerCollection";
            }

            collection = doc.SelectSingleNode(nodeCollection);

            //Set attributes from user object
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, type, null);
            XmlAttribute xa = doc.CreateAttribute("firstName");
            xa.Value = user.FirstName;
            XmlAttribute xb = doc.CreateAttribute("lastName");
            xb.Value = user.LastName;
            XmlAttribute xc = doc.CreateAttribute("guid");
            xc.Value = user.UserID.ToString();
            XmlAttribute xd = doc.CreateAttribute("userName");
            xd.Value = user.UserName;
            XmlAttribute xe = doc.CreateAttribute("password");
            xe.Value = "StartPassword";

            newNode.Attributes.Append(xa);
            newNode.Attributes.Append(xb);
            newNode.Attributes.Append(xc);
            newNode.Attributes.Append(xd);
            newNode.Attributes.Append(xe);

            collection.AppendChild(newNode);

            doc.Save(location);
        }

        #region HelperFunctions
        //Helper Function to find a node based on a specific attribute value
        private XmlNode findNodeBasedOnAttributeValue(string elementName, string attribute, string searchValue)
        {
            string searchQuery = "//" + elementName + "[@" + attribute + "='" + searchValue + "']";
            XmlNodeList xnList = doc.SelectNodes(searchQuery);
            return xnList[0];
        }

        //find an attribute value based on the element name and the knowledge of another attribute value, e.g. userName
        private string findInformation(string elementName, string givenAttribute, string searchValue, string searchAttributeValue)
        {
            XmlNode node = findNodeBasedOnAttributeValue(elementName, givenAttribute, searchValue);
            string attr = node.Attributes[searchAttributeValue]?.InnerText;
            return attr;
        }
        #endregion
    }
}
