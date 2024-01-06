
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
    {
        var groupedDependencies = _dal.Dependency.ReadAll()
        .OrderBy(dep => dep?.DependsOnTask)
        .GroupBy(dep => dep.DependentTask, dep => dep.DependsOnTask,
        (id, dependency) => new { TaskId = id, Dependencies = dependency })
        .ToList();

        var distinctDependencies = groupedDependencies
       //.Select(depGroup => new { TaskId = depGroup.TaskId, Dependencies = depGroup.Dependencies })
       .GroupBy(dep => dep.Dependencies,
        (key, group) => group.First())
       .ToList();
        //var distinctDependencies2 = new List<dynamic>();

        //foreach (var depGroup in groupedDependencies)
        //{
        //    var existingDep = distinctDependencies.FirstOrDefault(d => Enumerable.SequenceEqual(d.Dependencies, depGroup.Dependencies));

        //    if (existingDep == null)
        //    {
        //        distinctDependencies.Add(depGroup);
        //    }
        //}

        //var groupedDependencies =
        //from dependency in _dal.Dependency.ReadAll()
        //group dependency by (dependency.DependentTask,dependency.DependsOnTask) into (DependentTask, DependsOnTask)=> newGroup
        //orderby newGroup.DependentTask
        //select new { Key = newGroup.Key, List = new List<TaskInList> };

        //var distinctDependencies = groupedDependencies
        //   .SelectMany(depGroup => depGroup.Dependencies)
        //   .Where(dep => dep != null)
        //   .Distinct()
        //   .ToList();
        //

        //    var distinctDependencies = groupedDependencies
        //.SelectMany(depGroup => depGroup.Dependencies.
        //Select(dep => new { TaskId = depGroup.TaskId, DependencyId = dep }))
        //.Where(dep => dep.DependencyId != null)
        //.Distinct()
        //.ToList();
        int id = 0;
        var mileStone =
        from dependencies in distinctDependencies
        select new BO.Task()
        {
            Id = id++,
            Alias = "M" + id,
            Description = null,
            CreatedAtDate = DateTime.Today,
            Status = Status.Scheduled,//נדרש חישוב
            Dependencies = (from dep in dependencies.Dependencies
                           let task = _dal.Task.Read(dep)
                           select new BO.TaskInList()
                           {
                               Id = task.Id,
                               Description= task.Description,
                               Alias = task.Alias,
                               Status = null,
                           }).ToList(),

        StartDate = null,//נדרש חישוב
            ForecastDate = null,//נדרש חישוב
            CompleteDate = null,//נדרש חישוב,
            DeadlineDate = null,//נדרש חישוב
            Remarks = null,//נדרש חישוב
            Milestone = null,//נדרש חישוב
            RequiredEffortTime= null,//נדרש חישוב
            Deliverables= null,//נדרש חישוב
            Engineer= null, 
            Copmlexity= null,
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
