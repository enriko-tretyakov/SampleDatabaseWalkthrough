using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SampleDatabaseWalkthrough.Specs.Xml
{
    public class XmlUtility
    {
        public string SerchChildElement(string searchName,string searchValue)
        {
            int num_el = 0;
            string str = "";
            string strValue = "";
            XPathNavigator nav;
            string returnValue = "";

            XDocument xmlDoc = XDocument.Load("D:\\VS2022_Projects\\SampleDatabaseWalkthrough\\SampleDatabaseWalkthrough.Specs\\Xml\\Balance_01082022.xml");

            IEnumerable<XElement> childList =
                from el in xmlDoc.Elements()
                select el;

            // Create a navigator to query with XPath.
            nav = xmlDoc.CreateNavigator();
            //Initial XPathNavigator to start at the root.
            // nav.MoveToRoot();
            nav.MoveToFollowing(XPathNodeType.Element);
            //Move to the first child node (comment field).
            nav.MoveToFirstChild();
            do
            {
                //Find the first element.
                if (nav.NodeType == XPathNodeType.Element)
                {
                    //Determine whether children exist.
                    if (nav.HasChildren == true)
                    {
                        //Move to the first child.
                         nav.MoveToFirstChild();
                        //Loop through all of the children.
                        do
                        {
                            num_el = childList.Count();
                            foreach (XElement el in childList)
                            {
                                if(el.Name != null)
                                {
                                    str += el.Name;
                                    strValue +=el.Value; 
                                }
                            }
                            //Display the data.

                            if (nav.Name == searchName)
                            {
                                string result = nav.Value;
                                returnValue = nav.Value;
                            }

                            //Check for attributes.
                            if (nav.HasAttributes == true)
                            {
                                Console.WriteLine("This node has attributes");
                            }
                        } while (nav.MoveToNext());
                    }
                }
            } while (nav.MoveToNext());

            return returnValue;
        }

    }
}
