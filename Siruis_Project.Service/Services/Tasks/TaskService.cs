using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Dtos.TaskDto;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<TaskUpdateReq> AddTask(TaskAddReq task)
        {
            try
            {
              
                if (task == null)
                    throw new ArgumentNullException(nameof(task), "Task data is null.");
                var checkmember = await _unitOfWork.Repository<TeamMember>().GetAsync(task.TeamMember_Id);
                if (checkmember == null) throw new ArgumentNullException(nameof(task), "Team member is not exist.");
                var newTask = new TaskMember
                {
                    TaskDesc = task.TaskDesc,
                    TaskType = task.TaskType,
                    TeamMember_Id = task.TeamMember_Id,
                    DateEnd = task.DateEnd,
                };

                await _unitOfWork.Repository<TaskMember>().AddAsync(newTask);
                await _unitOfWork.CompleteAsync();

                return new TaskUpdateReq
                {
                    Id = newTask.Id,
                    TaskDesc = task.TaskDesc,
                    TaskType = task.TaskType,
                    TeamMember_Id = task.TeamMember_Id,
                    DateEnd = task.DateEnd,
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a Task.");
                throw new InvalidOperationException("An error occurred while adding the Task.", ex);
            }
        }

       

        public async Task<bool> DeleteAllTasks()
        {
            try
            {
                var tasks = await _unitOfWork.Repository<TaskMember>().GetAllAsync();
                if (tasks == null || !tasks.Any())
                    return false;

                _unitOfWork.Repository<TaskMember>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all Tasks.");
                return false;
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                var tskRepository = _unitOfWork.Repository<TaskMember>();
                var client = await tskRepository.GetAsync(id);
                if (client == null)
                    return false;

                tskRepository.Delete(client);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting a Task by ID.");
                return false;
            }
        }

        public async Task<IEnumerable<TaskUpdateReq>> GetAllTasks()
        {
            try
            {
                var taskRepository = _unitOfWork.Repository<TaskMember>();
                var tasks = await taskRepository.GetAllAsync();
                if (tasks == null || !tasks.Any())
                    return Enumerable.Empty<TaskUpdateReq>();

                var response = tasks.Select(task => new TaskUpdateReq
                {
                    Id = task.Id,
                    TaskDesc = task.TaskDesc,
                    TaskType = task.TaskType,
                    TeamMember_Id = task.TeamMember_Id,
                    DateEnd = task.DateEnd,
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all Tasks.");
                throw new InvalidOperationException("An error occurred while retrieving Tasks.", ex);
            }
        }

        public async Task<TaskUpdateReq> GetTaskById(int id)
        {
            try
            {
                var task = await _unitOfWork.Repository<TaskMember>().GetAsync(id);
                if (task is null) return null;
                var response= new TaskUpdateReq
                {
                    Id = task.Id,
                    TaskDesc = task.TaskDesc,
                    TaskType = task.TaskType,
                    TeamMember_Id = task.TeamMember_Id,
                    DateEnd = task.DateEnd,
                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a Task by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the Task with ID {id}.", ex);
            }
        }

        public async Task<TaskUpdateReq> UpdateTask(TaskUpdateReq  taskMember)
        {
            try
            {
                if (taskMember == null)
                    throw new ArgumentNullException(nameof(taskMember), "Task update data is null.");

                var existingtask = await _unitOfWork.Repository<TaskMember>().GetAsync(taskMember.Id);
                if (existingtask == null)
                    return null;
                var checkmember = await _unitOfWork.Repository<TeamMember>().GetAsync(taskMember.TeamMember_Id);
                if (checkmember == null) return null;



                existingtask.TaskDesc = taskMember.TaskDesc;
                existingtask.TaskType = taskMember.TaskType;
                existingtask.TeamMember_Id = taskMember.TeamMember_Id;
                existingtask.DateEnd = taskMember.DateEnd;

                await _unitOfWork.Repository<TaskMember>().Update(existingtask);
                await _unitOfWork.CompleteAsync();

                return taskMember;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a client.");
                throw new InvalidOperationException($"An error occurred while updating the client with ID {taskMember.Id}.", ex);
            }
        }




    }
}
