using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controle_De_Contatos.Controllers
{
    [PaginaParaUsuarioLogado]

    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
       
        private readonly ISessao _sessao;

        //O sistema cria uma instância da classe concreta (ContatoRepositorio) que implementa essa interface e a injeta (entrega) como um parâmetro neste construtor.
        public ContatoController(IContatoRepositorio contatoRepositorio,
                                 ISessao sessao)
        {
            //Esta linha pega o objeto recém-entregue (armazenado temporariamente em contatoRepositorio) e o move para o "Armário de Referência" (_contatoRepositorio).
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);

            return View(contatos);
        }
        // Método GET
        // Ele apenas chama return View(), que diz ao ASP.NET Core: "Busque e exiba o formulário HTML/Razor (a View) que tem o nome Criar.cshtml
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu contato!";
                }

                    return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu contato!, mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        // Método POST
        // Este método é acionado quando o usuário envia o formulário preenchido
        // IActionResult é o Tipo de Retorno Padrão que você usa em todos os métodos de ação (como Index(), Criar(), Editar()) em um Controller do ASP.NET Core MVC.
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Criar o contato apenas para o usuario logado
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                // verifica se o modelo é valido(está tudo preenchido ou não) e se não for valido volta para a pagina com o conteudo escrito 
                return View(contato);
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}