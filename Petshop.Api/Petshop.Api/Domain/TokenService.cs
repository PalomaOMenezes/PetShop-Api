using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Petshop.Api.DTO;
using Petshop.Api.Entidades;
using Petshop.Api.Interface;

namespace Petshop.Api.Domain
{
    public class TokenService : ITokenService
    {
        //Aqui você está declarando uma variável privada, serve como um "espaço reservado" para guardar a ferramenta que acessa o banco de dados.
        private IUsuarioRepository _usuarioRepository;

        public TokenService(IUsuarioRepository repository)
        {
            //Aqui você pega a ferramenta que recebeu (parâmetro repository) e a guarda no "bolso" da classe
            //(_clienteRepository) para usar depois em outros métodos.
            _usuarioRepository = repository;
        }


        public async Task<object?> EfetuarLogin(string email, string senha)
        {
            try
            {
                var resultado = await _usuarioRepository.ObterUsuario(email, senha);

                if (resultado == null)
                {
                    return null;
                }

                var token = await CriarTokenJwt(resultado);


                return token;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<string> CriarTokenJwt(Usuario usuario)
        {
            // 1. Preparar a Chave (Key)
            // Transformamos o texto da nossa senha em um array de bytes, que é o formato que o .NET exige.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UGFsb21hIGRlIE9saXZlaXJhIE1lbmV6ZXM"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Criar as Claims (As informações/bagagem do Token)
            // Aqui é onde colocamos os dados que queremos recuperar depois sem precisar ir ao banco de dados.
            var claims = new[] {
                new Claim("container", usuario.Id.ToString()),
                new Claim("booking", usuario.Email),
                };

            // 4. Montar o Objeto do Token
            var token = new JwtSecurityToken(
                issuer: "yourApp",
                audience: "yourApi",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
