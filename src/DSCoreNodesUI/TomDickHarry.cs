﻿using System.Collections.Generic;
using DSCoreNodesUI;
using Dynamo.Models;
using ProtoCore.AST.AssociativeAST;

namespace Dynamo.Nodes
{
    public enum TomDickHarry
    {
        Tom,
        Dick,
        Harry
    };

    [NodeName("Tom Dick and Harry")]
    [NodeCategory(BuiltinNodeCategories.REVIT_SELECTION)]
    [NodeDescription("Every tom dick and harry.")]
    [IsDesignScriptCompatible]
    public class TomDickHarryList : EnumBase
    {
        public TomDickHarryList() : base(typeof(TomDickHarry)) { }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            //var args = new List<AssociativeNode>
            //{
            //    AstFactory.BuildStringNode((Items[SelectedIndex].Item).ToString())
            //};

            //var node = AstFactory.BuildFunctionCall("PiTimes2", new List<AssociativeNode>());

            //return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), node) };

            var functionCall = new FunctionCallNode
            {
                Function = new IdentifierNode("Math.PiTimes2")
            };

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
        }
    }
}