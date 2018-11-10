using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PartialResponse.Core;

namespace OpenSim.WebServer.App.Controllers
{
    public static class PartialResponseFieldsExtensions
    {
        public static bool TryGetFields(this HttpRequest request, out Fields result)
        {
            if (!request.Query.ContainsKey("fields"))
                return false;

            if (!Fields.TryParse(request.Query["fields"][0], out var fields))
            {
                return false;
            }

            result = fields;
            return true;
        }

        public static FieldsTreeNode UnfoldFieldsTree(this IEnumerable<IEnumerable<string>> fieldsParts)
        {
            var nodes = GetNodes(fieldsParts).ToList();
            if (nodes.Count == 0)
                throw new ArgumentException("Fields collection should not be empty", nameof(fieldsParts));
            
            if (nodes.Count > 1)
                throw new ArgumentException("Fields collection doesn't have common parent for every field paths", nameof(fieldsParts));

            return nodes.First();
        }

        public static FieldsTreeNode GetEmbeddedNode(this FieldsTreeNode node) =>
            node.Nodes.FirstOrDefault(n => n.Value == "_embedded");

        private static IEnumerable<FieldsTreeNode> GetNodes(IEnumerable<IEnumerable<string>> paths) => 
            from grouping in paths.GroupBy(p => p.First())
            let childrenPaths = grouping.Select(p => p.Skip(1)).Where(p => p.Any())
            select new FieldsTreeNode(
                grouping.Key, 
                childrenPaths.Any() ? GetNodes(childrenPaths) : Enumerable.Empty<FieldsTreeNode>());
    }
}
