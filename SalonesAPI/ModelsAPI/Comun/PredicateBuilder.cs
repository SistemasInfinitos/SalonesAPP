using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SalonesAPI.ModelsAPI.Comun
{
    public static class PredicateBuilder
    {
        /// <summary>
        /// Crea un predicado que se evalúa como verdadero.
        /// </summary>
        public static Expression<Func<T, bool>> True<T>() { return param => true; }

        /// <summary>
        /// Crea un predicado que se evalúa como falso.
        /// </summary>
        public static Expression<Func<T, bool>> False<T>() { return param => false; }

        /// <summary>
        /// Crea una expresión de predicado a partir de la expresión lambda especificada.
        /// </summary>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate) { return predicate; }

        /// <summary>
        /// Combina el primer predicado con el segundo usando el "y" lógico.
        /// </summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// Combina el primer predicado con el segundo usando el "o" lógico.
        /// </summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// Niega el predicado.
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            UnaryExpression negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>
        /// Combina la primera expresión con la segunda usando la función de combinación especificada.
        /// </summary>
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parámetros (mapa de los parámetros del segundo a los parámetros del primero)
            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // reemplace los parámetros en la segunda expresión lambda con los parámetros en la primera
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // crear una expresión lambda combinada con parámetros de la primera expresión
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// Recopilador de parámetros
        /// </summary>
        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {

                if (map.TryGetValue(p, out ParameterExpression replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }

        /// <summary>
        /// orderByPropertyes el nombre de propiedad por el que desea ordenar y, si pasa order="desc" como parámetro, 
        /// se clasificará en orden descendente; de lo contrario, se ordenará en orden ascendente order="asc".
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderByProperty"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy2<TEntity>(this IQueryable<TEntity> source, string orderByProperty, string order)
        {
            bool desc = false;
            if (order == "desc")
            {
                desc = true;
            }
            else
            {
                desc = false;
            }
            try
            {

                string command = desc ? "OrderByDescending" : "OrderBy";

                var type = typeof(TEntity);

                //var type3 = typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                var type2 = typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                var property = type2.Where(x => orderByProperty.ToUpper().Contains(x.Name.ToUpper())).FirstOrDefault();

                //var property = type.GetProperty(orderByProperty);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression =
                Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
                return source.Provider.CreateQuery<TEntity>(resultExpression);

            }
            catch (Exception e)
            {
                return source.Provider.CreateQuery<TEntity>(null);
            }
            //return source.Provider.CreateQuery<TEntity>(null);
        }

        /// <summary>
        /// orderByPropertyes el nombre de propiedad por el que desea ordenar y, si pasa order="desc" como parámetro, 
        /// se clasificará en orden descendente; de lo contrario, se ordenará en orden ascendente order="asc".
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderByProperty"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ThenBy2<TEntity>(this IQueryable<TEntity> source, string orderByProperty, string order)
        {
            bool desc = false;
            if (order == "desc")
            {
                desc = true;
            }
            else
            {
                desc = false;
            }
            string command = desc ? "ThenByDescending" : "ThenBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression =
            Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}