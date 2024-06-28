using MEB_ARHUD_Calibration.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MEB_ARHUD_Calibration.Common
{
    class XMLUtil
    {
        public static User[] GetAllUsers()
        {
            return GetAllUsers(@"Config\UserConfig.xml");
        }

        private static User[] GetAllUsers(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(fileName);
            }
            catch (Exception)
            {
                return new User[0];
            }

            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("user");

            List<User> userslist = new List<User>();

            foreach (XmlNode node in nodes)
            {
                XmlElement oneEle = node as XmlElement;

                string name = oneEle.GetAttribute("name");
                string passwordString = oneEle.GetAttribute("password");
                string typeString = oneEle.GetAttribute("type");
                int typeInt = 0;
                int.TryParse(typeString, out typeInt);

                User user = new User(name, "", passwordString, (UserType)typeInt);
                userslist.Add(user);
            }

            return userslist.ToArray();
        }

        public static void ResetAllUser(User[] users)
        {
            ResetAllUser(@"Config\UserConfig.xml", users);
        }

        private static void ResetAllUser(string fileName, User[] users)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("users");
            XmlElement oneEle = nodes[0] as XmlElement;
            oneEle.RemoveAll();

            foreach (User user in users)
            {
                XmlElement newNode = xmldoc.CreateElement("user");
                newNode.SetAttribute("name", user.Name);
                newNode.SetAttribute("password", user.PasswordString);
                newNode.SetAttribute("type", ((int)user.Type) + "");
                oneEle.AppendChild(newNode);
            }

            xmldoc.Save(fileName);
        }

        public static int[] GetCameraCenterFromProjectConfigXml(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(fileName);

            XmlElement root = xmldoc.DocumentElement;

            int centerX = 0;
            int centerY = 0;
            int offsetX = 0;
            int offsetY = 0;
            int.TryParse(GetAttr(root, "Center", "CC_X"), out centerX);
            int.TryParse(GetAttr(root, "Center", "CC_Y"), out centerY);
            int.TryParse(GetAttr(root, "Offset", "X"), out offsetX);
            int.TryParse(GetAttr(root, "Offset", "Y"), out offsetY);
            return new int[] { centerX, centerY, offsetX, offsetY };
        }

        public static void SetCameraCenterFromProjectConfigXml(string fileName, int X, int Y)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(fileName);

                XmlElement root = xmldoc.DocumentElement;

                XmlNodeList nodes = root.GetElementsByTagName("Center");
                foreach (XmlNode node in nodes)
                {
                    XmlElement oneEle = node as XmlElement;
                    oneEle.SetAttribute("CC_X", X + "");
                    oneEle.SetAttribute("CC_Y", Y + "");
                    xmldoc.Save(fileName);
                    return;
                }
            }
            catch (Exception)
            {
            }
        }


        public static void UpdateNextCarInfoToSystemConfigXml(string fileName, ProjectType type, string vin)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(fileName);

                XmlElement root = xmldoc.DocumentElement;
                XmlNodeList nodes = root.GetElementsByTagName("NextCar");

                foreach (XmlNode node in nodes)
                {
                    XmlElement oneEle = node as XmlElement;

                    oneEle.SetAttribute("type", type + "");
                    oneEle.SetAttribute("vin", vin + "");
                    break;
                }
                xmldoc.Save(fileName);

            }
            catch (Exception)
            {
                return;
            }
        }

        public static void InitConfigsFromSystemConfigXml(string fileName)
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(fileName);
            XmlElement root = xmldoc.DocumentElement;
            
            string CarType = GetAttr(root, "NextCar", "type");
            Config.ProjectType = (ProjectType)Enum.Parse(typeof(ProjectType), CarType);

            Config.NeedTest_ID3 = GetBoolAttr(root, "NeedTest", "ID3");
            Config.NeedTest_ID4X = GetBoolAttr(root, "NeedTest", "ID4X");
            Config.NeedTest_ID6X = GetBoolAttr(root, "NeedTest", "ID6X");
            Config.NeedTest_AUDI = GetBoolAttr(root, "NeedTest", "AUDI");

            Config.NeedTest_ID3N = GetBoolAttr(root, "NeedTest", "ID3N");
            Config.NeedTest_ID4XN = GetBoolAttr(root, "NeedTest", "ID4XN");
            Config.NeedTest_ID6XN = GetBoolAttr(root, "NeedTest", "ID6XN");
            Config.NeedTest_AUDIN = GetBoolAttr(root, "NeedTest", "AUDIN");
        }


        public static string GetAttr(XmlElement root, string elementName, string attrName)
        {
            XmlElement? element = root.GetElementsByTagName(elementName).OfType<XmlElement>().FirstOrDefault();
            return element?.GetAttribute(attrName) ?? string.Empty;
        }


        public static bool GetBoolAttr(XmlElement root, string elementName, string attrName)
        {
            string attr = GetAttr(root, elementName, attrName);
            if (bool.TryParse(attr, out bool value))
                return value;
            return false;
        }

        public static void UpdateAttrToXml(string fileName, string elementName, string attrName, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(fileName);

                XmlElement root = xmldoc.DocumentElement;

                XmlNodeList nodes = root.GetElementsByTagName(elementName);
                foreach (XmlNode node in nodes)
                {
                    XmlElement oneEle = node as XmlElement;
                    oneEle.SetAttribute(attrName, value);
                    xmldoc.Save(fileName);
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
