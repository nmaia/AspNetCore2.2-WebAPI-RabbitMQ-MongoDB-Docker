﻿using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ParentsReports
        : BaseReports<ParentsReport>
        , IParentsReports
    {
        private readonly IParentsReportsRepository _parentsRepository;

        public ParentsReports(IParentsReportsRepository parentsRepository)
            => _parentsRepository = parentsRepository;

        public ParentsReport MountParentsObjectToInsert(Research research)
        {
            var parents = new string[research.Person.Filiation.Length];
            var index = 0;

            foreach (var parent in research.Person.Filiation)
            {
                parents[index] = string.Join(" ", parent.FirstName, parent.LastName);
                index++;
            }

            var parentsObject = new ParentsReport()
            {
                Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                Parent = string.Join(", ", parents[0], parents[1])
            };

            return parentsObject;
        }

        public async Task<ParentsReport> GetParentsReport(string id)
        {
            return await _parentsRepository.GetParentsById(id);
        }
    }
}
