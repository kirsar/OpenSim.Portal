﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PartialResponse.Core;

namespace OpenSim.Portal.Controllers
{
    public static class PartialResponseFieldsExtensions
    {
        internal static IEnumerable<FieldsTreeNode> GetFieldsDefinition(this HttpRequest request) =>
            request.TryGetFields(out var fields)
                ? fields.Values.Select(f => f.Parts).UnfoldFieldsTree()
                : Enumerable.Empty<FieldsTreeNode>();

        internal static bool TryGetFields(this HttpRequest request, out Fields result)
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

        public static IEnumerable<FieldsTreeNode> UnfoldFieldsTree(this IEnumerable<IEnumerable<string>> fieldsParts)
        {
            var nodes = GetNodes(fieldsParts).ToList();
            if (nodes.Count == 0)
                throw new ArgumentException("Fields collection should not be empty", nameof(fieldsParts));
            
            return nodes;
        }

        private static IEnumerable<FieldsTreeNode> GetNodes(IEnumerable<IEnumerable<string>> paths) => 
            from grouping in paths.GroupBy(p => p.First())
            let childrenPaths = grouping.Select(p => p.Skip(1)).Where(p => p.Any())
            select new FieldsTreeNode(
                grouping.Key, 
                childrenPaths.Any() ? GetNodes(childrenPaths) : Enumerable.Empty<FieldsTreeNode>());

        internal static FieldsTreeNode GetEmbeddedFieldNode(this IEnumerable<FieldsTreeNode> fields) =>
            fields.FirstOrDefault(n => n.Value == "_embedded");
    }
}
