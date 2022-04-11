using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente:EntidadeBase
    {
        public Paciente(string nome, string bairro, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
            Bairro = bairro;
        }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Bairro { get; set; }


        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "cpf: " + Cpf + Environment.NewLine +
                "Bairro: " + Bairro + Environment.NewLine;

        }
    }
}
