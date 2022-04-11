using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class Medicamento : EntidadeBase
    {
        public int vezesQueFoiPego;
        public Medicamento(string nome, string descrição, int quantidade)
        {
            Nome = nome;
            Quantidade = quantidade;
            Descrição = descrição;
        }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public int Quantidade { get; set; }
        

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descrição: " + Descrição + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
                
        }
    }
}
