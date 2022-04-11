using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class TelaCadastroFuncionario : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioFuncionario _repositorioFuncionario;
        private readonly Notificador _notificador;

        public TelaCadastroFuncionario(RepositorioFuncionario repositorioFuncionario, Notificador notificador)
            : base("Cadastro de Funcionários")
        {
            _repositorioFuncionario = repositorioFuncionario;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Funcionário");

            Funcionario novoFuncionrio = ObterFuncionario();

            _repositorioFuncionario.Inserir(novoFuncionrio);

            _notificador.ApresentarMensagem("Funcionário cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Funcionario");

            bool temFuncionariosCadastrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum funcionário cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFuncionario = ObterNumeroRegistro();

            Funcionario funcionarioAtualizado = ObterFuncionario();

            bool conseguiuEditar = _repositorioFuncionario.Editar(numeroFuncionario, funcionarioAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Funcionário editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Funcionário");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum funcionário cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFuncionario = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFuncionario.Excluir(numeroFuncionario);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Funcionário excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Funcionários");

            List<Funcionario> funcionarios = _repositorioFuncionario.SelecionarTodos();

            if (funcionarios.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum funcionário disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Funcionario funcionario in funcionarios)
                Console.WriteLine(funcionario.ToString());

            Console.ReadLine();

            return true;
        }

        private Funcionario ObterFuncionario()
        {
            Console.WriteLine("Digite o nome do funcionário: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o login do funcionário: ");
            string login = Console.ReadLine();

            Console.WriteLine("Digite a senha do funcionário: ");
            string senha = Console.ReadLine();

            return new Funcionario(nome, login, senha);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Funcionário que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFuncionario.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Funcionário não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
