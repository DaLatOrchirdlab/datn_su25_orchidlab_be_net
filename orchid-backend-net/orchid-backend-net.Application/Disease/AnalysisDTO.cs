namespace orchid_backend_net.Application.Disease
{
    public class AnalysisDTO(string stage, DiseaseDTO disease)
    {
        public string Stage { get; set; } = stage;
        public DiseaseDTO Disease { get; set; } = disease;
    }
}
