using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
namespace ControleMedicamentos.ConsoleApp.Modulorequisicao
{
    internal class TelaRequisicao : TelaBase,ITelaCadastravel

    {
        RepositorioRequisicao repositorio;
        Notificador notificador;
        RepositorioPaciente RepositorioPaciente;
        RepositorioMedicamento RepositorioMedicamento;

        public TelaRequisicao(RepositorioRequisicao repositorioRequisicao, Notificador Notificador,RepositorioMedicamento repositorioMedicamento,RepositorioPaciente repositorioPaciente)
             : base("Cadastro de Paciente")
        {
            repositorio = repositorioRequisicao;
            notificador = Notificador;
            RepositorioPaciente= repositorioPaciente;
            RepositorioMedicamento= repositorioMedicamento;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Requisicao novoRequisicao = ObterRequisicao();

            repositorio.Inserir(novoRequisicao);

            notificador.ApresentarMensagem("Requisicao cadastrado com sucesso!", TipoMensagem.Sucesso);
         }

        public void Editar()
        {
            MostrarTitulo("Editando Requisicao");

            bool temRequisicao = VisualizarRegistros("Pesquisando");

            if (temRequisicao == false)
            {
                notificador.ApresentarMensagem("Nenhuma Requisicao cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Requisicao RequisicaoAtualizado = ObterRequisicao();

            bool conseguiuEditar = repositorio.Editar(numeroRequisicao, RequisicaoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Requisicao editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisicao");

            bool temRequisicaoRegistrados = VisualizarRegistros("Pesquisando");

            if (temRequisicaoRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Requisicao cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorio.Excluir(numeroRequisicao);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Requisicao excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisicao");

            List<Requisicao> registros = repositorio.SelecionarTodos();

            if (registros.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum Requisicao disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao fornecedor in registros)
                Console.WriteLine(fornecedor.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao()
        {
            Console.WriteLine("Digite o id do Paciente: ");
            int id = Convert.ToInt32(Console.ReadLine());
           Paciente paciente= RepositorioPaciente.SelecionarRegistro(id);
            Console.WriteLine("Digite o id do medicamento:");
            int numero = Convert.ToInt32(Console.ReadLine());
            Medicamento medicamento = RepositorioMedicamento.SelecionarRegistro(numero);
            if (medicamento.Quantidade == 0)
            {
                Console.WriteLine("Estoque de medicamento se encontra vazio");
                return null;
            }
            int total = 0;
            DateTime data = DateTime.Now;
            bool aprovada = true;
            total = medicamento.Quantidade;
            medicamento.Quantidade = total - 1;
            medicamento.vezesQueFoiPego++;



            return new Requisicao(paciente,  medicamento,  aprovada,  data);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Requisicao que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorio.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Requisicaonão foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;

        }
    }
}
