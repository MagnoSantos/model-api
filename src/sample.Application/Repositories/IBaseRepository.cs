using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sample.Application.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Consulta todos os registros
        /// </summary>
        /// <returns>Instância(s) do(s) objeto(s) retornado(s).</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Consulta um registro seguindo a clausula especificada. <br />
        /// Por padrão, esses objetos não são rastreados após a consulta.
        /// </summary>
        /// <param name="predicate">Expressão de consulta</param>
        /// <returns>Instância do objeto retornado.</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Consulta um registro seguindo a clausula especificada. <br />
        /// Assinatura que viabiliza especificar se deseja rastrear o objeto desde o momento da consulta ou não.
        /// </summary>
        /// <param name="predicate">Expressão de consulta</param>
        /// <param name="tracking">Informa se o ORM deve rastrear esse objeto desde o momento da consulta, possibilitando assim detectar alterações e gerar históricos de modificações</param>
        /// <returns>Instância do objeto retornado.</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, bool tracking);

        /// <summary>
        /// Consulta vários registros seguindo a clausula especificada. <br />
        /// Por padrão, esses objetos não são rastreados após a consulta.
        /// </summary>
        /// <param name="predicate">Expressão de consulta</param>
        /// <returns>Instância(s) do(s) objeto(s) retornado(s).</returns>
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Consulta um registro seguindo a clausula especificada. <br />
        /// Assinatura que viabiliza especificar se deseja rastrear o objeto desde o momento da consulta ou não.
        /// </summary>
        /// <param name="predicate">Expressão de consulta</param>
        /// <param name="tracking">Informa se o ORM deve rastrear esse objeto desde o momento da consulta, possibilitando assim detectar alterações e gerar históricos de modificações</param>
        /// <returns>Instância do objeto retornado.</returns>
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, bool tracking);

        /// <summary>
        /// Provê um IQueryable da entidade para montar consultas personalizadas.
        /// </summary>
        /// <returns>IQueryable da entidade</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Adiciona um novo registro
        /// </summary>
        /// <param name="entity">Registro a ser adicionado</param>
        void Add(TEntity entity);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <param name="entity">Registro a ser atualizado</param>
        void Update(TEntity entity);

        /// <summary>
        /// Adiciona um registro
        /// </summary>
        /// <param name="entity">Registro a ser removido</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Salvar as manipulações registradas
        /// </summary>
        Task SaveChangesAsync();
    }
}