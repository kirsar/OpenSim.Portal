using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OpenSim.WebServer.App.Controllers
{
    public static class PartialResponseFieldsExtensions
    {
        [Obsolete("Use dynamic relation injection")]
        public static bool HasFieldsQuery(this HttpRequest request) =>
            request.Query.ContainsKey("fields");

        public static FieldsTreeNode UnfoldFieldsTree(this IEnumerable<string> fields)
        {
            var nodes = GetNodes(fields.Select(f => f.Split('/'))).ToList();
            if (nodes.Count == 0)
                throw new ArgumentException("Fields collection should not be empty", nameof(fields));
            
            if (nodes.Count > 1)
                throw new ArgumentException("Fields collection doesn't have common parent for every field paths", nameof(fields));

            return nodes.First();
        }

        private static IEnumerable<FieldsTreeNode> GetNodes(IEnumerable<IEnumerable<string>> paths)
        {
            var distinctPaths = paths.GroupBy(p => p.First());
            return distinctPaths
                .Select(grouping => new FieldsTreeNode(
                    grouping.Key, 
                    grouping.Any(p => p.Any()) 
                        ? GetNodes(grouping.Where(p => p.Any()).Select(p => p.Skip(1))) 
                        : Enumerable.Empty<FieldsTreeNode>()));
        }
    }

    public class FieldsTreeNode
    {
        private ICollection<FieldsTreeNode> nodes = new List<FieldsTreeNode>(); 

        public FieldsTreeNode(string value, IEnumerable<FieldsTreeNode> nodes)
        {
            Value = value;
            this.nodes = new List<FieldsTreeNode>(nodes);
        }

        public string Value { get; }

        public IEnumerable<FieldsTreeNode> Nodes => nodes;

        public void AddNode(FieldsTreeNode node) => nodes.Add(node);
    }
}
