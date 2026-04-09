namespace Petshop.Api.Models
{
    public class ResumoServico
    {
        public int Id { get; set; }
        public string ClienteNome {  get; set; }
        public string PetNome { get; set; }
        public string PetDescricaoBreve { get; set; }
        public string TipoServico { get; set; }
    }
}
