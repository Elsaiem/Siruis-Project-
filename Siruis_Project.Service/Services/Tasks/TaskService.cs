using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core;
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


        public async Task<TaskMember> AddTask(TaskMember task)
        {
            if (task == null) return null;


            await _unitOfWork.Repository<TaskMember>().AddAsync(task);
            await _unitOfWork.CompleteAsync();
            return task;
        }

       

        public async Task DeleteAllTasks()
        {
            _unitOfWork.Repository<TaskMember>().DeleteAll();
            await _unitOfWork.CompleteAsync(); // Save the changes to the database
        }

        public async Task DeleteTask(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var TaskRepository = _unitOfWork.Repository<TaskMember>();
            if (TaskRepository == null)
            {
                throw new InvalidOperationException("TeamMember repository is not initialized.");
            }

            var result = await TaskRepository.GetAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException($"Team member with id {id} does not exist.");
            }

            TaskRepository.Delete(result);
            await _unitOfWork.CompleteAsync(); // Save the changes
        }

        public async Task<IEnumerable<TaskMember>> GetAllTasks()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var TaskMemberRepository = _unitOfWork.Repository<TaskMember>();
            if (TaskMemberRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await TaskMemberRepository.GetAllAsync();
            return result ?? Enumerable.Empty<TaskMember>();
        }

        public async Task<TaskMember> GetTaskById(int id)
        {
            var task = await _unitOfWork.Repository<TaskMember>().GetAsync(id);
            if (task == null) return null;
            return task;
        }

        public async Task<TaskMember> UpdateTask(TaskMember  taskMember)
        {
            var check = await _unitOfWork.Repository<TaskMember>().GetAsync(taskMember.Id);
            if (check is null) return null;

            // Update properties explicitly
            check.TaskDesc = taskMember.TaskDesc;
            check.TaskType = taskMember.TaskType;
            check.DateEnd = taskMember.DateEnd;

            await _unitOfWork.Repository<TaskMember>().Update(check);
            await _unitOfWork.CompleteAsync();
            return check; // Return the updated entity
        }




    }
}
