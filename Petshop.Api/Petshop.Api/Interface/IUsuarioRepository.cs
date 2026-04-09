using Petshop.Api.Entidades;

namespace Petshop.Api.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterUsuario(string email, string senha);
    }
}
