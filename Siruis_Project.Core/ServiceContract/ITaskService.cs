using Siruis_Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Core.ServiceContract
{
    public interface ITaskService
    {

        Task<IEnumerable<TaskMember>> GetAllTasks();

        Task<TaskMember> GetTaskById(int id);
        Task<TaskMember> AddTask(TaskMember task);




        Task DeleteTask(int id);

        Task<TaskMember> UpdateTask(TaskMember task);

       

         Task DeleteAllTasks();
    }
}
