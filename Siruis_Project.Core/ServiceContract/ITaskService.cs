using Siruis_Project.Core.Dtos.TaskDto;
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

        Task<IEnumerable<TaskUpdateReq>> GetAllTasks();

        Task<TaskUpdateReq> GetTaskById(int id);
        Task<TaskUpdateReq> AddTask(TaskAddReq task);

        Task<bool> DeleteTask(int id);

        Task<TaskUpdateReq> UpdateTask(TaskUpdateReq task);

       

         Task<bool> DeleteAllTasks();
    }
}
