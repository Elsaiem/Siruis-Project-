using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities;
using Siruis_Project.Core.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siruis_Project.Service.Services.TeamMembers
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TeamMember> AddMember(TeamMember member)
        {
            if (member == null) return null;


            await _unitOfWork.Repository<TeamMember>().AddAsync(member);
            await _unitOfWork.CompleteAsync();
            return member;
        }

        public async Task DeleteAllTeamMembers()
        {
            _unitOfWork.Repository<TeamMember>().DeleteAll();
            await _unitOfWork.CompleteAsync(); // Save the changes to the database
        }


        public async Task DeleteMember(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var teamMemberRepository = _unitOfWork.Repository<TeamMember>();
            if (teamMemberRepository == null)
            {
                throw new InvalidOperationException("TeamMember repository is not initialized.");
            }

            var result = await teamMemberRepository.GetAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException($"Team member with id {id} does not exist.");
            }

            teamMemberRepository.Delete(result);
            await _unitOfWork.CompleteAsync(); // Save the changes
        }



        public async Task<IEnumerable<TeamMember>> GetAllMembers()
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var TeamMemberRepository = _unitOfWork.Repository<TeamMember>();
            if (TeamMemberRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await TeamMemberRepository.GetAllAsync();
             return result ?? Enumerable.Empty<TeamMember>();
        }

        public async Task<TeamMember> GetMemberById(int id)
        {
            if (_unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of Work is not initialized.");
            }

            var TeamMemberRepository = _unitOfWork.Repository<TeamMember>();
            if (TeamMemberRepository == null)
            {
                throw new InvalidOperationException("Client repository is not initialized.");
            }

            var result = await TeamMemberRepository.GetAsync(id);
            return result ?? null;
        }

        public async Task<TeamMember> UpdateMember(TeamMember  teamMember)
        {
            var check = await _unitOfWork.Repository<TeamMember>().GetAsync(teamMember.Id);
            if (check is null) return null;

            // Update properties explicitly
            check.TeamName = teamMember.TeamName;
            check.JobTitle = teamMember.JobTitle;

            await _unitOfWork.Repository<TeamMember>().Update(check);
            await _unitOfWork.CompleteAsync();
            return check; // Return the updated entity
        }
    }
}
