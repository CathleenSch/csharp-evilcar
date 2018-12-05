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
        //find a user based on their userName and their type (customer, manager, admin)
        //return the found user
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

            }
            
            return user;
        }

        //Find a user within the system
        //set the type according to location of finding
        public EvilCarUser searchSystemUser(string username)
        {
            EvilCarUser user;
            user = getUserInformation("admin", username);
            if(user.UserID == Guid.Empty)
            {
                user = getUserInformation("manager", username);
                if (user.UserID == Guid.Empty)
                {
                    user = getUserInformation("customer", username);
                    if (user.UserID == Guid.Empty)
                    {
                        throw new System.NullReferenceException("User does not exist in system.");
                    }
                    else
                    {
                        user.Type = EvilCarUser.UserType.CUSTOMER;
                    }
                } else
                {
                    user.Type = EvilCarUser.UserType.FLEET_MANAGER;
                }
            } else
            {
                user.Type = EvilCarUser.UserType.ADMIN;
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
                nodeCollection = "customerCollection";
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

            //Only Admins and Manager have passwords
            if(user.Type != EvilCarUser.UserType.CUSTOMER)
            {
                XmlAttribute xe = doc.CreateAttribute("password");
                xe.Value = user.Password;
                newNode.Attributes.Append(xe);
            }

            newNode.Attributes.Append(xa);
            newNode.Attributes.Append(xb);
            newNode.Attributes.Append(xc);
            newNode.Attributes.Append(xd);

            collection.AppendChild(newNode);

            doc.Save(location);
        }

        #endregion


        #region Car and Fleet Management
        //add a car node to the fleet DB
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
            XmlAttribute xf = doc.CreateAttribute("description");
            xf.Value = car.CarDescription.ToString();

            newNode.Attributes.Append(xa);
            newNode.Attributes.Append(xb);
            newNode.Attributes.Append(xc);
            newNode.Attributes.Append(xd);
            newNode.Attributes.Append(xe);
            newNode.Attributes.Append(xf);

            collection.AppendChild(newNode);

            doc.Save(location);
        }

        //fetch fleet information from fleet DB
        //return fleet object
        public Fleet getFleetInformation(Guid id)
        {
            Fleet fleet = new Fleet();

            try
            {
                XmlNode manager = findNodeBasedOnAttributeValue("manager", "guid", id.ToString("D"));
                fleet.FleetId = Guid.Parse(manager.ParentNode.Attributes["guid"]?.InnerText);
                fleet.Name = manager.ParentNode.Attributes["name"]?.InnerText;
            }
            catch (Exception ex)
            {
                throw new System.NullReferenceException("Car Not Found");
            }

            return fleet;
        }

        //get all cars in a fleet based on the fleet Id
        //returns list of all cars
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

        //create a new branch node
        //add node to fleet DB
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

        //create new manager node
        //add the manager to the fleet specified via name in the parameters
        public void newManagerNode(Guid managerId, string fleetName)
        {
            XmlNode branch = findNodeBasedOnAttributeValue("fleet", "name", fleetName);
            if(branch == null)
            {

                Console.WriteLine("Unable to find this fleet.");
                throw new System.NullReferenceException("Fleet Not Found");
            }

            //Set attributes from user object
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "manager", null);
            XmlAttribute xa = doc.CreateAttribute("guid");
            xa.Value = managerId.ToString();

            newNode.Attributes.Append(xa);

            branch.AppendChild(newNode);

            doc.Save(location);
        }

        //get and return all available Services from the fleet DB
        //contains information on their name, description and fee
        public List<Service> availableServices()
        {
            List<Service> services = new List<Service>();
            XmlNodeList serviceList = doc.SelectNodes("//service");
            foreach (XmlNode s in serviceList)
            {
                // Add services to list
                services.Add(new Service()
                {
                    Name = s.Attributes["name"].Value,
                    Pricing = Convert.ToInt32(s.Attributes["price"].Value)
                });
            }
            return services;
        }

        #endregion

        #region General
        /* region used for general functions
         * required for several tasks related to communicating with the xml
         */
         //remove a node from any xml DB
        public void removeNode(string elementName, string attribute, string searchValue)
        {
            XmlNode parentNode;
            XmlNode node = findNodeBasedOnAttributeValue(elementName, attribute, searchValue);
            if(node == null)
            {
                return;
            } else
            {
                parentNode = node.ParentNode;
            }

            if (checkChildCount(parentNode) > 1) {
                parentNode.RemoveChild(node);
                doc.Save(location);
            } else
            {
                throw new System.InvalidOperationException("Can't remove last manager");
            }
        }

        //check wether a node has children
        //return number of children
        private int checkChildCount(XmlNode node)
        {
            int count = node.ChildNodes.Count;
            return count;
        }
        #endregion


        #region HelperFunctions
        //change the value of an already existing node attribute
        //use guid attribute to identify node which needs changing
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

        //find all nodes that have a specific attribute
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
            if (node == null)
            {
                throw new System.NullReferenceException("Unable to find any entry related to your input.");
            } else
            {
                string attr = node.Attributes[searchAttributeValue]?.InnerText;
                return attr;
            }
        }
        #endregion
    }
}
