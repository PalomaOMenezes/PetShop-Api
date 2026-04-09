namespace Petshop.Api.Entidades
{
    public class Pet
    {
        public int Id {  get; set; }
        public string NomePet { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string TipoPet { get; set; } = string.Empty;
        public string DescricaoBreve { get; set; } = string.Empty;
    }
}
