namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency      
{
    const string filePath = @"dependencies";

    public int Create(Dependency item)
    {
        int id = Config.NextDependencyId;
        XElement dependenciesElement = XMLTools.LoadListFromXMLElement(filePath);

        XElement newDependencyElement = new XElement("Dependency",
             new XElement("Id", id),
             new XElement("DependentTask", item.DependentTask),
             new XElement("DependsOnTask", item.DependsOnTask)
         );

        dependenciesElement.Add(newDependencyElement);
        XMLTools.SaveListToXMLElement(dependenciesElement, filePath);
        return id;
    }

    public void Delete(int id)
    {
        if (Read(id) is not null)
        {
            XElement dependenciesElement = XMLTools.LoadListFromXMLElement(filePath);
            var deleteDependencyElement = dependenciesElement.Elements("Dependency").FirstOrDefault(d => (int)d.Element("Id") == id);
            if (deleteDependencyElement != null)
            {
                deleteDependencyElement.Remove();
                XMLTools.SaveListToXMLElement(dependenciesElement, filePath);
            }
            else
            {
                throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
            }
        }
    }
    public Dependency? Read(int id)
    {
        XElement dependenciesElement = XMLTools.LoadListFromXMLElement(filePath);
        var readDependencyElement = dependenciesElement.Elements("Dependency").FirstOrDefault(d => (int)d.Element("Id") == id);
        
        return readDependencyElement;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
