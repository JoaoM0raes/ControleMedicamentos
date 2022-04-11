using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class TelaMedicamento : TelaBase, ITelaCadastravel
    {

        RepositorioMedicamento repositorio;
        Notificador notificador;

        public TelaMedicamento(RepositorioMedicamento repositorioMedicamento, Notificador Notificador)
             : base("Cadastro de Medicamento")
        {
            repositorio = repositorioMedicamento;
            notificador = Notificador;
        }
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para vizualizar Medicamentos em falta");
            Console.WriteLine("Digite 6 para vizualizar Medicamentos mais requisitados");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Medicamento novoFornecedor = ObterFornecedor();

            repositorio.Inserir(novoFornecedor);

            notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temFornecedoresCadastrados = VisualizarRegistros("Pesquisando");

            if (temFornecedoresCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFornecedor = ObterNumeroRegistro();

            Medicamento fornecedorAtualizado = ObterFornecedor();

            bool conseguiuEditar = repositorio.Editar(numeroFornecedor, fornecedorAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Fornecedor editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temFornecedoresRegistrados = VisualizarRegistros("Pesquisando");

            if (temFornecedoresRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFornecedor = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorio.Excluir(numeroFornecedor);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("medicamento excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de medicamento");

            List<Medicamento> registros = repositorio.SelecionarTodos();



            if (registros.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Medicamento fornecedor in registros)
                Console.WriteLine(fornecedor.ToString());

            Console.ReadLine();

            return true;
        }

        private Medicamento ObterFornecedor()
        {
            Console.WriteLine("Digite o nome do Medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do Medicamento: ");
            string descrição = Console.ReadLine();

            Console.WriteLine("Digite a quantidade do medicamento: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());





            return new Medicamento(nome, descrição, quantidade);
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
                    notificador.ApresentarMensagem("ID do fornecedor não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        public void VisualizarMedicamentoEmFalta()
        {
            List<Medicamento> registros = repositorio.SelecionarTodos();    
            
           for (int i = 0; i < registros.Count; i++)
            {
                if (registros[i].Quantidade == 0)
                {
                    Console.WriteLine(registros[i]);
                }
            }
            Console.ReadLine();
        }
        public void MostrarMedicamentosMaisSolicitados()
        {
            List<Medicamento> registros = repositorio.SelecionarTodos();
            
            
         List<Medicamento> medicamentoOrdenado=registros.OrderBy(x=>x.vezesQueFoiPego).ToList();
            for (int i = 0; i < medicamentoOrdenado.Count; i++)
            {
                Console.WriteLine(medicamentoOrdenado[i]);
            }
            Console.ReadLine ();    
        }
    }
}
