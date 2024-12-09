using Microsoft.AspNetCore.Mvc;
using Siruis_Project.Core;
using Siruis_Project.Core.Dtos.ClientDto;
using Siruis_Project.Core.Dtos.TeamMemberDto;
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

        public async Task<TeamMemberUpdateReq> AddMember(TeamMemberAddReq member)
        {
            try
            {
                if (member == null)
                    throw new ArgumentNullException(nameof(member), "Team Member data is null.");

                var newMember = new TeamMember
                {
                    TeamName = member.TeamName,
                    JobTitle = member.JobTitle,
                };

                await _unitOfWork.Repository<TeamMember>().AddAsync(newMember);
                await _unitOfWork.CompleteAsync();

                return new TeamMemberUpdateReq
                {
                    Id=newMember.Id,
                    TeamName = member.TeamName,
                    JobTitle = member.JobTitle,
                };

            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while adding a client.");
                throw new InvalidOperationException("An error occurred while adding the client.", ex);
            }
        }

        public async Task<bool> DeleteAllTeamMembers()
        {
            try
            {
                var members = await _unitOfWork.Repository<TeamMember>().GetAllAsync();
                if (members == null || !members.Any())
                    return false;

                _unitOfWork.Repository<TeamMember>().DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting all Members.");
                return false;
            }
        }


        public async Task<bool> DeleteMember(int id)
        {
            try
            {
                var memberRepository = _unitOfWork.Repository<TeamMember>();
                var client = await memberRepository.GetAsync(id);
                if (client == null)
                    return false;

                memberRepository.Delete(client);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while deleting a Team Member by ID.");
                return false;
            }
        }



        public async Task<IEnumerable<TeamMemberUpdateReq>> GetAllMembers()
        {
            try
            {
                var memberRepository = _unitOfWork.Repository<TeamMember>();
                var members = await memberRepository.GetAllAsync();
                if (members == null || !members.Any())
                    return Enumerable.Empty<TeamMemberUpdateReq>();

                var response = members.Select(member => new TeamMemberUpdateReq
                {
                    Id = member.Id,
                    TeamName = member.TeamName,
                    JobTitle = member.JobTitle,
                });

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving all Team members.");
                throw new InvalidOperationException("An error occurred while retrieving Team members.", ex);
            }
        }


        public async Task<TeamMemberUpdateReq> GetMemberById(int id)
        {
            try
            {
                var member = await _unitOfWork.Repository<TeamMember>().GetAsync(id);
                if (member is null) return null;
                var response = new TeamMemberUpdateReq
                {
                   Id=member.Id,
                   TeamName=member.TeamName,
                   JobTitle=member.JobTitle,

                };

                return response ?? null;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while retrieving a Member by ID.");
                throw new InvalidOperationException($"An error occurred while retrieving the Team memeber with ID {id}.", ex);
            }
        }

        public async Task<TeamMemberUpdateReq> UpdateMember(TeamMemberUpdateReq teamMember)
        {
            try
            {
                if (teamMember == null)
                    throw new ArgumentNullException(nameof(teamMember), "member update data is null.");

                var existingMember = await _unitOfWork.Repository<TeamMember>().GetAsync(teamMember.Id);
                if (existingMember == null)
                    return null;

                existingMember.TeamName = teamMember.TeamName;
                existingMember.JobTitle = teamMember.JobTitle;

                await _unitOfWork.Repository<TeamMember>().Update(existingMember);
                await _unitOfWork.CompleteAsync();

                return teamMember;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Logger.LogError(ex, "Error occurred while updating a Team Member.");
                throw new InvalidOperationException($"An error occurred while updating the Team Memeber with ID {teamMember.Id}.", ex);
            }
        }
    }
}
