using System;
using System.Collections.Generic;
using WebApi.Hal.Interfaces;

namespace OpenSim.WebServer.Controllers
{
    public abstract class ResourseEmbeddedRelationsSchema<TResource, TModel> where TResource : IResource
    {
        private Dictionary<string, Func<TResource, TModel, object>> embedActions =
            new Dictionary<string, Func<TResource, TModel, object>>();

        public Func<TResource, TModel, object> this[string relationName] => embedActions[relationName];

        protected void RegisterEmbeddedRelation(string relationName, Func<TResource, TModel, object> embedAction) =>
            embedActions.Add(relationName, embedAction);
    }
}