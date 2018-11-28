using System;
using System.Collections.Generic;
using WebApi.Hal.Interfaces;

namespace OpenSim.WebServer.Controllers
{
    public abstract class ResourseEmbeddedRelationsSchema<TResource, TModel> where TResource : IResource
    {
        private Dictionary<string, Func<TResource, TModel, string, object>> embedActions =
            new Dictionary<string, Func<TResource, TModel, string, object>>();

        public Func<TResource, TModel, string, object> this[string relationName] => embedActions[relationName];

        public void RegisterEmbeddedRelation(string relationName, Func<TResource, TModel, string, object> embedAction) =>
            embedActions.Add(relationName, embedAction);
    }
}