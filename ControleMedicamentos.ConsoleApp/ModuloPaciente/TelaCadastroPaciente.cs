using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaCadastroPaciente:TelaBase,ITelaCadastravel
    {
        RepositorioPaciente repositorio;
        Notificador notificador;

        public TelaCadastroPaciente(RepositorioPaciente repositorioPaciente, Notificador Notificador)
             : base("Cadastro de Paciente")
        {
            repositorio = repositorioPaciente;
            notificador = Notificador;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Paciente novoPaciente = ObterPaciente();

            repositorio.Inserir(novoPaciente);

            notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Paciente");

            bool temPacienteCadastrados = VisualizarRegistros("Pesquisando");

            if (temPacienteCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Paciente cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            Paciente PacienteAtualizado = ObterPaciente();

            bool conseguiuEditar = repositorio.Editar(numeroPaciente, PacienteAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Fornecedor editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temPacienteRegistrados = VisualizarRegistros("Pesquisando");

            if (temPacienteRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Paciente cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorio.Excluir(numeroPaciente);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Paciente excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de medicamento");

            List<Paciente> registros = repositorio.SelecionarTodos();

            if (registros.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum Paciente disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Paciente fornecedor in registros)
                Console.WriteLine(fornecedor.ToString());

            Console.ReadLine();

            return true;
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do Paciente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o cpf do paciente: ");
            string cpf = Console.ReadLine();

            Console.WriteLine("Digite o bairro do paciente ");
            string bairro= Console.ReadLine();





            return new Paciente(nome, cpf, bairro);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do fornecedor que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorio.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
