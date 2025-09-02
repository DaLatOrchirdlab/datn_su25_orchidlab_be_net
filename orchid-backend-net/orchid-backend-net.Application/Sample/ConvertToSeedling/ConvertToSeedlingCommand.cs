using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Sample.ConvertToSeedling
{
    public class ConvertToSeedlingCommand : IRequest<string>, ICommand
    {
        public string SampleID {  get; set; }
        public ConvertToSeedlingCommand(string sampleID)
        {
            this.SampleID = sampleID;
        }

    }

    public class ConvertToSeedlingCommandHandler(
        ISeedlingRepository seedlingRepository, 
        ISampleRepository sampleRepository,
        IExperimentLogRepository experimentLogRepository
        ) : IRequestHandler<ConvertToSeedlingCommand, string>
    {
        public async Task<string> Handle(ConvertToSeedlingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sample = await sampleRepository.FindAsync(x => x.ID.Equals(request.SampleID), cancellationToken);
                //if (!(await sampleRepository.AnyAsync(x => x.Linkeds.Any(t => t.ProcessStatus == 2), cancellationToken)))
                //    throw new Exception("Sample is not ready to be an Seedling, finish the Experiment first.");
                //else if (sample.Status != 0)
                //    throw new Exception("Sample was destroyed or suspended");
                var ExperimentLog = await experimentLogRepository.FindAsync(x => x.Linkeds.Any(x => x.SampleID.Equals(sample.ID)), cancellationToken);
                List<Hybridizations> listParent = ExperimentLog.Hybridizations.ToList();
                var seedling = new Seedlings()
                {
                    Dob = sample.Dob,
                    Parent1 = listParent[0].ParentID,
                    //Parent2 = listParent[1].ParentID,
                    LocalName = sample.Name,
                    ScientificName = sample.Name,
                    Description = sample.Description,
                };
                seedlingRepository.Add(seedling);
                sample.Status = 3;
                sampleRepository.Update(sample);
                return (await seedlingRepository.UnitOfWork.SaveChangesAsync(cancellationToken)) > 0 ? "Success" : "Failed";
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
