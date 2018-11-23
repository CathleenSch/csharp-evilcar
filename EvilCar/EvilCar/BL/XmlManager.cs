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

        public XmlManager(string location)
        {
            doc.Load(location);
            this.location = location;
        }

        #region userManagement
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
            }
            else if (user.Type == EvilCarUser.UserType.FLEET_MANAGER)
            {
                type = "manager";
                nodeCollection = "userDB/managerCollection";
            }
            else
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

        #endregion


        #region Car and Fleet Management
        public void newCarNode(Car car)
        {
            string nodeCollection = "fleetDB/carDB";
            XmlNode collection;

            collection = doc.SelectSingleNode(nodeCollection);

            //Set attributes from user object
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "car", null);
            XmlAttribute xa = doc.CreateAttribute("type");
            xa.Value = car.CarType.ToString();
            XmlAttribute xb = doc.CreateAttribute("price");
            xb.Value = car.PricePerHour.ToString();
            XmlAttribute xc = doc.CreateAttribute("guid");
            xc.Value = car.CarId.ToString();
            XmlAttribute xd = doc.CreateAttribute("fleetGuid");
            xd.Value = car.FleetGuid.ToString();
            XmlAttribute xe = doc.CreateAttribute("status");
            xe.Value = car.CarStatus.ToString();

            newNode.Attributes.Append(xa);
            newNode.Attributes.Append(xb);
            newNode.Attributes.Append(xc);
            newNode.Attributes.Append(xd);
            newNode.Attributes.Append(xe);

            collection.AppendChild(newNode);

            doc.Save(location);
        }

        public Fleet getFleetInformation(Guid id)
        {
            Fleet fleet = new Fleet();

            try
            {
                fleet.Name = findInformation("branch", "guid", id.ToString("D"), "name");
                fleet.FleetId = id;
            }
            catch (Exception ex)
            {
                throw new System.NullReferenceException("Car Not Found");
            }

            return fleet;
        }

        public List<Car> getCarInformation(Guid fleetId)
        {
            List<Car> cars = new List<Car>();

            try
            {
                XmlNodeList xmlNodeList = findNodesBasedOnAttributeValue("car", "fleetGuid", fleetId.ToString());
                foreach (XmlNode n in xmlNodeList)
                {
                    // Add cars to list
                    cars.Add(new Car() {
                        CarStatus = (Car.Status)Enum.Parse(typeof(Car.Status), n.Attributes["status"].Value, true),
                        CarType = (Car.TypeOfCar)Enum.Parse(typeof(Car.TypeOfCar), n.Attributes["type"].Value, true),
                        PricePerHour = int.Parse(n.Attributes["price"].Value)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new System.NullReferenceException("User Not Found");
            }

            return cars;
        }

        public void newBranchNode(Branch branch)
        {
            string nodeCollection = "fleetDB/branches";
            XmlNode collection;

            collection = doc.SelectSingleNode(nodeCollection);

            //Set attributes from user object
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "branch", null);
            XmlAttribute xa = doc.CreateAttribute("name");
            xa.Value = branch.Name;
            XmlAttribute xb = doc.CreateAttribute("guid");
            xb.Value = branch.BranchId.ToString();

            newNode.Attributes.Append(xa);
            newNode.Attributes.Append(xb);


            collection.AppendChild(newNode);

            doc.Save(location);
        }

        public void newManagerNode(Guid managerId, string branchName)
        {
            XmlNode branch = findNodeBasedOnAttributeValue("branch", "name", branchName);
            if(branch == null)
            {

                Console.WriteLine("Unable to find this brach.");
                throw new System.NullReferenceException("Branch Not Found");
            }

            //Set attributes from user object
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "manager", null);
            XmlAttribute xa = doc.CreateAttribute("guid");
            xa.Value = managerId.ToString();

            newNode.Attributes.Append(xa);

            branch.AppendChild(newNode);

            doc.Save(location);
        }

        #endregion

        #region General
        public void removeNode(string elementName, string attribute, string searchValue)
        {
            XmlNode node = findNodeBasedOnAttributeValue(elementName, attribute, searchValue);
            XmlNode parentNode = node.ParentNode;
            if (checkChildCount(parentNode) > 1) {
                parentNode.RemoveChild(node);
                doc.Save(location);
            } else
            {
                throw new System.InvalidOperationException("Can't remove last manager");
            }
        }

        private int checkChildCount(XmlNode node)
        {
            int count = node.ChildNodes.Count;
            return count;
        }
        #endregion


        #region HelperFunctions
        //change the value of an already existing node attribute
        public void changeInformationBasedOnGuid(string element, string guid, string attribute, string value)
        {
            XmlNode node = findNodeBasedOnAttributeValue(element, "guid", guid);
            node.Attributes[attribute].Value = value;
            doc.Save(location);
        }

        //Helper Function to find a node based on a specific attribute value
        private XmlNode findNodeBasedOnAttributeValue(string elementName, string attribute, string searchValue)
        {
            string searchQuery = "//" + elementName + "[@" + attribute + "='" + searchValue + "']";
            XmlNodeList xnList = doc.SelectNodes(searchQuery);
            return xnList[0];
        }

        private XmlNodeList findNodesBasedOnAttributeValue(string elementName, string attribute, string searchValue)
        {
            string searchQuery = "//" + elementName + "[@" + attribute + "='" + searchValue + "']";
            XmlNodeList xnList = doc.SelectNodes(searchQuery);
            return xnList;
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
