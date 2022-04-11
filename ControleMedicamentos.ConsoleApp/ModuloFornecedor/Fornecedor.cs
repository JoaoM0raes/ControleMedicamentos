using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase
    {
        public Fornecedor(string nome, string telefone, string email, string cidade, string estado)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Cidade = cidade;
            Estado = estado;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine +
                "Email: " + Email + Environment.NewLine +
                "Cidade: " + Cidade + Environment.NewLine +
                "Estado: " + Estado + Environment.NewLine;
        }

    }
}
