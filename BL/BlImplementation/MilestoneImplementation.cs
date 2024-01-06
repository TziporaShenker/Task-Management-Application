
using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public List<BO.TaskInList>? Create()
    {//
     //    var groupedDependencies = _dal.Dependency.ReadAll()
     //.OrderBy(dep => dep?.DependsOnTask)
     //.GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
     //.ToList();
        var groupedDependencies =
        from dependency in _dal.Dependency.ReadAll()
        group dependency by dependency.DependentTask into newGroup
        orderby newGroup.Key
        select new { Key = newGroup.Key, List = new List<TaskInList> };

        var distinctDependencies = groupedDependencies
           .SelectMany(depGroup => depGroup.List)
           .Where(dep => dep != null)
           .Distinct()
           .ToList();


        int id = 0;
        var mileStone =
        from dependency in distinctDependencies
        let task = _dal.Task.Read(dependency.Key)
        select new BO.Milestone()
        {
            Id = id++,
            Alias = "M" + id,
            Description = task.Description,
            CreatedAtDate = task.CreatedAtDate,
            Status = Status.Scheduled,//נדרש חישוב
            StartDate = task.StartDate,
            ForecastDate = null,//נדרש חישוב
            CompleteDate = task.CompleteDate,
            DeadlineDate = task.DeadlineDate,
            CompletionPercentage = 0,
            Remarks = task.Remarks,
            Dependencies = (List<TaskInList>)dependency.List

        };


        //return (
        //        from DO.Dependency doDependency in _dal.Dependency.ReadAll()
        //        where (doDependency.DependentTask == taskId)
        //        let dependentTask = Read(doDependency.DependsOnTask)
        //        select new BO.TaskInList()
        //        {
        //            Id = doDependency.DependsOnTask,
        //            Description = dependentTask.Description,
        //            Alias = dependentTask.Alias,
        //            Status = null,
        //        }).ToList();
    }

    public Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }

}
