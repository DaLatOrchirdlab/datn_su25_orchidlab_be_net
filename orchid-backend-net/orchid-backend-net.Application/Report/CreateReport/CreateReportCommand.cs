using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Report.CreateReport
{
    public class CreateReportCommand : IRequest<string>, ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technician { get; set; }
        public string Sample {  get; set; }
        public CreateReportCommand() { }
        public CreateReportCommand(string name, string description, string technician, string sample)
        {
            Name = name;
            Description = description;
            Technician = technician;
            Sample = sample;
        }
    }
}
