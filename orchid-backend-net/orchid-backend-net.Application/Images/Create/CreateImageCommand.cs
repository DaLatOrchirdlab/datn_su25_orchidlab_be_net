using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Images.Create
{
    public class CreateImageCommand(Stream fileStream, string fileName) : IRequest<string>, ICommand
    {
        public Stream FileStream { get; set; } = fileStream;
        public string FileName { get; set; } = fileName;
    }

    internal class CreateImageCommandHandler(IImageUploaderService uploaderService, IImageRepository imageRepository, IReportRepository reportRepository) : IRequestHandler<CreateImageCommand, string>
    {
        public async Task<string> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var url = await uploaderService.UpdloadImageAsync(request.FileStream, request.FileName);
            var reports = await reportRepository.FindAllAsync(cancellationToken);
            Imgs imgs = new()
            {
                ReportID = reports[0].ID,
                Url = url,
                Status = true,
            };
            imageRepository.Add(imgs);
            return await imageRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? $"Created Images with Url: {url}" : "Failed to create Images.";
        }
    }
}
