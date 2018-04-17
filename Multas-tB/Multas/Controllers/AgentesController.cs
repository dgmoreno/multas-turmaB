﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        // cria uma variável que representa a Base de Dados
        private MultasDb db = new MultasDb();

        // GET: Agentes
        /// <summary>
        /// lista os agentes
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // db.Agentes.ToList() -> em sql: Select * From Agentes
            // enviar para a View uma lista com todos os Agentes, da BD

            //obter a lista de todos os agentes
            // em sql: SELECT * FROM Agentes ORDER BY Nome;
            var listaDeAgentes = db.Agentes.ToList().OrderBy(a=>a.Nome);


            return View(listaDeAgentes);
        }

        // GET: Agentes/Details/5
        // int? significa que pode ser de preenchimento facultativo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            // se se escrever 'int?' é possível
            // não fornecer o valor para o ID e não há erro

            // proteção para o caso de não ter sido fornecido um ID válido
            if (id == null)
            {
                //instrução original
                //devolve um erro qd não há ID
                //logo não é possível pesquisar por um Agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // procura na BD, o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            // proteção para o caso de não ter sido encontrado qq Agente 
            // que tenha o ID fornecido
            if (agente == null)
            {
                //o agente não foi encontrado
                //logo, gera-se uma msg de erro
                //return HttpNotFound();
                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }

            // entrega à View os dados do Agente encontrado
            return View(agente);
        }

        // GET: Agentes/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // apresenta a View para se inserir um novo Agente
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        // anotador para HTTP Post
        [HttpPost]
        // anotador para proteção por roubo de identidade
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,HttpPostedFileBase uploadFotografia)
        {
            // escrever os dados de um novo Agente na Bd

            //especificar o ID do novo Agente
            int idNovoAgente = db.Agentes.Max(a => a.ID) + 1;
            //guardar o ID do novo Agente
            agente.ID = idNovoAgente;
            //especificar (escolher) o nome do ficheiro
            string nomeImagem = "Agente_"+idNovoAgente+".jpg";

            //var. auxiliar
            string path = "";
            //validar se a imagem foi fornecida
            if (uploadFotografia != null)
            {
                //o ficheiro foi fornecido
                //validar se o que foi fornecido é uma imagem ----> fazer em casa
                //formatar o tamanho da imagem
                //criar o caminho completo até ao sítio onde o ficheiro
                //será guardado
                path = Path.Combine(Server.MapPath("~/imagens/"),nomeImagem);

                //guardar o nome do ficheiro
                agente.Fotografia = nomeImagem;
            }
            else
            {
                //não foi fornecido qualquer ficheiro
                //gerar uma mensagem de erro
                ModelState.AddModelError("", "Não foi fornecida uma imagem");
                //devolver o controlo à View
                return View(agente);
            }
     
            // ModelState.IsValid -> confronta os dados fornecidos da View
            //                       com as exigências do Modelo
            if (ModelState.IsValid)
            {
                // adiciona o novo Agente à BD
                db.Agentes.Add(agente);
                // faz 'Commit' às alterações
                db.SaveChanges();
                //escrever o ficheiro com a fotografia no disco rígido na pasta
                uploadFotografia.SaveAs(path);
                // se tudo correr bem, redireciona para a pagina de Index
                return RedirectToAction("Index");
            }

            // se houver um erro, reapresenta os dados do Agente
            // na View
            return View(agente);
        }

        // GET: Agentes/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //instrução original
                //devolve um erro qd não há ID
                //logo não é possível pesquisar por um Agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                //o agente não foi encontrado
                //logo, gera-se uma msg de erro
                //return HttpNotFound();
                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                // neste caso já existe um Agente
                // apenas quero EDITAR os seus dados
                db.Entry(agentes).State = EntityState.Modified;
                //efetuar 'Commit'
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        /// <summary>
        /// apresenta na View os dados de um agentes, 
        /// com vista à sua eventual, eliminação
        /// </summary>
        /// <param name="id">identificador do agente</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //instrução original
                //devolve um erro qd não há ID
                //logo não é possível pesquisar por um Agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index"); ;
            }

            //pesquisar pelo Agente, cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            //verificar se o Agente foi encontrado
            if (agente == null)
            {
                //o agente não foi encontrado
                //logo, gera-se uma msg de erro
                //return HttpNotFound();
                //redirecionar para uma página que nós controlamos
                return RedirectToAction("Index");
            }
            //apresentar o Agente
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agentes = db.Agentes.Find(id);
            // remove o Agente da BD
            db.Agentes.Remove(agentes);
            //Commit
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
