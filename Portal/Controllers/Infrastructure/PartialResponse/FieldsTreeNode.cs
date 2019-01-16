using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSim.Portal.Controllers
{
    public class FieldsTreeNode
    {
        public FieldsTreeNode(string value, IEnumerable<FieldsTreeNode> nodes)
        {
            Value = value;
            Nodes = nodes;
        }

        public IEnumerable<FieldsTreeNode> Nodes { get; }

        public string Value { get; }

        public override string ToString() => Value;

        public FieldsTreeNode GetByPath(string nodePath) => GetByPath(nodePath.Split('/'));

        private FieldsTreeNode GetByPath(IEnumerable<string> pathParts)
        {
            if (Value != pathParts.First())
                throw new ArgumentOutOfRangeException(nameof(pathParts));

            pathParts = pathParts.Skip(1);
            if (!pathParts.Any())
                return this;

            var node = Nodes.Single(n => n.Value == pathParts.First());
            return node.GetByPath(pathParts);
        }
    }
}