using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class TelaCadastroFornecedor : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioFornecedor _repositorioFornecedor;
        private readonly Notificador _notificador;

        public TelaCadastroFornecedor(RepositorioFornecedor repositorioFornecedor, Notificador notificador)
            : base("Cadastro de Fornecedores")
        {
            _repositorioFornecedor = repositorioFornecedor;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Fornecedor");

            Fornecedor novoFornecedor = ObterFornecedor();

            _repositorioFornecedor.Inserir(novoFornecedor);

            _notificador.ApresentarMensagem("Fornecedor cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Fornecedor");

            bool temFornecedoresCadastrados = VisualizarRegistros("Pesquisando");

            if (temFornecedoresCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum fornecedor cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFornecedor = ObterNumeroRegistro();

            Fornecedor fornecedorAtualizado = ObterFornecedor();

            bool conseguiuEditar = _repositorioFornecedor.Editar(numeroFornecedor, fornecedorAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Fornecedor editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Fornecedor");

            bool temFornecedoresRegistrados = VisualizarRegistros("Pesquisando");

            if (temFornecedoresRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum fornecedor cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFornecedor = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFornecedor.Excluir(numeroFornecedor);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Fornecedor excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Fornecedores");

            List<Fornecedor> fornecedores = _repositorioFornecedor.SelecionarTodos();

            if (fornecedores.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum fornecedor disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Fornecedor fornecedor in fornecedores)
                Console.WriteLine(fornecedor.ToString());

            Console.ReadLine();

            return true;
        }

        private Fornecedor ObterFornecedor()
        {
            Console.WriteLine("Digite o nome do fornecedor: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o telefone do fornecedor: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite o email do fornecedor: ");
            string email = Console.ReadLine();

            Console.WriteLine("Digite a cidade do fornecedor: ");
            string cidade = Console.ReadLine();

            Console.WriteLine("Digite o estado do fornecedor: ");
            string estado = Console.ReadLine();

            return new Fornecedor(nome, telefone, email, cidade, estado);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do fornecedor que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFornecedor.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do fornecedor não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
