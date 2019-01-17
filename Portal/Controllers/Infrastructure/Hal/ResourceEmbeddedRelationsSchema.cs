using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebApi.Hal.Interfaces;

namespace OpenSim.Portal.Controllers
{
    public abstract class ResourceEmbeddedRelationsSchema<TResource, TModel> where TResource : IResource
    {
        private Dictionary<string, Func<TResource, TModel, string, UserManager<Model.User>, object>> embedActions =
            new Dictionary<string, Func<TResource, TModel, string, UserManager<Model.User>, object>>();

        public Func<TResource, TModel, string, UserManager<Model.User>, object> this[string relationName] => embedActions[relationName];

        public void RegisterEmbeddedRelation(string relationName, Func<TResource, TModel, string, UserManager<Model.User>, object> embedAction) =>
            embedActions.Add(relationName, embedAction);

        public void RegisterEmbeddedRelation(string relationName, Func<TResource, TModel, string, object> embedAction) =>
            embedActions.Add(relationName, (resource, model, relation, _) => embedAction(resource, model, relation));
    }
}