using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleMedicamentos.ConsoleApp.Modulorequisicao
{
    internal class Requisicao:EntidadeBase
    {
        Paciente paciente;
        Medicamento medicamento;
        bool aprovada;
        DateTime data;

        public Requisicao(Paciente paciente, Medicamento medicamento, bool aprovada, DateTime data)
        {
            this.paciente = paciente;
            this.medicamento = medicamento;
            this.aprovada = aprovada;
             this.data = data;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Aprovado"+aprovada + Environment.NewLine +
                "Medicamento: " + medicamento.Nome + Environment.NewLine +
                "Paciente: " + paciente.Nome + Environment.NewLine +
                "Data: " + data + Environment.NewLine;

        }
    }
}
